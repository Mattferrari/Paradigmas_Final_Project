using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int numberCoins = 0;
    public int numberLives = 5;
    public int minLives = 0;
    public int maxCoins = 100;
    public int level;
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddCoin()
    {
        numberCoins++;

        if (numberCoins == maxCoins)
        {
            numberLives += 1;
            numberCoins = 0;
            // activamos animacion de 1 UP
        }
        UpdateHUD();

    }
    public void LooseLife()
    {
        numberLives--;
        if (numberLives == 0)
        {
            GameOver();
        }
        else { UpdateHUD(); }

    }

    public void NextLevel()
    {
        level++;
        UpdateHUD();
    }


    public void UpdateHUD()
    {
        // Método para actualizar la interfaz del jugador
        // Aquí llamarás a un script del HUD para actualizar texto, imágenes, etc.
        UIManager.instance.UpdateHUD(numberCoins, numberLives, level);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene"); // Escena de Game Over
    }
}
