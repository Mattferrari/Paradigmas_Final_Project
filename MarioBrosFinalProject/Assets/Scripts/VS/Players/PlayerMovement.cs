using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // objects
    private Rigidbody2D rb;
    private Animator Animator;

    // fields
    //keycodes
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode sprintKey;

    // general
    private bool canMove = true;
    
    // X-movement related
    // direction
    private int perspective = 1;
    private int movedir = 0;
    // speed
    private float movementSpeed;
    [SerializeField] private float lowspeed = 5f;
    [SerializeField] private float maxspeed = 8f;

    // jumping related
    private bool isGrounded;
    private float jumpStartTimer = 0f;
    private bool isJumping = false;
    private float defaultgravity;
    [SerializeField] private float highJumpMinPressingTime = 1.5f;
    [SerializeField] private float jumpForce = 15.0f;

    // canMove Setter
    public void SetCanMove(bool canmove) { canMove = canmove;  }

    public int GetPerspective() { return perspective; }


    void Forcedjump() 
    {
        // set y speed = 0 before jumping
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce / 2), ForceMode2D.Impulse);

        // modify variables
        isJumping = true;
    }

    void Jump()
    {
        if (isGrounded) // normal jump
        {
            // set y speed = 0 before jumping
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void StopJumping()
    {
        isJumping = false;
        jumpStartTimer = 0f;
    }

    private void JumpIntensity()
    {
        // if W pressed 
        if (Input.GetKey(jumpKey))
        {
            jumpStartTimer += Time.deltaTime;
        }

        // if W keeps pressed after a time threshold
        if (Input.GetKeyUp(jumpKey) && jumpStartTimer > highJumpMinPressingTime)
        {
            rb.gravityScale = defaultgravity * 3f;
        }
    }

    public void MoveOnXVisuals()
    {
        transform.localScale = new Vector2(movedir, 1);
        perspective = movedir;
    }

    public void Sprint()
    {
        if (isGrounded) { movementSpeed = maxspeed; }
    }

    public void StopSprint()
    {
        movementSpeed = lowspeed;
    }
    
    private void MoveCommands()
    {
        if (canMove)
        {
            if (isJumping)
            {
                if (rb.velocity.y <= 0f)
                {
                    rb.gravityScale = defaultgravity;
                    if (isGrounded)
                    {
                        StopJumping();
                    }
                }
                else if (rb.velocity.y > 0f)
                {
                    JumpIntensity();
                }
            }

            if (Input.GetKey(moveLeftKey))
            {
                movedir = -1;
                MoveOnXVisuals();
            }
            else if (Input.GetKey(moveRightKey))
            {
                movedir = 1;
                MoveOnXVisuals();
            }
            else { movedir = 0; }

            if (Input.GetKey(sprintKey)) 
            { 
                Sprint(); 
            }
            else 
            { 
                StopSprint(); 
            }

            if (Input.GetKeyDown(jumpKey))
            {
                Jump();
            }

        }
    }

    private void AnimationMovement() 
    { 
        //rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (rb)
        {
            Animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        }
        Animator.SetBool("Grounded", isGrounded);
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        movementSpeed = lowspeed;
        defaultgravity = rb.gravityScale;
    }

    void Update()
    {
        MoveCommands();
        AnimationMovement();
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            Vector2 velocity = new Vector2(movedir * movementSpeed, rb.velocity.y);
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") | collision.gameObject.CompareTag("Element"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") | collision.gameObject.CompareTag("Element"))
        {
            isGrounded = false;
        }
    }
}
