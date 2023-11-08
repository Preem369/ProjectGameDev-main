using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
     private int damageAmount = 1;

    private void Start()
    {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnermyHealth enermyHealth = collision.gameObject.GetComponent<EnermyHealth>();
        enermyHealth?.TakeDamage(damageAmount);
        
    }
}
