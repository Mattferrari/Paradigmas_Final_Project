using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleLevelChanger : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You have passed to the next level!");
            SceneManager.LoadScene("MainMenu");
        }
        
        //Camera.transform.position = 
    }
}
