using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BigTurretManager : MonoBehaviour
{
    public static BigTurretManager instance;

    public GameObject selectedBigNode;
    // memorize 'selectedBigNode'
    private GameObject marker;
    private GameObject previousPos;

    public GameObject bigTurret;
    //stores info about big turret
    private GameObject bigTurretPrefab;
    public Transform bigTurretList;

    public Vector3 positionOffsetY = new Vector3(0f, 0.5f, 0f);

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BigTurretManager in scene");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                Debug.Log(hitInfo.transform.gameObject.name);
                if (hitInfo.collider.tag == "BigNode")
                {
                    selectedBigNode = hitInfo.transform.gameObject;
                }
            }

            if (bigTurretList.childCount == 0 && hitInfo.collider.tag == "BigNode")
            {
                BuildBigTurret();
            }
            if (bigTurretList.childCount == 1 && hitInfo.collider.tag == "BigNode" 
                && GetComponent<BigTurretAbility>().ab4info.isinCD == false && marker != selectedBigNode)
            {
                MoveBigTurret();
                GetComponent<BigTurretAbility>().Ability4Trigger();
            }
            else
            {
                Debug.Log("You can only have 1 big turret / selectedBigNode == null.");
                return;
            }
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffsetY;
    }
    void BuildBigTurret()
    {
        bigTurretPrefab = Instantiate(bigTurret, GetBuildPosition(), Quaternion.identity);
        bigTurretPrefab.transform.parent = bigTurretList;
        bigTurretPrefab.transform.position = selectedBigNode.transform.position;
        // to access NodeUI
        selectedBigNode.GetComponent<Node>().objectOnNode = bigTurretPrefab;
        previousPos = selectedBigNode;
    }
    void MoveBigTurret()
    {
        previousPos.GetComponent<Node>().objectOnNode = null;
        bigTurretPrefab.transform.position = selectedBigNode.transform.position;
        selectedBigNode.GetComponent<Node>().objectOnNode = bigTurretPrefab;
        marker = selectedBigNode;
    }
}
