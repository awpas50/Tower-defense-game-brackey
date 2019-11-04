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
        lifeText.text = life.ToString();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        lifeText.text = life.ToString();
    }
}
