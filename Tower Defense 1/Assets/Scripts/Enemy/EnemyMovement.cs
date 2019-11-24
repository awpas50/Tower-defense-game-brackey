using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform nextWavePoint;
    public int wayPointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        nextWavePoint = WayPoints.points[wayPointIndex];
    }
    void Update()
    {
        // Enemy Movement
        Vector3 dir = nextWavePoint.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, nextWavePoint.position) <= 0.4f)
        {
            GetNextWavePoint();
        }
        enemy.speed = enemy.initialSpeed;
    }
    void GetNextWavePoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wayPointIndex += 1;
        nextWavePoint = WayPoints.points[wayPointIndex];
    }

    // Player's life
    void EndPath()
    {
        Life.life--;
        Destroy(gameObject);
    }
}
