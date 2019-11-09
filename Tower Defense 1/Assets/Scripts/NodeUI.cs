using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI upgradeCost1;
    public TextMeshProUGUI upgradeCost2;
    public Button upgradeButton;
    public Button targetSelectButton1;
    public Button targetSelectButton2;
    public Button targetSelectButton3;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI targetLockText;
    private Node nodeTarget;

    public void SetTarget(Node _target)
    {
        nodeTarget = _target;
        if (nodeTarget.turret.GetComponent<Turret>().level == nodeTarget.turret.GetComponent<Turret>().maxLevel)
        {
            upgradeCost1.text = "LEVEL";
            upgradeCost2.text = "MAX";
            upgradeButton.interactable = false;
        } else
        {
            upgradeCost1.text = "UPGRADE";
            upgradeCost2.text = "$" + nodeTarget.turretBluePrint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        levelText.text = "Level " + nodeTarget.turret.GetComponent<Turret>().level.ToString();
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        if (nodeTarget.turret.GetComponent<Turret>().level >= nodeTarget.turret.GetComponent<Turret>().maxLevel)
        {
            return;
        }
        nodeTarget.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Close()
    {
        nodeTarget.turret.GetComponent<Turret>().targetSelectionMode = 1;
    }
    public void Hard()
    {
        nodeTarget.turret.GetComponent<Turret>().targetSelectionMode = 2;
    }
    public void Weak()
    {
        nodeTarget.turret.GetComponent<Turret>().targetSelectionMode = 3;
    }
    public void TargetLockMode()
    {
        bool target_Lock = nodeTarget.turret.GetComponent<Turret>().targetLock;
        target_Lock = !target_Lock;
        nodeTarget.turret.GetComponent<Turret>().targetLock = target_Lock;
        if (target_Lock == true)
        {
            targetLockText.text = "TARGET LOCK ON";
        }
        if (target_Lock == false)
        {
            targetLockText.text = "TARGET LOCK OFF";
        }
    }

    public void Sell()
    {
        nodeTarget.Sell();
        BuildManager.instance.DeselectNode();
    }

    private void Update()
    {
        switch(nodeTarget.turret.GetComponent<Turret>().targetSelectionMode)
        {
            case 1:
                targetSelectButton1.GetComponent<Image>().color = new Color(255, 0, 0);
                targetSelectButton2.GetComponent<Image>().color = new Color(255, 189, 76);
                targetSelectButton3.GetComponent<Image>().color = new Color(255, 189, 76);
                break;
            case 2:
                targetSelectButton1.GetComponent<Image>().color = new Color(255, 189, 76);
                targetSelectButton2.GetComponent<Image>().color = new Color(255, 0, 0);
                targetSelectButton3.GetComponent<Image>().color = new Color(255, 189, 76);
                break;
            case 3:
                targetSelectButton1.GetComponent<Image>().color = new Color(255, 189, 76);
                targetSelectButton2.GetComponent<Image>().color = new Color(255, 189, 76);
                targetSelectButton3.GetComponent<Image>().color = new Color(255, 0, 0);
                break;
            default:
                Debug.Log("Target selection mode ERROR");
                break;
        }
    }
}
