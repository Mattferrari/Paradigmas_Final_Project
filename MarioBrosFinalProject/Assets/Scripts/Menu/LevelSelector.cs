using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class LevelSelector : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    public void OpenLevel(int levelId)
    {
        //base name of the scene
        string BaseName = "Level";
        //Scene name
        string LevelName = BaseName + levelId;
        gameManager.SetLevel(levelId);
        SceneManager.LoadScene(LevelName);
    }
}
