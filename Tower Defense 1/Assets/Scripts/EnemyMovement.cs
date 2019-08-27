using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public Transform target;
    private int wayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = WayPoints.points[wayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWavePoint();
        }
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

    void EndPath()
    {
        Life.life--;
        Destroy(gameObject);
    }
}
