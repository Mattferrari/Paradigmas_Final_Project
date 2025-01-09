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
    [SerializeField] private bool canMove = true;
    
    // X-movement related
    // direction
    [SerializeField] private int perspective = 1;
    [SerializeField] private int movedir = 0;
    // speed
    private float movementSpeed;
    [SerializeField] private float lowspeed = 5f;
    [SerializeField] private float maxspeed = 8f;

    // jumping related
    private bool isGrounded;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float highJumpMinPressingTime = 1.5f;
    [SerializeField] private float jumpStartTimer = 0f;
    [SerializeField] private float defaultgravity;
    [SerializeField] private float jumpForce = 15.0f;
    
    [SerializeField] private bool Attacked = false;


    // canMove Setter
    public void SetCanMove(bool canmove) { canMove = canmove;  }

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

    private void StopJumping()
    {
        isJumping = false;
        jumpStartTimer = 0f;
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
    
    private void AtackPushback()
    {
        Attacked = true;
        Jump();
        Attacked = false;
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
            isGrounded = true;  // Permite saltar nuevamente cuando Mario toca el suelo
        }
    }
}
