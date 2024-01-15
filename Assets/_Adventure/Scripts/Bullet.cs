using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    protected int minDamage = 6;
    protected int maxDamage = 16;
    public bool IsBullet = true;
    protected Rigidbody2D rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Init(int minDamage, int maxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
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
        if (collision.CompareTag("Enemy"))
        {
                
                int damage = Random.Range(minDamage, maxDamage);
                collision.GetComponent<Health>().TakeDam(damage);
                collision.GetComponent<EnemyController>()?.TakeDamEffect(damage);
                gameObject.SetActive(IsBullet ? false : true);

        }
    }

}