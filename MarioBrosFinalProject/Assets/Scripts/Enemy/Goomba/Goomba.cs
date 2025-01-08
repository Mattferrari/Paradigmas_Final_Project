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

    public void Move()
    {
        rb.velocity = new Vector2(movedir * speed, rb.velocity.y);
    }

    public void GetKilled()
    {
        Destroy(gameObject, 1f); // Destruye el GameObject que contiene este script
        Animator.SetTrigger("GoombaDead");
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CircleCollider2D>());
        speed = 0;
        Destroy(GetComponent<Rigidbody2D>());
        
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
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Enemy"))
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
