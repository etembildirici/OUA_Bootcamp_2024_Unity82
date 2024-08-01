using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    public GemUIManager gemUIManager; // GemUIManager referansı
    public AudioSource gemSound; // Gem ses kaynağı

    void OnTriggerEnter(Collider other)
    {
        // Eğer çarpan nesne "Gem" tag'ine sahipse
        if (other.CompareTag("Gem"))
        {
            // Elması topla ve UI'ı güncelle
            gemUIManager.IncrementGemCount();

            // Eğer ses kaynağı atanmışsa, sesi çal
            if (gemSound != null)
            {
                gemSound.Play();
            }
            else
            {
                Debug.LogWarning("Gem ses kaynağı atanmadı.");
            }

            // Elmayı yok et
            Destroy(other.gameObject);
        }
    }
}


