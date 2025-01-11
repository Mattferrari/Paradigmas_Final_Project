using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    private PlayerController Mario;

    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Mario.PickCoin();
            Destroy(gameObject); // Destruye el GameObject que contiene este script
            GameManager.instance.AddCoin(); 
        }
    }
}
