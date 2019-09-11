using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Life : MonoBehaviour
{
    public static int life;
    public int initialLives = 20;

    public TextMeshProUGUI lifeText;

    public Transform END;
    public Transform enemyPrefab;
    void Start()
    {
        life = initialLives;
        lifeText.text = "Life: " + life.ToString();
    }

    void Update()
    {
        UpdateUI();
        if (life <= 0)
        {
            lifeText.text = "GAME OVER";

        }
    }

    private void UpdateUI()
    {
        lifeText.text = "Life: " + life.ToString();
    }
}
