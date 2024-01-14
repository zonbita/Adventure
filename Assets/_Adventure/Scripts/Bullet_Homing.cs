using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Homing : Bullet
{
    public Transform target;
    public float rotateSpeed = 200f;
    public float speed = 5f;

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
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
            c.gameObject.SetActive(false);
    }


}
