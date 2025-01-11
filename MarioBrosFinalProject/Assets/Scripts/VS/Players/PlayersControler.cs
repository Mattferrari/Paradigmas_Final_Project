using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayersController : MonoBehaviour
{    
    // objects
    private Animator Animator;
    public PlayerHUDController manager;

    // Enemy
    private PlayersController enemy;
    private BoxCollider2D enemyUpper;
    private BoxCollider2D enemyLower;

    // movement related
    private PlayerMovement movement;
    private bool canMove = true;

    //atack related
    private PlayerAttack attack;

    // colliders
    [SerializeField] private BoxCollider2D upperCollider;
    public BoxCollider2D GetUpperCollider() {  return upperCollider; }
    [SerializeField] private BoxCollider2D lowerCollider;
    public BoxCollider2D GetLowerCollider() { return lowerCollider; }

    // growing params
    // lower Collider:
    // size
    [SerializeField] private float lowerXIncrement = 0.3f;
    [SerializeField] private float lowerYIncrement = 0.65f;
    // Offset
    [SerializeField] private float lowerXOffset = 0f;
    [SerializeField] protected float lowerYOffset = 0.38f;
    // UpperCollider
    // size
    [SerializeField] public float upperXIncrement = 0.2f;
    [SerializeField] public float upperYIncrement = 0.2f;
    // Offset
    [SerializeField] public float upperXOffset = 0f;
    [SerializeField] public float upperYOffset = 0.9f;

    // fields
    public bool Dead = false;
    public bool isBigMario = false;
    public bool isFireMario = false;


    public void GetHit()
    {
        if (isFireMario)
        {
            isFireMario = false;
            attack.SetFireMario(isFireMario);
            isBigMario = true;
        }
        else if (isBigMario)
        {
            isBigMario = false;
            lowerCollider.size = new Vector2(lowerCollider.size.x - lowerXIncrement, lowerCollider.size.y - lowerYIncrement);
            lowerCollider.offset = new Vector2(lowerCollider.offset.x - lowerXOffset, lowerCollider.offset.y - lowerYOffset);

            upperCollider.size = new Vector2(upperCollider.size.x - upperXIncrement, upperCollider.size.y - upperYIncrement);
            upperCollider.offset = new Vector2(upperCollider.offset.x - upperXOffset, upperCollider.offset.y - upperYOffset);

        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        // Destroy colliders
        Destroy(upperCollider);
        Destroy(lowerCollider);

        // Destroy rigidbody
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
        attack.SetFireMario(isFireMario);
        isBigMario = false;
    }

    public void BigMario()
    {
        isBigMario = true;
        lowerCollider.size = new Vector2(lowerCollider.size.x + lowerXIncrement, lowerCollider.size.y + lowerYIncrement);
        lowerCollider.offset = new Vector2(lowerCollider.offset.x - lowerXOffset, lowerCollider.offset.y + lowerYOffset);

        upperCollider.size = new Vector2(upperCollider.size.x + upperXIncrement, upperCollider.size.y + upperYIncrement);
        upperCollider.offset = new Vector2(upperCollider.offset.x + upperXOffset, upperCollider.offset.y + upperYOffset);
    }

    public PlayersController GetEnemy()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject player in players)
        {
            if (player != this.gameObject)
            {
                enemy = player.GetComponent<PlayersController>();
                break;
            }
        }
        return enemy;
    }

    void Start()
    {   
        PlayersController enemy = GetEnemy();

        // get enemy colliders
        enemyUpper = enemy.GetUpperCollider();
        enemyLower = enemy.GetLowerCollider();

        // get Managers
        Animator = GetComponent<Animator>();
        manager = GetComponent<PlayerHUDController>();
        
        // Atack related
        attack = GetComponent<PlayerAttack>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == enemyLower && collision.otherCollider == upperCollider) 
        {
            Debug.Log("colide");
            GetHit();
        }
    }
}