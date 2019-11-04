using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BigTurretAbility : MonoBehaviour
{
    public AbilityInfo ab1info;
    public AbilityInfo ab2info;
    public AbilityInfo ab3info;
    public AbilityInfo ab4info;
    private AbilityInfo[] allInfo;

    public void Ability1Click()
    {
        Debug.Log("Ability 1");
        ab1info.LoadingBar.enabled = true;
        ab1info.timer = ab1info.CD;
        ab1info.LoadingBar.fillAmount = 0;
        ab1info.button.interactable = false;
        ab1info.isinCD = true;
        ab1info.textBox.text = ab1info.CD.ToString();
        ab1info.textBox.enabled = true;
    }
    public void Ability2Click()
    {
        Debug.Log("Ability 2");
        ab2info.LoadingBar.enabled = true;
        ab2info.timer = ab2info.CD;
        ab2info.LoadingBar.fillAmount = 0;
        ab2info.button.interactable = false;
        ab2info.isinCD = true;
        ab2info.textBox.text = ab2info.CD.ToString();
        ab2info.textBox.enabled = true;
    }
    public void Ability3Click()
    {
        Debug.Log("Ability 3");
        ab3info.LoadingBar.enabled = true;
        ab3info.timer = ab3info.CD;
        ab3info.LoadingBar.fillAmount = 0;
        ab3info.button.interactable = false;
        ab3info.isinCD = true;
        ab3info.textBox.text = ab3info.CD.ToString();
        ab3info.textBox.enabled = true;
    }

    public void Ability1Trigger()
    {
        Debug.Log("Triggered Ability 1");
        ab1info.LoadingBar.enabled = true;
        ab1info.timer = ab1info.CD;
        ab1info.LoadingBar.fillAmount = 0;
        ab1info.button.interactable = false;
        ab1info.isinCD = true;
        ab1info.textBox.text = ab1info.CD.ToString();
        ab1info.textBox.enabled = true;
    }
    public void Ability2Trigger()
    {
        Debug.Log("Triggered Ability 2");
        ab2info.LoadingBar.enabled = true;
        ab2info.timer = ab2info.CD;
        ab2info.LoadingBar.fillAmount = 0;
        ab2info.button.interactable = false;
        ab2info.isinCD = true;
        ab2info.textBox.text = ab2info.CD.ToString();
        ab2info.textBox.enabled = true;
    }
    public void Ability3Trigger()
    {
        Debug.Log("Triggered Ability 3");
        ab3info.LoadingBar.enabled = true;
        ab3info.timer = ab3info.CD;
        ab3info.LoadingBar.fillAmount = 0;
        ab3info.button.interactable = false;
        ab3info.isinCD = true;
        ab3info.textBox.text = ab3info.CD.ToString();
        ab3info.textBox.enabled = true;
    }
    public void Ability4Trigger()
    {
        ab4info.LoadingBar.enabled = true;
        ab4info.timer = ab4info.CD;
        ab4info.LoadingBar.fillAmount = 0;
        ab4info.button.interactable = false;
        ab4info.isinCD = true;
        ab4info.textBox.text = ab4info.CD.ToString();
        ab4info.textBox.enabled = true;
    }

    public void Start()
    {
        allInfo = new AbilityInfo[4] { ab1info, ab2info, ab3info, ab4info };
        for (int i = 0; i < allInfo.Length; i++)
            allInfo[i].textBox.enabled = false;
    }

    public void Update()
    {
        for (int i = 0; i < allInfo.Length; i++)
        {
            if (allInfo[i].LoadingBar.fillAmount < 1)
            {
                allInfo[i].LoadingBar.enabled = true;
                allInfo[i].timer -= Time.deltaTime;
                allInfo[i].LoadingBar.fillAmount += Time.deltaTime / allInfo[i].CD;
                if(allInfo[i].timer <= 1)
                    allInfo[i].textBox.text = string.Format("{0:0.0}", allInfo[i].timer);
                else
                    allInfo[i].textBox.text = string.Format("{0:0}", allInfo[i].timer);
            }
            else if (allInfo[i].LoadingBar.fillAmount == 1)
            {
                allInfo[i].LoadingBar.enabled = false;
                allInfo[i].textBox.enabled = false;
                allInfo[i].button.interactable = true;
                allInfo[i].isinCD = false;
            }
        }
    }
}
