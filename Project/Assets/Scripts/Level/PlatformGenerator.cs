using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public List<GameObject> prefabs; // Prefablar?n?z? buraya ekleyin
    public Transform player; // Oyuncunun transform'u
    public float spawnDistance = 20f; // Oyuncunun ?n?nde spawn edilecek mesafe
    public int tilesToShow = 5; // Ayn? anda g?sterilecek tile say?s?
    public GameObject level; // Level GameObject
    public GameObject startTile; // Oyunun ba?lang?? noktas? olan prefab

    private Queue<GameObject> activeTiles = new Queue<GameObject>(); // Aktif prefablar? tutan kuyruk
    private Vector3 nextSpawnPosition;
    private GameObject lastSpawnedTile; // Son eklenen prefab? tutmak i?in de?i?ken

    void Start()
    {
        // Ba?lang?? prefab?n?n pozisyonunu al
        nextSpawnPosition = startTile.transform.position + GetPrefabSize(startTile);

        for (int i = 0; i < tilesToShow; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.position, nextSpawnPosition) < spawnDistance)
        {
            SpawnTile();
            DespawnTile();
        }
    }

    void SpawnTile()
    {
        GameObject newTile = Instantiate(prefabs[Random.Range(0, prefabs.Count)], nextSpawnPosition, Quaternion.identity, level.transform);

        if (lastSpawnedTile != null)
        {
            if (lastSpawnedTile.CompareTag("FloorGreyBlack02") && newTile.CompareTag("FloorGreyBlack02"))
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[2], nextSpawnPosition, Quaternion.identity, level.transform);
            }
            else if (lastSpawnedTile.CompareTag("FloorGreyBlack01") && newTile.CompareTag("FloorGreyBlack01"))
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[3], nextSpawnPosition, Quaternion.identity, level.transform);
            }
        }

        // Sadece FloorGreyBlack01 ve FloorGreyBlack02 tag'ine sahip Tile prefab'larına sabit engeller ekleyin
        if (newTile.CompareTag("FloorGreyBlack01") || newTile.CompareTag("FloorGreyBlack02"))
        {
            RoadSegment roadSegment = newTile.GetComponent<RoadSegment>();
            if (roadSegment != null)
            {
                roadSegment.PlaceObstacles();
            }
        }

        activeTiles.Enqueue(newTile);
        nextSpawnPosition += GetPrefabSize(newTile);
        lastSpawnedTile = newTile;
    }

    void DespawnTile()
    {
        if (activeTiles.Count > tilesToShow)
        {
            GameObject oldTile = activeTiles.Dequeue();
            Destroy(oldTile);
        }
    }

    Vector3 GetPrefabSize(GameObject prefab)
    {
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            return new Vector3(0, 0, renderer.bounds.size.x); // Prefab?n k?sa kenar?n? hesaplayarak z ekseninde ileri ta??
        }
        else
        {
            return Vector3.forward * 2; // Varsay?lan de?er
        }
    }
}
