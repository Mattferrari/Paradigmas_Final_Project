using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILooseLifeScene : MonoBehaviour
{
    public TextMeshProUGUI livesText; // Referencia al texto de vidas
    public GameManager gameManager;

    public static UILooseLifeScene instance; // Instancia estática para acceso global

    private void Awake()
    {
        // Configurar la instancia estática
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Se ha detectado más de un UIManager en la escena. Esto puede causar problemas.");
            Destroy(gameObject); // Destruir instancias duplicadas
        }
    }

    void Start()
    {
        // Obtener referencias necesarias
        if (livesText == null)
        {
            livesText = GetComponentInChildren<TextMeshProUGUI>();
        }

        if (gameManager == null)
        {
            GameObject gmObject = GameObject.FindWithTag("GameManager");
            if (gmObject != null)
            {
                gameManager = gmObject.GetComponent<GameManager>();
            }
            else
            {
                Debug.LogError("No se encontró un objeto con el tag 'GameManager'. Asegúrate de que exista en la escena.");
            }
        }

        // Actualizar el texto inicialmente
        UpdateLivesText();
    }

    // Método para actualizar el texto de vidas
    public void UpdateLivesText()
    {
        if (livesText != null && gameManager != null)
        {
            livesText.text = "x " + gameManager.numberLives;
        }
    }
}
