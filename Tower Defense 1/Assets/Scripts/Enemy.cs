using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wayPointIndex = 0;

    public float initialSpeed = 10f;

    [HideInInspector] public float speed;
    public float HP = 50;
    public int worth;

    public GameObject deathEffect;

    //Movement
    void Start()
    {
        target = WayPoints.points[wayPointIndex];
        speed = initialSpeed;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWavePoint();
        }

        speed = initialSpeed;
    }

    void GetNextWavePoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wayPointIndex += 1;
        target = WayPoints.points[wayPointIndex];
    }

    // Player's life
    void EndPath()
    {
        Life.life--;
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if(HP <= 0)
        {
            Die();
        }
    }

    public void SlowDown(float slowPercent)
    {
        speed = initialSpeed * (1 - slowPercent);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        Destroy(gameObject);
        GameObject effectInstance = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
    }
}
