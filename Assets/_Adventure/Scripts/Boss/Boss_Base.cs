using System.Collections;
using UnityEngine;
using Pathfinding;

public class Boss_Base : MonoBehaviour
{
    public Boss_Data_SO Boss_Data_SO;
    public float moveSpeed = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public float nextWayPointDistance = 3f;
    public bool Roaming = false;

    public int minDamage = 2;
    public int maxDamage = 5;

    [HideInInspector] protected Transform target;
    [SerializeField] protected Transform SpawnProjectile;

    Vector2 force;
    Health health;
    Path path;
    Seeker seeker;
    Coroutine moveCoroutine;
    Rigidbody2D rb;
    SpriteRenderer sr;
    protected Animator animator;
    float distance;
    bool StopMove = false;

    protected static readonly int A_Idle = Animator.StringToHash("Idle");
    protected static readonly int A_Death = Animator.StringToHash("Death");
    protected static readonly int A_Walk = Animator.StringToHash("Walk");
    protected static readonly int A_Cast = Animator.StringToHash("Cast");

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;

        // Drop XP
        health.XP = 6;

        if (Boss_Data_SO)
        {
            health.maxHealth = Boss_Data_SO.maxHealth;
        }
    }
    protected virtual void Start()
    {
        
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath);
        InvokeRepeating("ChangeFace", 0f, 0.2f);
    }

    void CalculatePath()
    {
        if (seeker.IsDone() && !StopMove)
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
    public void StopMovement()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        StopMove = true;
        animator.SetBool("Walk", false);
    }

    public void EnableMovement()
    {
        StopMove = false;
    }

    void ChangeFace()
    {
        
        if (force.x >= 0.001f)
            sr.flipX = false;
        else if (force.x <= .001f)
            sr.flipX = true;
    }
    IEnumerator MoveToTargetCoroutine()
    {
        if(!animator.GetBool("Walk")) animator.SetBool("Walk", true);
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {

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
    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);

        health.GetComponent<I_Damage>()?.TakeDamageEffect(damage, maxDamage);

    }

    public void RotateGun(Vector3 pos)
    {
        Vector2 lookDir = pos - SpawnProjectile.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        SpawnProjectile.transform.rotation = rotation;

        if (SpawnProjectile.transform.eulerAngles.z > 90 && SpawnProjectile.transform.eulerAngles.z < 270) SpawnProjectile.transform.localScale = new Vector3(1, -1, 0);
        else SpawnProjectile.transform.localScale = new Vector3(1, 1, 0);
    }

}
