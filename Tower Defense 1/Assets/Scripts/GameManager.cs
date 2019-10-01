using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameEnded = false;
    public GameObject gameOverUI;

    void Awake()
    {
        GameEnded = false;
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if(GameEnded)
        {
            return;
        }
        if(Life.life <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {

    }
}
