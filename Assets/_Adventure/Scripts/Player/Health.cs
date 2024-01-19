using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, I_Damage
{
    public int maxHealth;
    public int currentHealth;
    public int XP = 1;
    public HealthBar healthBar;

    private float safeTime;
    private int hpregen;
    Coroutine regenCoroutine;
    public float safeTimeDuration = 0f;
    public bool isDead = false;
    public bool camShake = false;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);

        GameManager.Instance.GameRevive += Revive;
    }

    public void Revive()
    {
        currentHealth = maxHealth;
        isDead = false;
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);

    }

    public void Healing(int i)
    {
        currentHealth = Mathf.Clamp(i, currentHealth, maxHealth);
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void Init(int max)
    {
        maxHealth = max;
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }
    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }

    public void Regen(int hp)
    {
        hpregen = hp;

        if (regenCoroutine != null) StopCoroutine("LoopRegen");

        regenCoroutine = StartCoroutine("LoopRegen");
    }

    IEnumerator LoopRegen()
    {
        while (currentHealth < maxHealth)
        {
            currentHealth += hpregen;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);
            yield return new WaitForSeconds(1);
        }
    }

        

    public void TakeDamageEffect(int damage, int max)
    {
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (this.gameObject.tag == "Enemy")
            {

                WeaponManager.instance.RemoveEnemyToFireRange(this.transform);
                GameManager gm = GameManager.Instance;
                if (gm)
                {
                    Vector3 p = this.transform.position;
                    gm.ItemSpawner.GetComponent<ItemSpawner>().SpawnRandomItem(p);
                    gm.SpawnExp(p, XP);
                    gm.SpawnDeathEffect(p);
                }
                AudioManager.instance?.PlaySfx(AudioManager.Sfx.Dead);

                Destroy(this.gameObject, 0.125f);

            }
            if (this.gameObject.tag == "Player") GameManager.Instance.GameOver();
            isDead = true;
        }

        // If player then update health bar
        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
        
    }
}
