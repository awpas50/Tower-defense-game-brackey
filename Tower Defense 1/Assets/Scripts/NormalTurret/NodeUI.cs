using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI TurretNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI upgradeCost1;
    public TextMeshProUGUI upgradeCost2;

    public Button upgradeButton;
    public Button targetSelectButton1;
    public Button targetSelectButton2;
    public Button targetSelectButton3;
    public TextMeshProUGUI targetLockText;
    public TextMeshProUGUI sellText;
    private Node nodeTarget;
    private Turret selectedTurret_Script;

    //this will be called in "BuildManager"
    public void SetTarget(Node _target)
    {
        nodeTarget = _target;
        // to simplify the name
        selectedTurret_Script = nodeTarget.objectOnNode.GetComponent<Turret>();
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        if (selectedTurret_Script.turretStat.level >= selectedTurret_Script.turretStat.maxLevel)
        {
            return;
        }
        nodeTarget.UpgradeTurret();
        //BuildManager.instance.DeselectNode();
    }
    public void Close()
    {
        selectedTurret_Script.targetSelectionMode = 1;
    }
    public void Hard()
    {
        selectedTurret_Script.targetSelectionMode = 2;
    }
    public void Weak()
    {
        selectedTurret_Script.targetSelectionMode = 3;
    }
    public void TargetLockMode()
    {
        selectedTurret_Script.targetLock = !selectedTurret_Script.targetLock;
    }
    public void Sell()
    {
        nodeTarget.Sell();
        BuildManager.instance.DeselectNode();
    }

    void Start()
    {
        Hide();
    }
    void Update()
    {
        if (nodeTarget != null)
        {
            // Turret name(Text)
            TurretNameText.text = selectedTurret_Script.turretStat.turretName;

            // Upgrade(Button)
            if(PlayerStats.Money < selectedTurret_Script.turretStat.upgradeCost)
            {
                upgradeButton.interactable = false;
            } else {
                upgradeButton.interactable = true;
            }

            if (selectedTurret_Script.turretStat.level == selectedTurret_Script.turretStat.maxLevel)
            {
                upgradeCost1.text = "LEVEL MAX";
                upgradeButton.interactable = false;
            } else {
                upgradeCost1.text = "UPGRADE " + "$" + nodeTarget.turretBluePrint.upgradeCost.ToString();
                upgradeButton.interactable = true;
            }

            // Level(Text)
            levelText.text = "Level " + selectedTurret_Script.turretStat.level;

            // Damage(Text)
            if (selectedTurret_Script.useLaser)
            {
                dmgText.text = "DPS: " + selectedTurret_Script.DPS;
            } else {
                dmgText.text = "Dmg: " + selectedTurret_Script.ATK;
            }

            // Range(Text)
            rangeText.text = "Range: " + selectedTurret_Script.range;

            // Sell(Button)
            sellText.text = "Sell $" + Mathf.Round((selectedTurret_Script.turretStat.cost + 
                                                    (selectedTurret_Script.turretStat.level - 1) * selectedTurret_Script.turretStat.upgradeCost) / 2);

            // Close(Button) Hard(Button) Weak(Button)
            switch (selectedTurret_Script.targetSelectionMode)
            {
                case 1:
                    targetSelectButton1.GetComponent<Image>().color = new Color(0.8f, 0, 0); //red
                    targetSelectButton2.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f); //orange
                    targetSelectButton3.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f);
                    break;
                case 2:
                    targetSelectButton1.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f);
                    targetSelectButton2.GetComponent<Image>().color = new Color(0.8f, 0, 0);
                    targetSelectButton3.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f);
                    break;
                case 3:
                    targetSelectButton1.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f);
                    targetSelectButton2.GetComponent<Image>().color = new Color(1f, 0.7f, 0.3f);
                    targetSelectButton3.GetComponent<Image>().color = new Color(0.8f, 0, 0);
                    break;
                default:
                    Debug.Log("Target selection mode ERROR");
                    break;
            }

            // TargetLockMode(Button)
            if (selectedTurret_Script.targetLock == true)
            {
                targetLockText.text = "TARGET LOCK ON";
            }
            if (selectedTurret_Script.targetLock == false)
            {
                targetLockText.text = "TARGET LOCK OFF";
            }
        }
    }
}
