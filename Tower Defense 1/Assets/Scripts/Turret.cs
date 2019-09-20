using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Physical Bullet")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float bulletSpeed = 10f;
    public int ATK;

    [Header("Laser Attack")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light pointLight;

    public int DPS;
    public float slowPercentage;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.3f);

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if(useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                pointLight.enabled = false;
                impactEffect.Stop();
            }
            return;
        }
        LockOnTarget();

        if(useLaser)
        {
            LaserAttack();
        }
        else if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
            // as the bullet needs to take the variable in the "Turret" script, the bullet needs to recongize which turret shoots it.
            bullet.RecognizeTurret(this);
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Quaternion.LookRoation: instance lock a new target
        //Quaternion.Lerp: lock a target (from A to B) with a speed
        //Therefore we use Quaternion.Lerp here.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //only rotate Y-axis
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void LaserAttack()
    {
        // deals damage
        targetEnemy.TakeDamage(DPS * Time.deltaTime);
        // slow enemies
        targetEnemy.SlowDown(slowPercentage);
        // visual effects
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            // lighting
            pointLight.enabled = true;
            impactEffect.Play();
        }
        // laser beam
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;

        //particle effect
        impactEffect.transform.position = target.position + dir.normalized * 0.5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
