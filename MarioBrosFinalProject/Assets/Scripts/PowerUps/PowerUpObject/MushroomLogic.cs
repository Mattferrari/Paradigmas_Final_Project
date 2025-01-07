using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLogic : PowerUp
{
    private Rigidbody2D rb;
    public PlayerController Mario;

    private Animator Animator;

    public bool goingLeft = false;
    public float speed = 2.0f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD:MarioBrosFinalProject/Assets/Scripts/GoombaMovement.cs
        Animator = GetComponent<Animator>();
=======
        //Mario = GameObject.Find("Player").GetComponent<PlayerController>();
>>>>>>> b9a72b5732971d0dd05713d4c78bb707e5efcdef:MarioBrosFinalProject/Assets/Scripts/PowerUps/PowerUpObject/MushroomLogic.cs
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
<<<<<<< HEAD:MarioBrosFinalProject/Assets/Scripts/GoombaMovement.cs
            Mario.Attacked = true;
            Destroy(gameObject, 1f); // Destruye el GameObject que contiene este script
            Animator.SetTrigger("GoombaDead");
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            speed = 0;
        }

        if (collision.otherCollider == lowerCollider && collision.gameObject.CompareTag("Player"))
        {
            Mario.GetHit();
=======
            Destroy(gameObject); // Destruye el GameObject que contiene este script
            if (!Mario.isFireMario)
            {
                Mario.boxCollider.size = new Vector2(Mario.boxCollider.size.x, Mario.boxCollider.size.y * 2);
                Mario.boxCollider.offset = new Vector2(Mario.boxCollider.offset.x, Mario.boxCollider.offset.y + 0.5f);
                Mario.isBigMario = true;
            }
            
>>>>>>> b9a72b5732971d0dd05713d4c78bb707e5efcdef:MarioBrosFinalProject/Assets/Scripts/PowerUps/PowerUpObject/MushroomLogic.cs
        }
    }

    public override void SetMario(PlayerController player)
    {
        // Configuración específica para el hongo
        Mario = player; // Ejemplo: el hongo le da una vida al jugador
    }
}
