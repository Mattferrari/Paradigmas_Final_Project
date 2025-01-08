using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerController Mario;

    private Animator Animator;

    public bool goingLeft = false;
    public float speed = 2.0f;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        goingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb)

        {
            if (goingLeft)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Enemy"))
        {
            if (goingLeft)
            {
                goingLeft = false;
            }
            else
            {
                goingLeft = true;
            }
        }

        if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
        {
            Mario.Attacked = true;
            Destroy(gameObject, 1f); // Destruye el GameObject que contiene este script
            Animator.SetTrigger("GoombaDead");
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            speed = 0;
        }

        if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Mario.GetHit();
        }
    }
}
