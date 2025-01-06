using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour, IEnemy
{
    //Rigid body
    private Rigidbody2D rb;
    
    //objects
    public PlayerController Mario;
    public Collider2D superiorCollider;
    public Collider2D lowerCollider;
    
    //parameters
    [SerializeField] private int movementDir = -1;
    [SerializeField] private float speed = 2.0f;

    // functions
    // Interface Functions must be public
    public void Move()
    {
        rb.velocity = new Vector2(movementDir * speed, rb.velocity.y);
    }

    public void Attack()
    {
        // function for goomba to Attack mario
        Mario.GetHit();
    }

    public void GetKilled() 
    {
        // function to kill Goomba
        Mario.Attacked = true;
        Destroy(gameObject); 
    }

    private void SetMovementDir() { movementDir *= -1; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Element") || collision.gameObject.CompareTag("Enemy"))
        {
            SetMovementDir();
        }

        if (collision.otherCollider == superiorCollider && collision.gameObject.CompareTag("Player"))
        {
            GetKilled();
        }

        if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
}
