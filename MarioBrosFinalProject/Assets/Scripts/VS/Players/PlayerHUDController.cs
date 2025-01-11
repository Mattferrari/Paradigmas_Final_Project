using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHUDController : MonoBehaviour
{
    public int initialLives = 5;
    public int numberCoins;
    public int numberLives;
    public int minLives;
    public int maxCoinsToLevelUp;

    public void AddCoin()
    {
        numberCoins++;

        if (numberCoins == maxCoinsToLevelUp)
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
        else
        {
            ReloadBoard();
            UpdateHUD();
        }

    }

    public void GainLife()
    {
        numberLives++;
        Debug.Log("1 mas");
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        // Last Parameter is adventuremode
        UIManager.instance.UpdateHUD(numberCoins, numberLives, 1);
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
        numberLives = initialLives;
        numberCoins = 0;
        UpdateHUD();
        SceneManager.LoadScene("MainMenu"); // Escena de Game Over
    }

    private void ReloadBoard()
    {
        // Recarga la escena actual
        string currentScene = "VsPVP";
        SceneManager.LoadScene(currentScene);
    }
}
