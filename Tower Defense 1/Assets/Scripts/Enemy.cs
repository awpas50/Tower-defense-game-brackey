using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wayPointIndex = 0;

    public float initialSpeed = 10f;
    public float initialHP = 50;

    [HideInInspector] public float speed;
    [HideInInspector] public float HP;
    public int worth;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    //Movement
    void Start()
    {
        target = WayPoints.points[wayPointIndex];
        speed = initialSpeed;
        HP = initialHP;
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
        healthBar.fillAmount = HP / initialHP;
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
