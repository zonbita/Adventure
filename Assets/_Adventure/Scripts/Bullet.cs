using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    protected int minDamage = 6;
    protected int maxDamage = 16;
    public bool IsBullet = true;
    protected bool TargetPlayer = false;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(int minDamage, int maxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public virtual void Init(int Damage, bool TargetPlayer)
    {
        this.minDamage = this.maxDamage = Damage;
        this.TargetPlayer = TargetPlayer;
    }

    public virtual void Init(int Damage, bool TargetPlayer, Vector2 bulletForce)
    {
        this.minDamage = this.maxDamage = Damage;
        this.TargetPlayer = TargetPlayer;
        rb.AddForce(bulletForce, ForceMode2D.Impulse);
    }

    public virtual void Init(int minDamage, int maxDamage, Vector2 bulletForce)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        rb.AddForce( bulletForce, ForceMode2D.Impulse);
    }

 
    protected virtual void Start()
    {
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Range);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (TargetPlayer)
        {
            if (collision.CompareTag("Player"))
            {

                int damage = Random.Range(minDamage, maxDamage);
                I_Damage[] dmg = collision.GetComponents<I_Damage>();
                foreach (I_Damage i in dmg)
                {
                    i.TakeDamage(damage);
                    i.TakeDamageEffect(damage, maxDamage);
                }

                gameObject.SetActive(IsBullet ? false : true);

            }
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {

                int damage = Random.Range(minDamage, maxDamage);
                I_Damage[] dmg = collision.GetComponents<I_Damage>();
                foreach (I_Damage i in dmg)
                {
                    i.TakeDamage(damage);
                    i.TakeDamageEffect(damage, maxDamage);
                }
                gameObject.SetActive(IsBullet ? false : true);

            }
        }

    }

}