using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerController Mario;

    public bool goingLeft = false;
    public float speed = 2.0f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mario = GameObject.Find("Player").GetComponent<PlayerController>();
        goingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (goingLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Element"))
        {
            if (goingLeft)
            {
                goingLeft = false;
            }
            else
            {
                goingLeft = true;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye el GameObject que contiene este script
            if (!Mario.isFireMario)
            {
                Mario.boxCollider.size = new Vector2(Mario.boxCollider.size.x, Mario.boxCollider.size.y * 2);
                Mario.boxCollider.offset = new Vector2(Mario.boxCollider.offset.x, Mario.boxCollider.offset.y + 0.5f);
            }
            
        }
    }

    public void SetMario(PlayerController player)
    {
        Mario = player;
    }
}
