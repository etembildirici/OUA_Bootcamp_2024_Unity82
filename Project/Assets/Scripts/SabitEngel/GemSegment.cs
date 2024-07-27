using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemSegment : MonoBehaviour
{
    public GameObject[] gems; // Elmas prefab'ları
    public float gemProbability = 0.1f; // Elmas yerleştirme olasılığı (%20)
    public Vector3 predefinedPosition; // Önceden tanımlanmış pozisyon
    public string[] validTags = { "FloorBlack", "FloorGrey", "RiverWithGrate" }; // Geçerli etiketler

    private void Start()
    {
        // Yalnızca geçerli etiketlere sahip parent objeleri için çalış
        if (IsParentTagValid())
        {
            PlaceGems();
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

    public void PlaceGems()
    {
        // Elmas listesinden rastgele bir elmas seç
        GameObject randomGem = gems[Random.Range(0, gems.Length)];

        if (Random.value < gemProbability) // %20 olasılıkla elmas yerleştir
        {
            Vector3 gemPosition = predefinedPosition;

            // Elması oluştur
            Instantiate(randomGem, transform.position + gemPosition, Quaternion.identity, transform);
        }
    }
}
