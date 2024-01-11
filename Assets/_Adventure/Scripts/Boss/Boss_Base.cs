using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Boss_Base : MonoBehaviour
{
    public Boss_Data_SO Boss_Data_SO;
    public float moveSpeed = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public float nextWayPointDistance = 3f;
    public bool Roaming = false;

    [SerializeReference] Transform target;

    Vector2 force;
    Health health;
    Path path;
    Seeker seeker;
    Coroutine moveCoroutine;
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
        health.maxHealth = Boss_Data_SO.maxHealth;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {

    }

    void Start()
    {
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
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

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {

            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            if (force.x >= 0.01f)
                sr.flipX =  false;
            else if(force.x <= .01f)
                sr.flipX = true;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InvokeRepeating("DamagePlayer", 0, 1f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke("DamagePlayer");
        }

    }

}
