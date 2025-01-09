using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // movement related
    private PlayerMovement movement;
    private bool canMove = true;

    public bool Dead = false;

    private Animator Animator;

    public BoxCollider2D boxCollider;

    public bool isBigMario = false;
    public bool isFireMario = false;
    public float fireBallTimer;
    public float rechargeTime;

    public GameObject fireBall;

    public GameManager manager;


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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        fireBallTimer = Time.time;
        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        
        //PlayerMovement related
        movement = GetComponent<PlayerMovement>();
        movement.SetCanMove(canMove);
    }

    void Update()
    {
        if (Dead)
        {
            manager.LooseLife();
        }
        Animator.SetBool("isBigMario", isBigMario);
        Animator.SetBool("isFireMario", isFireMario);
    }

    public void Die()
    {
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
        yield return new WaitForSeconds(2);
        canMove = false;
        Dead = true;
    }

    public void FireMario()
    {
        isFireMario = true;
        isBigMario = false;
    }

    public void BigMario()
    {
        isBigMario = true;
    }

}