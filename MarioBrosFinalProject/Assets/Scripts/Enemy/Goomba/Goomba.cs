using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour, IEnemy
{
    private Rigidbody2D rb;
    private PlayerController Mario;

    private Animator Animator;

    public int movedir = -1;
    public float speed = 2.0f;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;

    private bool dead = false;

    public void Move()
    {
        rb.velocity = new Vector2(movedir * speed, rb.velocity.y);
    }

    public void GetKilled()
    {
        // Destruye el GameObject que contiene este script
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Mario.KillingSound();
        Animator.SetTrigger("GoombaDead");
        speed = 0;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1f);
        dead = true;

    }

    public void Atack() { Mario.GetHit(); }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Mario = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Move();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == lowerCollider && (collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Enemy")))
        {
            movedir *= -1;
        }

        if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
        {
            GetKilled();
            Mario.Attacked = true;
        }

        if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Atack();
        }
    }
}
