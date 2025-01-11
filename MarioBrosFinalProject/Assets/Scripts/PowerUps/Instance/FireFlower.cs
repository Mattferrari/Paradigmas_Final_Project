using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : PowerUp
{
    private Rigidbody2D rb;
    public PlayerController Mario;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Mario = GameObject.FindWithTag("Player").GetComponent<PlayerControler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Mario.isBigMario)
            {
                Mario.boxCollider.size = new Vector2(Mario.boxCollider.size.x, Mario.boxCollider.size.y * 2);
                Mario.boxCollider.offset = new Vector2(Mario.boxCollider.offset.x, Mario.boxCollider.offset.y + 0.5f);
            }
            Mario.FireMario();
            Destroy(gameObject); // Destruye el GameObject que contiene este script
        }
    }
    public override void SetMario(PlayerController player)
    {
        Mario = player;
    }
}
