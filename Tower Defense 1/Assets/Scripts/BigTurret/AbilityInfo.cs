using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class AbilityInfo
{
    public Image LoadingBar;
    public float CD;
    public float timer;
    public TextMeshProUGUI textBox;
    public Button button;
    public bool isinCD = false;
}
