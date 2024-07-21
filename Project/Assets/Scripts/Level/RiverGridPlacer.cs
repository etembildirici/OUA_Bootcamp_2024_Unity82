using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverGridPlacer : MonoBehaviour
{
    public GameObject[] gridPrefabs; // Izgara prefab'larý
    public float minGridSpacing = 2f; // Izgaralar arasýndaki minimum mesafe
    public float maxGridSpacing = 10f; // Izgaralar arasýndaki maksimum mesafe
    public float riverLength = 50f; // Nehir prefab'ýnýn uzunluðu
    public LayerMask gridLayerMask; // Izgaralarýn yerleþtirilme katmaný
    public int direction = 1; // Izgara hareket yönü (-1 veya 1)

    void Start()
    {
        PlaceGrids();
    }

    void PlaceGrids()
    {
        float currentLength = 0f;
        int gridCount = Random.Range(2, 4); // Izgara sayýsýný 2 ile 4 arasýnda rastgele belirle

        for (int i = 0; i < gridCount; i++)
        {
            // Rastgele bir gridPrefab seç
            GameObject selectedGridPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];

            // Izgarayý yerleþtirilecek uygun bir pozisyon bul
            Vector3 position = FindNonOverlappingPosition(currentLength);
            if (position == Vector3.zero) break; // Uygun pozisyon bulunamazsa döngüden çýk

            GameObject newGrid = Instantiate(selectedGridPrefab, position, Quaternion.identity, transform);
            newGrid.GetComponent<TrapMover>().direction = direction;

            // Mevcut uzunluða ekle
            currentLength = position.x;

            // Nehir uzunluðunu aþýyorsa döngüden çýk
            if (currentLength > riverLength)
            {
                break;
            }
        }
    }

    Vector3 FindNonOverlappingPosition(float currentLength)
    {
        for (int attempt = 0; attempt < 10; attempt++) // 10 deneme yap
        {
            float randomSpacing = Random.Range(minGridSpacing, maxGridSpacing);
            Vector3 position = transform.position + new Vector3(currentLength + (randomSpacing * direction), 0, 0);

            Collider[] colliders = Physics.OverlapBox(position, new Vector3(1, 1, 1), Quaternion.identity, gridLayerMask);
            if (colliders.Length == 0)
            {
                return position;
            }
        }

        return Vector3.zero; // Uygun pozisyon bulunamazsa sýfýr döndür
    }
}

/*
public class RiverGridPlacer : MonoBehaviour
{
    public GameObject[] gridPrefabs; // Izgara prefab'larý
    public float minGridSpacing = 2f; // Izgaralar arasýndaki minimum mesafe
    public float maxGridSpacing = 10f; // Izgaralar arasýndaki maksimum mesafe
    public float riverLength = 50f; // Nehir prefab'ýnýn uzunluðu
    public LayerMask gridLayerMask; // Izgaralarýn yerleþtirilme katmaný

    void Start()
    {
        PlaceGrids();
    }

    void PlaceGrids()
    {
        float currentLength = 0f;
        int gridCount = Random.Range(1, 4); // Izgara sayýsýný 1 ile 3 arasýnda rastgele belirle

        for (int i = 0; i < gridCount; i++)
        {
            // Rastgele bir gridPrefab seç
            GameObject selectedGridPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];

            // Izgarayý yerleþtirilecek uygun bir pozisyon bul
            Vector3 position = FindNonOverlappingPosition(currentLength);
            if (position == Vector3.zero) break; // Uygun pozisyon bulunamazsa döngüden çýk

            Instantiate(selectedGridPrefab, position, Quaternion.identity, transform);

            // Mevcut uzunluða ekle
            currentLength = position.x;

            // Nehir uzunluðunu aþýyorsa döngüden çýk
            if (currentLength > riverLength)
            {
                break;
            }
        }
    }

    Vector3 FindNonOverlappingPosition(float currentLength)
    {
        for (int attempt = 0; attempt < 10; attempt++) // 10 deneme yap
        {
            float randomSpacing = Random.Range(minGridSpacing, maxGridSpacing);
            Vector3 position = transform.position + new Vector3(currentLength + randomSpacing, 0, 0);

            Collider[] colliders = Physics.OverlapBox(position, new Vector3(1, 1, 1), Quaternion.identity, gridLayerMask);
            if (colliders.Length == 0)
            {
                return position;
            }
        }

        return Vector3.zero; // Uygun pozisyon bulunamazsa sýfýr döndür
    }
}*/
