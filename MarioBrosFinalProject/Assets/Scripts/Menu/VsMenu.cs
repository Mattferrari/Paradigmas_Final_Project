using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class VsMenu : MonoBehaviour
{
    public GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void LoadVs(bool PvP)
    {
        string sceneName = "Vs" + (PvP ? "PVP" : "PVC");
        SceneManager.LoadScene(sceneName);
    }

}
