using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        knockback = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockBack) { return; }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if(moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }
}

