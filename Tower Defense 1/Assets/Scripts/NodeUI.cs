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
    public TextMeshProUGUI levelText;
    private Node nodeTarget;

    public void SetTarget(Node _target)
    {
        nodeTarget = _target;
        transform.position = nodeTarget.GetBuildPosition();
        if (nodeTarget.turretProperties.level == nodeTarget.turretProperties.maxLevel)
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
        levelText.text = "Level " + nodeTarget.turretProperties.level.ToString();
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        if (nodeTarget.turretProperties.level >= nodeTarget.turretProperties.maxLevel)
        {
            return;
        }
        nodeTarget.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        nodeTarget.Sell();
        BuildManager.instance.DeselectNode();
    }
}
