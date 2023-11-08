using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enermyType;
    [SerializeField] private float attackCD = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
        
    private bool canAttack = true;
    private enum State{
        Roaming,
        Attacking
    }

    private State state;
    private EnermyPathFinding enermyPathFinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private void Awake()
    {
        enermyPathFinding = GetComponent<EnermyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                //roam
                Roaming();
                break;
            case State.Attacking:
                //attack
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enermyPathFinding.MoveTo(roamPosition);

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enermyType as IEnermy).Attack();
            StartCoroutine(AttackCDRoutine());

            if(stopMovingWhileAttacking)
            {
                enermyPathFinding.StopMoving();
            }
            else
            {
                enermyPathFinding.MoveTo(roamPosition);
            }
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }

    //every 2 second random new position
    /*private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enermyPathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }*/

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }
}
