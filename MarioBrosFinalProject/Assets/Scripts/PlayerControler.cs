using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 15.0f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public bool Attacked = false;
    public bool Dead = false;

    private Animator Animator;

    public BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // TODO
        // funci�n aparte
        // Mario/Luigi key diferences
        int move = 0;
        if (Input.GetKey(KeyCode.A)) 
        {
            move = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else { move = 0; }

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;  // Evita saltos m�ltiples en el aire
        }

        if (Attacked)
        {
            rb.AddForce(new Vector2(0, jumpForce/2), ForceMode2D.Impulse);
            Attacked = false;
        }

        if (Dead)
        {
            Destroy(gameObject);
        }

        Animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        Animator.SetBool("Grounded", isGrounded);
        Animator.SetFloat("Size", boxCollider.size.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") | collision.gameObject.CompareTag("Element"))
        {
            isGrounded = true;  // Permite saltar nuevamente cuando Mario toca el suelo
        }
    }
    
}