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
        }
    }
}
