using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerController Mario;

    public bool goingLeft = false;
    public float speed = 2.0f;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goingLeft = true;
    }

    // Update is called once per frame
    void Update()
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
            Destroy(gameObject); // Destruye el GameObject que contiene este script
        }

        if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Mario.Dead = true;
        }
    }
}
