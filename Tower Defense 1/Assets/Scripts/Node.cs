using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color initialColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffsetY = new Vector3(0f, 0.5f, 0f);
    
    private Renderer rend;
    public GameObject objectOnNode;
    public bool isPossibleMineralPos = false;
    
    // create a "TurretBluePrint" class variable to access the cost, upgrade cost and the turret model.
    public TurretBluePrint turretBluePrint;
    public TurretStat turretStat;

    BuildManager buildManager;

    private void Start()
    {
        
        //keep track of mesh renderer
        rend = GetComponent<Renderer>();
        // set the nodes to semi-transparent
        //rend.material.color.a = 0.5f;

        initialColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffsetY;
    }

    private void OnMouseDown()
    {
        // without this code, when hover over the nodes under the UI (turret icon), the turret still gets set.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //Cannot be built
        if (objectOnNode != null)
        {
            buildManager.SelectNode(this);
            rend.material.color = initialColor;
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        //Build turret
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret (TurretBluePrint blueprint)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Insufficient $$");
            return;
        }

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        objectOnNode = _turret;

        //**IMPORTANT
        turretBluePrint = blueprint;

        PlayerStats.Money -= blueprint.cost;
        Debug.Log("Turret built. Money: " + PlayerStats.Money);

        GameObject effectInstance = Instantiate(buildManager.buildEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Insufficient $$");
            return;
        }

        // get rid of the old turret.
        //Destroy(turret);

        //replace the old turret in the same direction.
        //GameObject _turret = Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        //turret = _turret;

        objectOnNode.GetComponent<Turret>().turretStat.level += 1;
        objectOnNode.GetComponent<Turret>().ATK += objectOnNode.GetComponent<Turret>().turretStat.initialATK * 0.2f;
        objectOnNode.GetComponent<Turret>().DPS += objectOnNode.GetComponent<Turret>().turretStat.initialDPS * 0.2f;
        objectOnNode.GetComponent<Turret>().range += 1f;

        PlayerStats.Money -= turretBluePrint.upgradeCost;
        Debug.Log("Turret upgraded. Money: " + PlayerStats.Money);

        GameObject effectInstance = Instantiate(buildManager.buildEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
    }

    public void Sell()
    {
        Destroy(objectOnNode);
        PlayerStats.Money += (int)Mathf.Round((turretBluePrint.cost + (objectOnNode.GetComponent<Turret>().turretStat.level - 1) * turretBluePrint.upgradeCost) / 2);
    }

    // will only be called once when clicked by mouse
    void OnMouseEnter()
    {
        Debug.Log(objectOnNode);
        // if no turret in the shop is selected 
        if (!buildManager.CanBuild)
        {
            return;
        }
        // if enough money
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        // if not enough money
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

        // if there's object on the node
        if (objectOnNode != null)
        {
            rend.material.color = initialColor;
        }

        //keep track of mesh renderer
        // without this code, when hover over the nodes under the UI (turret icon), the turret still gets set.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = initialColor;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rend.material.color = initialColor;
        }
        // it seems strange, but it prevents "Missing object" warning once a mineral was destroyed / a turret was sold.
        if (objectOnNode == null)
        {
            objectOnNode = null;
        }
    }
}
