using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffsetY = new Vector3(0f, 0.5f, 0f);

    [Header("Optional")]
    private Color initialColor;
    private Renderer rend;
    
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
        
    }
    // will only be called once when clicked by mouse
    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }
        //keep track of mesh renderer
        // without this code, when hover over the nodes under the UI (turret icon), the turret still gets set.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            rend.material.color = initialColor;
            return;
        }
        else
        {
            rend.material.color = hoverColor;
        }
    }
    void OnMouseExit()
    {
        rend.material.color = initialColor;
    }
}
