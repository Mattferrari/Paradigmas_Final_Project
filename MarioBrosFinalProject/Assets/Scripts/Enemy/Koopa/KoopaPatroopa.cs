using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaPatroopa : MonoBehaviour, IEnemy
{
    public PlayerController Mario;
    public Rigidbody2D rb;

    //going up(1) or down(-1)
    private int movedir = 1;
    
    //y range
    [SerializeField] private int maxy;
    [SerializeField] private int miny;

    private float speed = 2f;
    // When koopaPatroopa reaches the extremes of the movement range will stop for 250 ms before changing dir
    private float stopTime = 0.25f;
    private bool isStopped = false;

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
        bool onEdge = PatroopaPosition.y >= maxy || PatroopaPosition.y <= miny;
        if (onEdge && !isStopped)
        {
            StartCoroutine(Stop());
        }
        else if (!isStopped)
        {
            rb.velocity = new Vector2(rb.velocity.x, movedir * speed);
        }
    }

    // function to stop the koopa 
    private IEnumerator Stop()
    {
        isStopped = true;
        yield return new WaitForSeconds(stopTime);
        isStopped = false;
        movedir *= -1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Mario = GetComponent<PlayerController>();
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
