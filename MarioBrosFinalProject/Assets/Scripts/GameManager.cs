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
    public int level = 1;
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
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

    public void GainLife()
    {
        numberLives++;
        UpdateHUD();
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

    public void RemoveAllEnemies()
    {
        // Obtiene todos los objetos con la etiqueta "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Recorre el array y destruye cada enemigo
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene"); // Escena de Game Over
    }
}
