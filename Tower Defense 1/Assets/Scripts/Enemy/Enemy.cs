using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int enemyID;

    public float initialHP;
    [HideInInspector] public float HP;
    public float initialSpeed;
    [HideInInspector] public float speed;

    public int worth;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    //Movement
    void Start()
    {
        HP = initialHP;
    }

    void Update()
    {
        // Enemy health color
        if(healthBar != null)
        {
            if (healthBar.fillAmount >= 0.50 && healthBar.fillAmount <= 1)
            {
                healthBar.color = Color.green;
            }
            if (healthBar.fillAmount >= 0.30 && healthBar.fillAmount <= 0.49)
            {
                healthBar.color = Color.yellow;
            }
            if (healthBar.fillAmount >= 0 && healthBar.fillAmount <= 0.29)
            {
                healthBar.color = Color.red;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
        if (healthBar != null)
            healthBar.fillAmount = HP / initialHP;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void SlowDown(float pct)
    {
        speed = initialSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        Destroy(gameObject);
        GameObject effectInstance = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);
    }
}
