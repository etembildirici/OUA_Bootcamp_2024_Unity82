using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverGridPlacer : MonoBehaviour
{
    public GameObject[] gridPrefabs; // Izgara prefab'lar�
    public float minGridSpacing = 2f; // Izgaralar aras�ndaki minimum mesafe
    public float maxGridSpacing = 10f; // Izgaralar aras�ndaki maksimum mesafe
    public float riverLength = 50f; // Nehir prefab'�n�n uzunlu�u
    public LayerMask gridLayerMask; // Izgaralar�n yerle�tirilme katman�
    public int direction = 1; // Izgara hareket y�n� (-1 veya 1)

    void Start()
    {
        PlaceGrids();
    }

    void PlaceGrids()
    {
        float currentLength = 0f;
        int gridCount = Random.Range(2, 4); // Izgara say�s�n� 2 ile 4 aras�nda rastgele belirle

        for (int i = 0; i < gridCount; i++)
        {
            // Rastgele bir gridPrefab se�
            GameObject selectedGridPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];

            // Izgaray� yerle�tirilecek uygun bir pozisyon bul
            Vector3 position = FindNonOverlappingPosition(currentLength);
            if (position == Vector3.zero) break; // Uygun pozisyon bulunamazsa d�ng�den ��k

            GameObject newGrid = Instantiate(selectedGridPrefab, position, Quaternion.identity, transform);
            newGrid.GetComponent<TrapMover>().direction = direction;

            // Mevcut uzunlu�a ekle
            currentLength = position.x;

            // Nehir uzunlu�unu a��yorsa d�ng�den ��k
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

        return Vector3.zero; // Uygun pozisyon bulunamazsa s�f�r d�nd�r
    }
}

/*
public class RiverGridPlacer : MonoBehaviour
{
    public GameObject[] gridPrefabs; // Izgara prefab'lar�
    public float minGridSpacing = 2f; // Izgaralar aras�ndaki minimum mesafe
    public float maxGridSpacing = 10f; // Izgaralar aras�ndaki maksimum mesafe
    public float riverLength = 50f; // Nehir prefab'�n�n uzunlu�u
    public LayerMask gridLayerMask; // Izgaralar�n yerle�tirilme katman�

    void Start()
    {
        PlaceGrids();
    }

    void PlaceGrids()
    {
        float currentLength = 0f;
        int gridCount = Random.Range(1, 4); // Izgara say�s�n� 1 ile 3 aras�nda rastgele belirle

        for (int i = 0; i < gridCount; i++)
        {
            // Rastgele bir gridPrefab se�
            GameObject selectedGridPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];

            // Izgaray� yerle�tirilecek uygun bir pozisyon bul
            Vector3 position = FindNonOverlappingPosition(currentLength);
            if (position == Vector3.zero) break; // Uygun pozisyon bulunamazsa d�ng�den ��k

            Instantiate(selectedGridPrefab, position, Quaternion.identity, transform);

            // Mevcut uzunlu�a ekle
            currentLength = position.x;

            // Nehir uzunlu�unu a��yorsa d�ng�den ��k
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

        return Vector3.zero; // Uygun pozisyon bulunamazsa s�f�r d�nd�r
    }
}*/
