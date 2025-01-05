using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 15.0f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float acceleration = 1000f;
    public float maxspeed = 8f;
    public int move = 0;
    public bool isJumping = false;
    public float jumpTimer = 0f;
    public float maxJumpingTime = 1.5f;

    public bool Attacked = false;
    public bool Dead = false;

    private Animator Animator;

    public BoxCollider2D boxCollider;
    public float defaultgravity;

    public bool isBigMario = false;
    public bool isFireMario = false;
    public float fireBallTimer;
    public float rechargeTime;

    public GameObject fireBall;
    public int perspective;

    public GameManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        defaultgravity = rb.gravityScale;
        fireBallTimer = Time.time;
        perspective = 1;
    }

    void Update()
    {
        // TODO
        // función aparte
        // Mario/Luigi key diferences


        if (isJumping)
        {
            if (rb.velocity.y < 0f)
            {
                rb.gravityScale = defaultgravity;
                if (isGrounded)
                {
                    isJumping = false;
                    jumpTimer = 0f;
                }    
            }
            else if (rb.velocity.y > 0f)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    jumpTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    if (jumpTimer > maxJumpingTime) 
                    {
                        rb.gravityScale = defaultgravity * 3f;
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            move = -1;
            transform.localScale = new Vector2(move, 1);
            if (perspective > 0)
            {
                ChangePerspective();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1;
            transform.localScale = new Vector2(move, 1);
            if (perspective < 0)
            {
                ChangePerspective();
            }
        }
        else { move = 0; }

        //rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Attacked)
        {
            Jump();
            Attacked = false;
        }

        if (Dead)
        {
            Debug.Log("I lost");
            manager.LooseLife();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isFireMario && Time.time - fireBallTimer > rechargeTime)
        {
            ThrowFire();
            fireBallTimer = Time.time;

        }

        Animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        Animator.SetBool("Grounded", isGrounded);
        Animator.SetBool("isBigMario", isBigMario);
        Animator.SetBool("isFireMario", isFireMario);
    }

    private void FixedUpdate()
    {
        //Vector2 forceAcceleration = new Vector2((int)move * acceleration, 0f);
        //rb.AddForce(forceAcceleration);
        //Debug.Log(forceAcceleration);
        float vBase = rb.velocity.x;
        if (vBase <= speed && vBase >= -speed)
        {
            vBase = speed*move;
        }
        float velocityX = vBase + move*acceleration*Time.deltaTime;

        if (velocityX >= maxspeed | velocityX <= -maxspeed)
        {
            //velocityX = Mathf.Clamp(rb.velocity.x, -maxspeed, maxspeed);
            velocityX = move * maxspeed;
            Debug.Log("Hola2");
        }

        Debug.Log(velocityX);

        Vector2 velocity = new Vector2(velocityX, rb.velocity.y);
        rb.velocity = velocity;
    }

    void ChangePerspective()
    {
        perspective *= -1;
    }
    void Jump()
    {
        
        if (isGrounded && !Attacked) // normal jump
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (!isGrounded && Attacked) // jump when you land on a an enemy
        {
            rb.AddForce(new Vector2(0, jumpForce / 2), ForceMode2D.Impulse);
            Attacked = false;
        }
        isJumping = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") | collision.gameObject.CompareTag("Element"))
        {
            isGrounded = true;  // Permite saltar nuevamente cuando Mario toca el suelo
        }
    }

    public void GetHit()
    {
        if (isFireMario)
        {
            isFireMario = false;
            isBigMario = true;
        }
        else if (isBigMario)
        {
            isBigMario = false;
            boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
            boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - 0.5f);
        }
        else
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Mori");
        Dead = true;
    }
    public void FireMario()
    {
        isFireMario = true;
        isBigMario = false;
    }

    public void ThrowFire()
    {
        Vector3 offset = new Vector3(perspective, 0, 0);
        Vector3 spawnPosition = transform.position + offset;
        GameObject fireball = Instantiate(fireBall, spawnPosition, Quaternion.identity);

        fireball.GetComponent<FireBall>().SetMario(this);

    }
    public void BigMario()
    {
        isBigMario = true;
    }

}