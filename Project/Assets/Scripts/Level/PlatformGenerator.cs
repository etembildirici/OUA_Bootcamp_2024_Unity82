using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public List<GameObject> prefabs; // Prefablarýnýzý buraya ekleyin
    public Transform player; // Oyuncunun transform'u
    public float spawnDistance = 20f; // Oyuncunun önünde spawn edilecek mesafe
    public int tilesToShow = 5; // Ayný anda gösterilecek tile sayýsý
    public GameObject level; // Level GameObject
    public GameObject startTile; // Oyunun baþlangýç noktasý olan prefab

    private Queue<GameObject> activeTiles = new Queue<GameObject>(); // Aktif prefablarý tutan kuyruk
    private Vector3 nextSpawnPosition;
    private GameObject lastSpawnedTile; // Son eklenen prefabý tutmak için deðiþken
    private string lastSpawnedRiver; // Son eklenen nehir prefabý tutmak için deðiþken

    void Start()
    {
        // Baþlangýç prefabýnýn pozisyonunu al
        nextSpawnPosition = startTile.transform.position + GetPrefabSize(startTile);

        for (int i = 0; i < tilesToShow; i++)
        {
            SpawnTile();
        }

        lastSpawnedRiver = "River"; 
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
        GameObject newTile = null;
        bool isValidTile = false;

        // Prefab seçimini tekrar yapma döngüsü
        while (!isValidTile)
        {
            newTile = Instantiate(prefabs[Random.Range(0, prefabs.Count)], nextSpawnPosition, Quaternion.identity, level.transform);

            if (newTile.CompareTag("Trap"))
            {
                if (Random.value <= 0.2f) // %10 olasýlýkla "Trap" prefabý seçilsin
                {
                    isValidTile = true;
                }
                else
                {
                    Destroy(newTile); // %90 olasýlýkla "Trap" prefabýný yok et ve tekrar dene
                }
            }
            else
            {
                isValidTile = true; // Diðer prefablar için doðrudan kabul
            }
        }

        if (lastSpawnedTile != null)
        {
            if (lastSpawnedTile.CompareTag("FloorGreyBlack02") && newTile.CompareTag("FloorGreyBlack02"))
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[1], nextSpawnPosition, Quaternion.identity, level.transform);
            }
            else if (lastSpawnedTile.CompareTag("FloorGreyBlack01") && newTile.CompareTag("FloorGreyBlack01"))
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[2], nextSpawnPosition, Quaternion.identity, level.transform);
            }
        }

        if (newTile != null && (newTile.CompareTag("River") || newTile.CompareTag("River2")))
        {

            if (lastSpawnedRiver == "River")
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[4], nextSpawnPosition, Quaternion.identity, level.transform);
                lastSpawnedRiver = "River2";
            }
            else
            {
                Destroy(newTile);
                newTile = Instantiate(prefabs[3], nextSpawnPosition, Quaternion.identity, level.transform);
                lastSpawnedRiver = "River";
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
            return new Vector3(0, 0, renderer.bounds.size.x); // Prefabýn kýsa kenarýný hesaplayarak z ekseninde ileri taþý
        }
        else
        {
            return Vector3.forward * 2; // Varsayýlan deðer
        }
    }
}
