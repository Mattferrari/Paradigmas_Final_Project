using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILooseLifeScene : MonoBehaviour
{
    public TextMeshProUGUI livesText; // Referencia al texto de vidas
    public GameManager gameManager;

    public static UILooseLifeScene instance; // Instancia est�tica para acceso global

    private void Awake()
    {
        // Configurar la instancia est�tica
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Se ha detectado m�s de un UIManager en la escena. Esto puede causar problemas.");
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
                Debug.LogError("No se encontr� un objeto con el tag 'GameManager'. Aseg�rate de que exista en la escena.");
            }
        }

        // Actualizar el texto inicialmente
        UpdateLivesText();
    }

    // M�todo para actualizar el texto de vidas
    public void UpdateLivesText()
    {
        if (livesText != null && gameManager != null)
        {
            livesText.text = "x " + gameManager.numberLives;
        }
    }
}
