using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileTurret;
    public TurretBluePrint laserTurret;

    public Text shopText1;
    public Text shopText2;
    public Text shopText3;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
        shopText1.text = "$" + standardTurret.cost;
        shopText2.text = "$" + missileTurret.cost;
        shopText3.text = "$" + laserTurret.cost;
    }
    
    public void SelectStandardTurret()
    {
        Debug.Log("Purchased Turret 1");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissileTurret()
    {
        Debug.Log("Purchased Turret 2");
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void SelectLaserTurret()
    {
        Debug.Log("Purchased Turret 3");
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
