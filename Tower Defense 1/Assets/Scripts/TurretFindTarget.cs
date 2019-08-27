using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFindTarget : MonoBehaviour
{
    public float searchRadius = 10f;  // eyesight distance of turret
    public int shootRate = 10;  // number of "looks" per second when not targetting yet
    public float rotationSpeed = 45;  // number of degrees-per-second of rotation when searching

    Transform target;  // the enemy to follow, initially null of course
    float timeToShoot;

    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        timeToShoot = 1 / shootRate;
        target = null;
    }

    void Update()
    {
        Debug.DrawRay(firePoint.position, firePoint.TransformDirection(Vector3.forward) * searchRadius);
        if (target == null) {
            transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        }
        if(target != null)
        {
            Debug.Log("Spotted target");
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            if (timeToShoot <= 0)
            {
                timeToShoot = 1 / shootRate;
                Shoot();
            }
            timeToShoot -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //timeToShoot -= Time.fixedDeltaTime;
        //if (target != null && timeToShoot <= 0)
        //{
        //    timeToShoot = 1 / shootRate;
        //    Shoot();
        //}
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, Vector3.forward, out hit, searchRadius))
        {
            if (hit.collider.tag == "Enemy") {
                target = hit.transform;
                
                GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                bullet.Seek(target);
                //if (bullet != null)
                //{
                    
                //}
            }
            // obviously in this code, you need to tag all your targettable objects with "Enemy"
        }
    }
}
