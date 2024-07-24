using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    public GemUIManager gemUIManager; // GemUIManager referansı

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            // Elması topla ve UI'ı güncelle
            gemUIManager.IncrementGemCount();
            Destroy(other.gameObject);
        }
    }
}
