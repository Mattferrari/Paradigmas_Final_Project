using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    public PlayerController Mario;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye el GameObject que contiene este script
            GameManager.instance.AddCoin(); 
        }
    }
}
