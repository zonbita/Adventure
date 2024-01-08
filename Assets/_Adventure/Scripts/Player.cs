using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public SpriteRenderer characterSR;
    Animator animator;

    public float dashBoost = 2f;
    private float dashTime;
    public float DashTime;
    private bool once;
 

    public Vector3 moveInput;

    
    public GameObject HealthBar;
    public Vector3 HealthBarOffset = new Vector3(0f, -15.42f, 0f);
    Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        GameManager.Instance.GameOver += GameOver;
        GameManager.Instance.GameWin += GameWin;
        GameManager.Instance.GameStart += GameStart;

       
    }

    private void GameStart()
    {

    }

    private void GameWin()
    {

    }

    private void GameOver()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // Movement
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        transform.position += (moveSpeed * Time.deltaTime * moveInput) / 3;
        HealthBar.transform.localPosition = HealthBarOffset + transform.position;
        //
        if (moveInput.sqrMagnitude >= 0.01)  
            animator.SetBool("isWalk", true); 
        else
             animator.SetBool("isWalk", false);
        

        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            animator.SetBool("Roll", true);
            moveSpeed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        // Rotate Face
        if (moveInput.x != 0)
            if (moveInput.x < 0)
                characterSR.flipX = true;
            else
                characterSR.flipX = false;
    }

    public void TakeDamageEffect(int damage)
    {
        if (GameManager.Instance.damPopUp != null)
        {
            if(GameManager.Instance.PopupID != -1)
            {
                Transform instance = GameManager.Instance.pool.Get(GameManager.Instance.PopupID).transform;
                instance.position = transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Animator animator = instance.GetComponentInChildren<Animator>();
                animator.Play("red");
            }
            else
            {
                GameObject instance = Instantiate(GameManager.Instance.damPopUp, transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Animator animator = instance.GetComponentInChildren<Animator>();
                animator.Play("red");
            }

        }
        if (GetComponent<Health>().isDead)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void ChangeSprite(Sprite s)
    {
        characterSR.sprite = s;
    }
}
