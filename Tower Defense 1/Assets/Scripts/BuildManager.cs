using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //static: this variable needs to be shard by all build managers
    // only wants one build manager
    // a "BuildManager" inside the BuildManager
    //stores a reference to itself
    public static BuildManager instance;
    private TurretBluePrint turretToBuild;

    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject LaserTurretPrefab;

    public GameObject buildEffect;
    public Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    void Awake()
    {
        //debug
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public void SelectNode(Node node)
    {
        //if (UI.activeInHierarchy == true)
        //{
        //    nodeUI.Hide();
        //}
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        //where one is active, than disable another selection
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    void Update()
    {
        //right click
        if (Input.GetMouseButtonDown(1))
        {
            DeselectNode();
            turretToBuild = null;
        }
    }
}
