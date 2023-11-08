using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnermyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int typeEnermy = 0; //0 = slime , 1 = Ghost ,2 = Grape
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 13f;
    [SerializeField] private AudioSource deathSource;

    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;



    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();  
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetactDeadRoutine());
    }

    private IEnumerator CheckDetactDeadRoutine()
    {
        yield return new WaitForSeconds(flash.GetTimeRestoreDefault());
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            
            GetComponent< PickUpSpawner>()?.DropItem(typeEnermy);
            Instantiate(deathVFXPrefab,transform.position,Quaternion.identity);
            deathSource.Play();
            Destroy(gameObject);
        }
    }

    
}