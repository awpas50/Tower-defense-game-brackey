using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralColor : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    //public bool repeatable = false;
    float startTime;

    public Light pointLight;
    public float maxIntensity;
    public float minIntensity;
    
    void Start()
    {
        startTime = Time.time;
    }
    
    void Update()
    {
        //if (!repeatable)
        //{
        //    float t = (Time.time - startTime) * speed;
        //    GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        //}
        //else
        //{

        //}
        float t = (Mathf.Sin(Time.time - startTime) * speed);
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        pointLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
    }
}
