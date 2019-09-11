using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Turret turret;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    // detect which turret shoot this bullet, in order to access the ATK from the "turret" script.
    public void RecognizeTurret(Turret _turret)
    {
        turret = _turret;
    }
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
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
        if(explosionRadius > 0f)
        {
            //splash damage
            SplashDamage();
        }
        else
        {
            // hit one target
            RegularDamage(target);
        }
        Destroy(gameObject);
    }
    void SplashDamage()
    {
        // generate an invisible sphere to detect if any colliders are in the range.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                RegularDamage(collider.transform);
            }
        }
    }
    void RegularDamage (Transform enemyInstance)
    {
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(turret.ATK);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
