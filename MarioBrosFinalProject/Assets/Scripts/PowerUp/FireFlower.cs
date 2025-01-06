using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerController Mario;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mario = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Mario.FireMario();
            Destroy(gameObject); // Destruye el GameObject que contiene este script
            Mario.boxCollider.size = new Vector2(Mario.boxCollider.size.x, Mario.boxCollider.size.y * 2);
            Mario.boxCollider.offset = new Vector2(Mario.boxCollider.offset.x, Mario.boxCollider.offset.y + 0.5f);
        }
    }
}
