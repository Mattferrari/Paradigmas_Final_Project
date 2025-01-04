using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleFlag : MonoBehaviour
{
    public Collider2D circleCollider;
    public GameManager gameManager;
    private bool levelPassed = false;
    public Flag flag;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!levelPassed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameManager.RemoveAllEnemies();
                collision.gameObject.transform.position += new Vector3(1, 0, 0);
                flag.PullDown();
            }
            if (collision.otherCollider == circleCollider)
            {
                gameManager.GainLife();
                // Imagen 1UP
            }
            levelPassed = true;
        }
        
    }
}
