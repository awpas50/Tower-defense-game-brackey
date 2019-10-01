using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;
    public float timeBetweenWaves = 4f;
    public float timeBetweenEnemies = 0.6f;
    public int enemyCount;

    

    public TextMeshProUGUI countDownText;
    private float countDown;

    void Start()
    {
        countDown = timeBetweenEnemies * (enemyCount - 1) + timeBetweenWaves;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {   
        while(!GameManager.GameEnded)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            if(!GameManager.GameEnded)
            {
                PlayerStats.waves += 1;
            }
        }
    }

    void Update()
    {

        countDown -= Time.deltaTime;
        countDownText.text = string.Format("{0:00.00}", countDown);
        if(countDown <= 0.1f)
        {
            countDown = timeBetweenEnemies * (enemyCount - 1) + timeBetweenWaves;
        }
    }
}
