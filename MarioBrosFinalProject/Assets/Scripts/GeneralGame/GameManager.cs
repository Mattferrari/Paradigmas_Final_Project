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
    public GameObject UImanager;
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
        Debug.Log(numberLives);
        
        if (numberLives == 0)
        {
            GameOver();
            
        }
        else 
        {
            StartCoroutine(LooseLifeScene());
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
        // M�todo para actualizar la interfaz del jugador
        // Aqu� llamar�s a un script del HUD para actualizar texto, im�genes, etc.
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
        numberLives = originalLives;
        numberCoins = 0;
        level = 1;
        UpdateHUD();
        StartCoroutine(GameOverScene());
    }

    private void ReloadLevel()
    {
        // Recarga la escena actual
        string currentScene = "Level" + level.ToString();

        SceneManager.LoadScene(currentScene);
    }

    private IEnumerator LooseLifeScene()
    {
        // Guardar el nombre de la escena actual
        string currentScene = SceneManager.GetActiveScene().name;

        // Cargar la escena de "Game Over"
        UImanager.SetActive(false);
        SceneManager.LoadScene("LooseLife");

        // Esperar 2 segundos
        yield return new WaitForSeconds(2);

        // Regresar a la escena original
        
        SceneManager.LoadScene(currentScene);
        UImanager.SetActive(true);
    }

    private IEnumerator GameOverScene()
    {
        UImanager.SetActive(false);
        SceneManager.LoadScene("GameOver");

        // Esperar 2 segundos
        yield return new WaitForSeconds(3);

        // Regresar a la escena original

        SceneManager.LoadScene("MainMenu");
        UImanager.SetActive(true);
    }
}
