using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleLevelChanger : MonoBehaviour
{

    public GameManager gameManager; 
    public GameObject player;
    public GameObject Castle;
    public GameObject Camera;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You have passed to the next level!");
            SceneManager.LoadScene("NextLevelScene");
            gameManager.NextLevel();
            player.transform.position = Castle.transform.position;
        }
        
        //Camera.transform.position = 
    }
}
