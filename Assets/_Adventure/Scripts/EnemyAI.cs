using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public SpriteRenderer characterSR;
    //public Animator animator;
    public int minDamage;
    public int maxDamage;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    Health PlayerHealth;
    Coroutine moveCoroutine;
    Vector2 force = new Vector2(-1f, 1f);
    public float freezeDurationTime;
    float freezeDuration;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        freezeDuration = 0;
        target = FindObjectOfType<Player>().transform;

        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
    }

    private void FixedUpdate()
    {
        if (force.x >= 0.005f && characterSR.flipX != false)
            characterSR.flipX = false;
        else if (force.x <= .005f && characterSR.flipX != true)
            characterSR.flipX = true;
    }

    void CalculatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            path = p;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    public void FreezeEnemy()
    {
        freezeDuration = freezeDurationTime;
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {
            while (freezeDuration > 0)
            {
                freezeDuration -= Time.deltaTime;
                yield return null;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;



            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth = collision.GetComponent<Health>();
            InvokeRepeating("DamagePlayer", 0, 1f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke("DamagePlayer");
            PlayerHealth = null;
        }

    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        PlayerHealth.TakeDam(damage);
        //
        PlayerHealth.GetComponent<Player>().TakeDamageEffect(damage);
  
    }
}
