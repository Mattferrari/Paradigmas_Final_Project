using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private float speed = 5;
    private float lifeTime = 5;
    private Vector2 jumpForce = new Vector2(0, 1f);
    private bool isGrounded = false;
    private float timer;
    public Rigidbody2D rb;

    public Collider2D lowerCollider;
    public Collider2D upperCollider;

    public int move;
    public PlayerController Mario;

    // Start is called before the first frame update
    private void Start()
    {
        timer = Time.time;
        rb = GetComponent<Rigidbody2D>();
        move = Mario.perspective;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        if (isGrounded)
        {
            Debug.Log("Estoy");
            Jump();
        }
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Element")) && collision.otherCollider == lowerCollider)
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Element") && collision.otherCollider == upperCollider)
        {
            move *= -1;
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void Jump()
    {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    private void Move()
    {
        rb.velocity = new Vector2(move*speed, rb.velocity.y);
    }

    public void SetMario(PlayerController player)
    {
        Mario = player;
    }

}
