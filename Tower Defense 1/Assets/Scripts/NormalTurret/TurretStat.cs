using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretStat
{
    public float range;
    public int level = 1;
    public float turnSpeed;
    [Header("Single bullet")]
    public float initialATK;
    public float fireRate;
    public float bulletSpeed;
    [Header("Continuous attack")]
    public float initialDPS;
    public float slowPercentage;
}
