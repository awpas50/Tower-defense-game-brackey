using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;
    private GameObject[] enemies;

    [Header("Target lock mode")]
    public int targetLockMode = 1;

    [Header("General")]
    public float range = 15f;
    public int level;
    public int maxLevel;
    bool isLocked = false;

    [Header("Physical Bullet")]
    public GameObject bulletPrefab;
    private float fireCountdown = 0f;
    public float bulletSpeed = 10f;
    public float fireRate;
    [HideInInspector] public float initialATK;
    public float ATK;

    [Header("Laser Attack")]
    public bool useLaser = false;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light pointLight;

    [HideInInspector] public float initialDPS;
    public float DPS;
    public float slowPercentage;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public TurretBluePrint turretBluePrint;
    public Transform firePoint;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.3f);
        ATK = initialATK;
        DPS = initialDPS;

        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    }

    void UpdateTarget_Close()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        // find out which enemy is the closet one.
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

    void UpdateTarget_Close_LockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float distanceToTarget = Mathf.Infinity;
        GameObject nearestEnemy = null;
        // find out which enemy is the closet one.
        if (!isLocked)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            
            distanceToTarget = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
            
            if (distanceToTarget > range || target == null) // out of range or enemy destroyed
            {
                isLocked = false;
            }
            //isLocked = true;
        }
        else
        {
            target = null;
        }
    }


    void UpdateTarget_Hard()
    {
        
    }

    void UpdateTarget_Weak()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(targetLockMode == 1)
            UpdateTarget_Close();
        if (targetLockMode == 2)
            UpdateTarget_Hard();
        if (targetLockMode == 3)
            UpdateTarget_Weak();
        
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

        AimAtTarget();

        // Laser Attack - Continous attack 
        if(useLaser)
        {
            LaserAttack();
        }
        // Normal Attack - Single bullet
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

    void AimAtTarget()
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
