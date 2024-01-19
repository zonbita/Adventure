using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    Controls controls;
    [Header("Options")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer characterSR;
    public Health health;
    Animator animator;

    public Vector3 moveInput;
    public GameObject HealthBar;
    public Vector3 HealthBarOffset = new Vector3(0f, -15.42f, 0f);
    Collider2D coll;

    private void Awake()
    {
/*        controls = new Controls();
        controls.Player.Move.performed += c =>
        {
            moveInput.x = c.ReadValue<float>();
        };*/
    }

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        
        GameManager.Instance.GameOver += GameOver;
        GameManager.Instance.GameWin += GameWin;
        GameManager.Instance.GameStart += GameStart;
         
       
    }

    public void Init(List<Stat> Stats, Sprite sprite)
    {
        foreach (Stat s in Stats)
        {


            switch (s._StatName)
            {
                case StatName.Speed:
                    moveSpeed = s._StatValue;
                    break;
                case StatName.LootRadius:
                    GetComponentInChildren<Looter>().SetLooterRadius(s._StatValue);
                    break;
                case StatName.HP:
                    health.Init((int)s._StatValue);
                    break;
                case StatName.Luck:
                    GameManager.Instance.CoinLuck = (int)s._StatValue;
                    break;
                case StatName.Regen:
                    health.Regen((int)s._StatValue);
                    break;
                default:
                    
                    break;
            }
        }
        if(!sprite)
            characterSR.sprite = sprite;
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
        

        // Rotate Face
        if (moveInput.x != 0)
            if (moveInput.x < 0)
                characterSR.flipX = true;
            else
                characterSR.flipX = false;
    }

    private void OnEnable()
    {
        //controls.Enable();
    }

    private void OnDisable()
    {
       // controls.Disable();
    }

}