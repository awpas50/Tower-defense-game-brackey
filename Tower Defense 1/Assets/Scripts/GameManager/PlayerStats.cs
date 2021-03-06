﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney;

    public static int waves = 1;
    public static int TotalWave;
    public int totalWave;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI survivedWaveText;
    public TextMeshProUGUI waveText;

    void Awake()
    {
        waves = 1;
        Money = startMoney;
        TotalWave = totalWave;
    }

    void Update()
    {
        moneyText.text = Money.ToString();
        survivedWaveText.text = waves.ToString();
        waveText.text = "Wave " + waves + " / " + totalWave;
    }

    private void OnEnable()
    {
        //Debug.Log(waves);
        survivedWaveText.text = waves.ToString();
    }
}
