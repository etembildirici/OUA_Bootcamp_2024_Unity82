using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class RoadSegment : MonoBehaviour
{
    public GameObject[] fixedObstacles; // Sabit engel prefab'ları
    public float obstacleProbability = 0.2f; // Engel yerleştirme olasılığı (%20)
    public Vector3 predefinedPosition; // Önceden tanımlanmış pozisyon
    public string[] validTags = { "FloorGreyBlack01", "FloorGreyBlack02" }; // Geçerli etiketler


    private void Start()
    {
        // Yalnızca geçerli etiketlere sahip parent objeleri için çalış
        if (IsParentTagValid())
        {

            PlaceObstacles();
        }
    }

    private bool IsParentTagValid()
    {
        foreach (string tag in validTags)
        {
            if (transform.parent != null && transform.parent.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceObstacles()
    {
        // Engel listesinden rastgele bir engel seç
        GameObject randomObject = fixedObstacles[Random.Range(0, fixedObstacles.Length)];

        if (Random.value < obstacleProbability) // %20 olasılıkla engel yerleştir
        {
            Vector3 obstaclePosition = predefinedPosition;

            // Engeli oluştur
            Instantiate(randomObject, transform.position + obstaclePosition, Quaternion.identity, transform);
        }

    }
}

/*
public class RoadSegment : MonoBehaviour
{
    public GameObject[] fixedObstacles; // Sabit engel prefab'ları
    public float obstacleProbability = 0.2f; // Engel yerleştirme olasılığı (%20)
    public Vector3[] predefinedPositions; // Önceden tanımlanmış pozisyonlar
    public string[] validTags = { "FloorGreyBlack01", "FloorGreyBlack02" }; // Geçerli etiketler

    private List<Vector3> availablePositions = new List<Vector3>();

    private void Start()
    {
        // Yalnızca geçerli etiketlere sahip parent objeleri için çalış
        if (IsParentTagValid())
        {
            // Başlangıçta tüm pozisyonları kullanılabilir olarak ayarlayın
            availablePositions.AddRange(predefinedPositions);
            PlaceObstacles();
        }
    }

    private bool IsParentTagValid()
    {
        foreach (string tag in validTags)
        {
            if (transform.parent != null && transform.parent.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public void PlaceObstacles()
    {
        foreach (GameObject obstacle in fixedObstacles)
        {
            if (Random.value < obstacleProbability && availablePositions.Count > 0) // %20 olasılıkla engel yerleştir
            {
                // Rastgele bir kullanılabilir pozisyon seç
                int index = Random.Range(0, availablePositions.Count);
                Vector3 obstaclePosition = availablePositions[index];

                // Engeli oluştur ve pozisyonu kullanılabilir listesinden çıkar
                Instantiate(obstacle, transform.position + obstaclePosition, Quaternion.identity, transform);
                availablePositions.RemoveAt(index);
            }
        }
    }
}
*/