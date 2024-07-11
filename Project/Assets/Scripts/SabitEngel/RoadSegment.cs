using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    public GameObject[] fixedObstacles; // Sabit engel prefab'ları
    public int gridWidth = 10; // Grid'in genişliği (Tile'ların sayısı)

    private void Start()
    {
        PlaceObstacles();
    }

    public void PlaceObstacles()
    {
        foreach (GameObject obstacle in fixedObstacles)
        {
            Vector3 obstaclePosition = GetRandomGridPosition();
            Instantiate(obstacle, transform.position + obstaclePosition, Quaternion.identity, transform);
        }
    }

    private Vector3 GetRandomGridPosition()
    {
        // Rastgele bir grid pozisyonu belirleme
        int randomX = Random.Range(0, gridWidth);
        int randomZ = Random.Range(0, gridWidth);
        return new Vector3(randomX, -2, randomZ);
    }
}



