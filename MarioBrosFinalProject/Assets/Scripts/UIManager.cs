using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinsText;  // Referencia al texto de monedas
    public TextMeshProUGUI livesText; // Referencia al texto de vidas
    public TextMeshProUGUI levelText; // Referencia al texto de nivel

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void UpdateHUD(int coins, int lives, int level)
    {
        coinsText.text = "x " + coins;
        livesText.text = "x " + lives;
        levelText.text = "Level " + level;
    }
}