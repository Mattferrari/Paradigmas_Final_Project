using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5.0f;
    private Rigidbody2D rb;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // TODO
        // función aparte
        // Mario/Luigi key diferences
        int move = 0;
        if (Input.GetKey(KeyCode.A)) 
        {
            move = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1;
        }
        else { move = 0; }

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;  // Evita saltos múltiples en el aire
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") | collision.gameObject.CompareTag("Element"))
        {
            isGrounded = true;  // Permite saltar nuevamente cuando Mario toca el suelo
        }
    }
    
}