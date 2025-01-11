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

    [SerializeField] private AudioSource themeSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip themeSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip growSound;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip flagSound;
    [SerializeField] private AudioClip pipeSound;
    [SerializeField] private AudioClip killingSound;
    [SerializeField] private AudioClip damageSound;

    private bool isGrounded;
    public bool canMove = true;
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
        themeSource.loop = true;
        themeSource.Play();
        defaultgravity = rb.gravityScale;
        fireBallTimer = Time.time;
        perspective = 1;
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Dead)
        {
            Debug.Log("I lost");
            manager.LooseLife();
            Dead = false;
        }
        else
        {
            if (canMove)
            {
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


                if (Input.GetKeyDown(KeyCode.W))
                {
                    Jump();
                }

                if (rb.velocity.y > 0.1)
                {
                    isGrounded = false;
                }
            }

            if (Attacked)
            {
                Jump();
                Attacked = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isFireMario && Time.time - fireBallTimer > rechargeTime)
            {
                ThrowFire();
                fireBallTimer = Time.time;

            }

            if (rb)
            {
                Animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
            }
            Animator.SetBool("Grounded", isGrounded);
            Animator.SetBool("isBigMario", isBigMario);
            Animator.SetBool("isFireMario", isFireMario);
        }
    }

        

        

    private void FixedUpdate()
    {
        if (rb)
        {
            Vector2 velocity = new Vector2(move*speed, rb.velocity.y);
            rb.velocity = velocity;
        }
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
            audioSource.PlayOneShot(jumpSound);
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Element"))
        {
            isGrounded = true;  // Permite saltar nuevamente cuando Mario toca el suelo
        }
    }

    public void GetHit()
    {

        audioSource.PlayOneShot(damageSound);
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
        canMove = false;
        audioSource.PlayOneShot(deadSound);
        themeSource.Stop();
        Debug.Log("Mori");
        //Eliminar colliders
        Destroy(GetComponent<BoxCollider2D>());

        //Eliminar rigidbody
        Destroy(GetComponent<Rigidbody2D>());

        //Wait one second and continue
        StartCoroutine(DeadAnimation());
    }

    IEnumerator DeadAnimation()
    {
        Animator.SetBool("MarioDead", true);
        yield return new WaitForSeconds(3);
        Dead = true;
        Debug.Log("Dead");
    }

    public void FireMario()
    {
        audioSource.PlayOneShot(growSound);
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
        audioSource.PlayOneShot(growSound);
        isBigMario = true;
    }

    public void PickCoin()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void FlagSound()
    {
        themeSource.Stop();
        audioSource.PlayOneShot(flagSound);
    }

    public void PipeSound()
    {
        audioSource.PlayOneShot(pipeSound);
    }

    public void KillingSound()
    {
        audioSource.PlayOneShot(killingSound);
    }

}