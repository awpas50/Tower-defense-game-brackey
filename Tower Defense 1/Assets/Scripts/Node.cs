using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color initialColor;
    private Renderer rend;

    private GameObject turret;
    private Vector3 positionOffsetY = new Vector3(0f, 0.5f, 0f);

    private void Start()
    {
        //keep track of mesh renderer
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
    }
    private void OnMouseDown()
    {
        //Cannot be built
        if (turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        //Build turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffsetY, transform.rotation);
    }
    // will only be called once when clicked by mouse
    void OnMouseEnter()
    {
        //keep track of mesh renderer
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        rend.material.color = initialColor;
    }
}
