using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public GameObject explosionEffect;
    // recognize which node is the mineral standing on
    // this variable will be assigned in "MineralSpawner", "AssignMinerals(int[] r)".
    public GameObject node;

    private void Update()
    {
        if (GetComponent<Enemy>().HP <= 0)
        {
            RemoveNodeMark();
        }
    }

    // Remove the node mark so that turret can be placed on it.
    void RemoveNodeMark()
    {
        node = null;
    }
}
