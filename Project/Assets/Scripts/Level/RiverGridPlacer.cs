using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverManager : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.5f;
    public float minMoveSpeed = 1.5f;
    public float maxMoveSpeed = 3.5f;
    public int gridCount = 5;
    public float riverWidth = 40f;

    private float spawnPositionX;
    private float fixedY;
    private float fixedZ;

    void Start()
    {
        spawnPositionX = -riverWidth / 2 - 6f;
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
        StartCoroutine(SpawnGrids());
    }

    IEnumerator SpawnGrids()
    {
        while (true)
        {
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
            SpawnGridSet();
        }
    }

    void SpawnGridSet()
    {
        List<GameObject> gridSet = new List<GameObject>();
        float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        for (int i = 0; i < gridCount; i++)
        {
            GameObject selectedPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];
            Vector3 spawnPosition = new Vector3(spawnPositionX, fixedY, fixedZ);
            GameObject grid = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);
            gridSet.Add(grid);
        }

        StartCoroutine(MoveGridSet(gridSet, moveSpeed));
    }

    IEnumerator MoveGridSet(List<GameObject> gridSet, float moveSpeed)
    {
        float elapsedDistance = 0f;

        while (elapsedDistance < riverWidth + 6f)
        {
            elapsedDistance += moveSpeed * Time.deltaTime;

            foreach (GameObject grid in gridSet)
            {
                grid.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }

            yield return null;
        }

        foreach (GameObject grid in gridSet)
        {
            Destroy(grid);
        }
    }
}


/*
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

*/