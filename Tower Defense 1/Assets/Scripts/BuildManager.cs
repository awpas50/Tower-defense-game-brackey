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

    public bool CanBuild { get { return turretToBuild != null; } }
    public void BuildTurretOn(Node node)
    {
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }
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

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
