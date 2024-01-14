using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public int XP = 1;
    public HealthBar healthBar;

    private float safeTime;
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

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
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
                isDead = true;
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }


    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }
}
