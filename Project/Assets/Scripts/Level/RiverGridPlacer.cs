using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverManager : MonoBehaviour
{
    public enum RiverDirection
    {
        LeftToRight,
        RightToLeft
    }

    public GameObject[] gridPrefabs;
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.5f;
    public float minMoveSpeed = 1.5f;
    public float maxMoveSpeed = 3.5f;
    public int initialGridSetCount = 3;
    public float initialGridSpacing = 2f;
    public int gridCount = 5;
    public float riverWidth = 40f;
    public RiverDirection riverDirection = RiverDirection.LeftToRight;


    private float spawnPositionX;
    private float fixedY;
    private float fixedZ;
    private float currentMoveSpeed;
    private Vector3 moveDirection;

    void Start()
    {
        SetSpawnPosition();
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
        SetNewRiverSpeed();
        SetMoveDirection();

        // Hemen ilk grid setlerini ekle
        SpawnInitialGridSets();

        // Sonra normal spawn döngüsünü baþlat
        StartCoroutine(SpawnGrids());
    }

    void SetSpawnPosition()
    {
        spawnPositionX = riverDirection == RiverDirection.LeftToRight ?
            -riverWidth / 2 - 6f : riverWidth / 2 + 6f;
    }

    void SetMoveDirection()
    {
        moveDirection = riverDirection == RiverDirection.LeftToRight ?
            Vector3.right : Vector3.left;
    }

    void SetNewRiverSpeed()
    {
        currentMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void SpawnInitialGridSets()
    {
        for (int i = 0; i < initialGridSetCount; i++)
        {
            float offset = i * initialGridSpacing;
            SpawnGridSet(offset);
        }
    }

    IEnumerator SpawnGrids()
    {
        while (true)
        {
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
            SpawnGridSet(0f);
        }
    }

    void SpawnGridSet(float offset)
    {
        List<GameObject> gridSet = new List<GameObject>();

        for (int i = 0; i < gridCount; i++)
        {
            GameObject selectedPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];
            Vector3 spawnPosition = new Vector3(spawnPositionX + (riverDirection == RiverDirection.LeftToRight ? offset : -offset), fixedY, fixedZ);
            GameObject grid = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);
            gridSet.Add(grid);
        }

        StartCoroutine(MoveGridSet(gridSet));
    }

    IEnumerator MoveGridSet(List<GameObject> gridSet)
    {
        float elapsedDistance = 0f;

        while (elapsedDistance < riverWidth + 6f)
        {
            elapsedDistance += currentMoveSpeed * Time.deltaTime;

            foreach (GameObject grid in gridSet)
            {
                grid.transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime);
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
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverManager : MonoBehaviour
{
    public enum RiverDirection
    {
        LeftToRight,
        RightToLeft
    }

    public GameObject[] gridPrefabs;
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.5f;
    public float minMoveSpeed = 1.5f;
    public float maxMoveSpeed = 3.5f;
    public int gridCount = 5;
    public float riverWidth = 40f;
    public RiverDirection riverDirection = RiverDirection.LeftToRight;

    private float spawnPositionX;
    private float fixedY;
    private float fixedZ;
    private float currentMoveSpeed;
    private Vector3 moveDirection;

    void Start()
    {
        SetSpawnPosition();
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
        SetNewRiverSpeed();
        SetMoveDirection();
        StartCoroutine(SpawnGrids());
    }

    void SetSpawnPosition()
    {
        spawnPositionX = riverDirection == RiverDirection.LeftToRight ?
            -riverWidth / 2 - 6f : riverWidth / 2 + 6f;
    }

    void SetMoveDirection()
    {
        moveDirection = riverDirection == RiverDirection.LeftToRight ?
            Vector3.right : Vector3.left;
    }

    void SetNewRiverSpeed()
    {
        currentMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
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

        for (int i = 0; i < gridCount; i++)
        {
            GameObject selectedPrefab = gridPrefabs[Random.Range(0, gridPrefabs.Length)];
            Vector3 spawnPosition = new Vector3(spawnPositionX, fixedY, fixedZ);
            GameObject grid = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, transform);
            gridSet.Add(grid);
        }

        StartCoroutine(MoveGridSet(gridSet));
    }

    IEnumerator MoveGridSet(List<GameObject> gridSet)
    {
        float elapsedDistance = 0f;

        while (elapsedDistance < riverWidth + 6f)
        {
            elapsedDistance += currentMoveSpeed * Time.deltaTime;

            foreach (GameObject grid in gridSet)
            {
                grid.transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime);
            }

            yield return null;
        }

        foreach (GameObject grid in gridSet)
        {
            Destroy(grid);
        }
    }
}
*/
