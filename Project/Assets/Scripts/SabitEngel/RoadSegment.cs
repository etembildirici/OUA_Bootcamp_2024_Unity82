using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    public GameObject[] fixedObstacles; // Sabit engel prefab'ları
    public float obstacleProbability = 0.2f; // Engel yerleştirme olasılığı (%20)

    private void Start()
    {
        PlaceObstacles();
    }

    public void PlaceObstacles()
    {
        // Sabit engelleri belirli pozisyonlara yerleştirin
        foreach (GameObject obstacle in fixedObstacles)
        {
            if (Random.value < obstacleProbability) // %20 olasılıkla engel yerleştir
            {
                Vector3 obstaclePosition = new Vector3(Random.Range(0, 0), 1, Random.Range(0, 0));
                Instantiate(obstacle, transform.position + obstaclePosition, Quaternion.identity, transform);
            }
        }
    }
}
