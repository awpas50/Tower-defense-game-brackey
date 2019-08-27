using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return; //debug
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // check if the distance from a bullet to a target is equal to the direction.magnitude
        // if that is less than the distance that we are going to move this frame, that means
        // this bullet already hit an object.
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        // if not yet hit the target:
        // (dir.normalized: remain a constant speed)
        // Space.World: rotate properly
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
