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
    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;

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
    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
