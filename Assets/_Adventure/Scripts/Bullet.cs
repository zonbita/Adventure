using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage = 6;
    public int maxDamage = 16;
    public bool IsBullet = true;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(int minDamage, int maxDamage)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public void Init(int minDamage, int maxDamage, Vector2 bulletForce)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        rigid.AddForce( bulletForce, ForceMode2D.Impulse);
    }

    private void Start()
    {
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Range);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
