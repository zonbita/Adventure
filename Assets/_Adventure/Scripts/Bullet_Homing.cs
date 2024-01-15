using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Homing : Bullet
{
    Transform target;
    public float rotateSpeed = 200f;
    public float speed = 5f;

    public virtual void Init(int minDamage, int maxDamage, Transform Target)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.target = Target;
    }

    private void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
        else
        {
            target = WeaponManager.instance.FindNearestEnemy(this.transform.position);
        }

    }

/*    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Enemy"))
        { 
        }
    }*/


}
