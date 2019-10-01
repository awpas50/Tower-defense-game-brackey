using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney;

    public static int waves = 1;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI waveText;

    void Awake()
    {
        waves = 1;
        Money = startMoney;
    }
    void Update()
    {
        moneyText.text = Money.ToString();
        waveText.text = waves.ToString();
    }

    private void OnEnable()
    {
        Debug.Log(waves);
        waveText.text = waves.ToString();
    }
}
