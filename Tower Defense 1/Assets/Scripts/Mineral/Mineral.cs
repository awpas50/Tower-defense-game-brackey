using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public GameObject explosionEffect;
    // recognize which node is the mineral standing on
    // this variable will be assigned in "MineralSpawner", "AssignMinerals(int[] r)".
    public GameObject node;

    // Remove the node mark so that turret can be placed on it.
    public void RemoveNodeMark()
    {
        Debug.Log("RemoveNodeMark");
        node = null;
    }
}
