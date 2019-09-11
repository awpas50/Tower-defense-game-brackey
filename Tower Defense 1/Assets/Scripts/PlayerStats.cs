using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public TextMeshProUGUI moneyText;

    void Start()
    {
        Money = startMoney;
    }
    void Update()
    {
        moneyText.text = Money.ToString();
    }
}
