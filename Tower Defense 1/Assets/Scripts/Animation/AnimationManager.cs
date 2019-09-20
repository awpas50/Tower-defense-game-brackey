using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator shopAnim;

    void Start()
    {
        shopAnim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(GameManager.GameEnded)
        {
            Debug.Log("GameOver, play animation");
            shopAnim.enabled = true;
            shopAnim.Play("Shop");
        } else
        {
            shopAnim.enabled = false;
        }
    }
}
