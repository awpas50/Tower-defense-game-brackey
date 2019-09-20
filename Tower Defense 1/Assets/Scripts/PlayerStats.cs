using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int waves = 1;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI waveText;

    void Start()
    {
        waves = 1;
        Money = startMoney;
    }
    void Update()
    {
        moneyText.text = Money.ToString();
        
    }

    private void OnEnable()
    {
        waveText.text = waves.ToString();
    }
}
