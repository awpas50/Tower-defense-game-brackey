using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool GameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Debug.Log("GameOver");
    }
}
