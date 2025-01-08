using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaPatroopa : MonoBehaviour, IEnemy
{
    private PlayerController Mario;
    private Rigidbody2D rb;

    //going up(1) or down(-1)
    private int movedir = 1;
    
    //y range
    [SerializeField] private int maxy;
    [SerializeField] private int miny;

    private float speed = 2f;

    public Collider2D superiorCollider;
    public Collider2D lowerCollider;

    public void Atack()
    {

    }
    
    public void GetKilled()
    {

    }
    
    public void Move()
    {
        Vector2 PatroopaPosition = transform.position;
        if (PatroopaPosition.y >= maxy)
        {
            movedir = -1;
        }
        else if (PatroopaPosition.y <= miny)
        {
            movedir = 1;
        }
        rb.velocity = new Vector2(rb.velocity.x, movedir * speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        Mario = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
