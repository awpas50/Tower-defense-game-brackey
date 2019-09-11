using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color initialColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffsetY = new Vector3(0f, 0.5f, 0f);
    
    private Renderer rend;
    public GameObject buildEffect;

    [Header("Optional")]
    public GameObject turret;
    
    BuildManager buildManager;

    private void Start()
    {
        //keep track of mesh renderer
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffsetY;
    }

    private void OnMouseDown()
    {
        // without this code, when hover over the nodes under the UI (turret icon), the turret still gets set.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }
        //Cannot be built
        if (turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        //Build turret
        buildManager.BuildTurretOn(this);
        GameObject effectInstance = Instantiate(buildEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

    }
    // will only be called once when clicked by mouse
    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        //keep track of mesh renderer
        // without this code, when hover over the nodes under the UI (turret icon), the turret still gets set.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = initialColor;
    }
}
