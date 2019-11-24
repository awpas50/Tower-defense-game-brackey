using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretStat
{
    public string turretName;
    public float initialRange;
    public int level = 1;
    public int maxLevel;
    public float turnSpeed;
    [Header("Single bullet")]
    public float initialATK;
    public float fireRate;
    public float bulletSpeed;
    [Header("Continuous attack")]
    public float initialDPS;
    public float slowPercentage;
    [Header("Shop")]
    public float cost;
    public float upgradeCost;
    [HideInInspector] public float sellCost;
}
