using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Shell : MonoBehaviour, IEnemy
{
    public PlayerController Mario;
    public Rigidbody2D rb;

    private Animator Animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float shellTime = 5f;
    [SerializeField] private bool moving = false;

    private float t0 = Time.time;
    private int movedir = 1;


    public Collider2D superiorCollider;
    public Collider2D leftCollider;
    public Collider2D rightCollider;


    public void Move()
    {
        rb.velocity = new Vector2(movedir * speed, rb.velocity.y);
    }
    public void Atack()
    {
        Mario.GetHit();
    }

    public void GetKilled()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Mario = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time - t0 > shellTime)
            {
                ShellAnimation();
            }
            if (moving) { Move(); }
    }

    private void ShellAnimation()
    {
        Animator.SetTrigger("Shell");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!moving)
        {
            if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
            {
                GetKilled();
                Mario.Attacked = true;
            }
            else if (collision.otherCollider == leftCollider && collision.gameObject.CompareTag("player"))
            {
                moving = true;
                movedir = 1;
            }
            else if (collision.otherCollider == rightCollider && collision.gameObject.CompareTag("Plater"))
            {
                moving = true;
                movedir = -1;
            }
        }

        else 
        {
            if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
            {
                moving = false;
            }
            else if (collision.otherCollider != superiorCollider && collision.gameObject.CompareTag("player"))
            {
                Atack();
                moving = false;
            }
            else if (collision.gameObject.CompareTag("Element"))
            {
                movedir *= -1;
            }
            IEnemy Enemy = collision.gameObject.GetComponent<IEnemy>();
            if (Enemy != null) { Enemy.GetKilled(); }

        }
    }
}

