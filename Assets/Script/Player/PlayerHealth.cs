using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool IsDead {  get; private set; }

    [SerializeField] private int maxHealth = 4;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;

    private KnockBack knockBack;
    private Flash flash;
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string SPWAN_SCENE_TEXT = "Scene_1";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();  
    }
    private void Start()
    {
        IsDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnermyAI enermy = collision.gameObject.GetComponent<EnermyAI>();

        if (enermy)
        {
            //take Damage
            TakeDamage(1, collision.transform);
            
        }
    }

    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
        
    }

    public void TakeDamage(int damageAmount ,Transform hitTranform)
    {
        if(!canTakeDamage) { return; }

        ScreenShakeManager.Instance.shakeScreen();
        knockBack.GetKnockBack(hitTranform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if(currentHealth <= 0 && !IsDead)
        {
            IsDead = true;

            EconomyManager.Instance.ResetCurrentGoldAfterDeath();
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            Debug.Log("Player Die");
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        SceneManager.LoadScene(SPWAN_SCENE_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>(); ;
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }
}
