using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int originalLives = 5;
    public int numberCoins;
    public int numberLives;
    public int minLives;
    public int maxCoins;
    public int level;
    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Verifica el valor de numberLives al iniciar
        Debug.Log("Número de vidas al iniciar la escena: " + numberLives);


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
        Debug.Log(numberLives);
        numberLives--;
        
        if (numberLives == 0)
        {
            GameOver();
            
        }
        else 
        {
            ReloadLevel();
            UpdateHUD();
        }

    }

    public void GainLife()
    {
        numberLives++;
        Debug.Log("1 mas");
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

    public void SetLevel(int nLevel)
    {
        level = nLevel;
        UpdateHUD();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        numberLives = originalLives;
        numberCoins = 0;
        level = 1;
        UpdateHUD();
        SceneManager.LoadScene("MainMenu"); // Escena de Game Over
    }

    private void ReloadLevel()
    {
        // Recarga la escena actual
        string currentScene = "Level" + level.ToString();
        SceneManager.LoadScene(currentScene);
    }
}
