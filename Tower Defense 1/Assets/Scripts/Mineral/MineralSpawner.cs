using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    public GameObject mineral;
    private int mineralNumber;
    public int mineralNumberMin;
    public int mineralNumberMax;

    public Transform mineralPosList;
    private Transform[] mineralPos;
    private GameObject[] mineralPos_Node;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    // ----------------------
    void Awake()
    {
        GetMineralPosList();
    }

    void Start()
    {
        RaycastRecognization();
        int[] r = GetRandomMineralPos(mineralPos_Node.Length, mineralNumberMin, mineralNumberMax);
        AssignMinerals(r);
    }

    // ------ Function ------
    void GetMineralPosList()
    {
        // Get a ref of the mineral position (the white dots)
        mineralPos = new Transform[mineralPosList.childCount];
        // Get a ref of the mineral position (the actual node)
        mineralPos_Node = new GameObject[mineralPos.Length];
        // Note max is exclusive. For example, Random.Range(0, 10) can return a value between 0 and 9
        // mineralNumberMax will be + 1
        mineralNumber = Random.Range(mineralNumberMin, mineralNumberMax + 1);
        // a ref of list is needed (mineralPosList)
        // Add possible position of minerals into the array (mineralPos[i])
        for (int i = 0; i < mineralPosList.childCount; i++)
        {
            mineralPos[i] = mineralPosList.GetChild(i);
        }
    }

    void RaycastRecognization()
    {
        for (int i = 0; i < mineralPos.Length; i++)
        {
            RaycastHit hit;
            // at each possible mineral position, project a ray downward to detect the nodes.
            if (Physics.Raycast(mineralPos[i].position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                // if a node is detected,
                if (hit.transform.GetComponent<Node>())
                {
                    mineralPos_Node[i] = hit.transform.gameObject;
                    // the node will be marked as a possible mineral spawn location 
                    hit.transform.GetComponent<Node>().isPossibleMineralPos = true;
                    // the node will be stored in a new array
                }
            }
        }
    }

    public int[] GetRandomMineralPos(int length, int min, int max)
    {
        // generate non-repetitive numbers
        // Debug.Log("mineralPos_Node.Length: " + mineralPos_Node.Length);
        int[] numList = new int[length];
        //min: inclusive; max: exclusive
        int[] result = new int[Random.Range(min, max + 1)];
        //Add numbers into the list (from 0 to the size of possible mineral location (int length)).
        // int[] numList = new int[] {0, 1, 2, .... length};
        for (int i = 0; i < length; i++)
        {
            numList[i] = i;
        }
        for(int i = 0; i < result.Length; i++)
        {
            int id = Random.Range(0, length);
            result[i] = numList[id];
            numList[id] = numList[length - 1];
            length--;
        }
        // return a random list of number into the array "result"
        return result;
    }

    void AssignMinerals(int[] r)
    {
        // assign minerals on specific locations
        for (int i = 0; i < mineralNumber; i++)
        {
            Debug.Log(r[i]);
            GameObject mineralPrefab = Instantiate(mineral, mineralPos_Node[r[i]].transform.position + offset, Quaternion.identity);
            // assign the mineral on the node so that turret cannot be placed on it (unless it has been destroyed)
            mineralPos_Node[r[i]].GetComponent<Node>().objectOnNode = mineralPrefab;
            mineralPrefab.GetComponent<Mineral>().node = mineralPos_Node[r[i]];
        }
    }
}
