using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves;
    public TextMeshProUGUI countDownText;

    private int _enemyCount;
    private float _timeBetweenEnemies;
    private float countDown;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {   
        // [i]: number of waves
        // [j]: determine which type of enemy to spawn
        while(!GameManager.GameEnded && PlayerStats.waves <= PlayerStats.TotalWave)
        {
            for (int i = 0; i < waves.Length; i++)
            {
                _enemyCount = waves[i].enemyList.Length;
                _timeBetweenEnemies = waves[i].timeBetweenEnemies;
                countDown = waves[i].timeBetweenEnemies * (_enemyCount - 1) + timeBetweenWaves;
                
                for (int j = 0; j < waves[i].enemyList.Length; j++)
                {
                    if (waves[i].enemyList[j] != null)
                    {
                        Instantiate(waves[i].enemyList[j], spawnPoint.position, spawnPoint.rotation);
                    }
                    yield return new WaitForSeconds(waves[i].timeBetweenEnemies);
                }
                yield return new WaitForSeconds(timeBetweenWaves);
                if (PlayerStats.waves == PlayerStats.TotalWave)
                {
                    PlayerStats.waves = PlayerStats.TotalWave;
                    countDownText.enabled = false;
                }
                else if (!GameManager.GameEnded)
                {
                    PlayerStats.waves += 1;
                }
            }
        }
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        countDownText.text = string.Format("{0:00.00}", countDown);
        if(countDown <= 0.1f)
        {
            countDown = _timeBetweenEnemies * (_enemyCount - 1) + timeBetweenWaves;
        }
    }
}
