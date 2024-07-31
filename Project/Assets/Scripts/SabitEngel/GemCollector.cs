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

            // Gem nesnesindeki AudioSource bileşenini bul ve sesi çal
            AudioSource gemAudioSource = other.GetComponent<AudioSource>();
            if (gemAudioSource != null)
            {
                gemAudioSource.Play();
            }

            // Elmayı yok et
            Destroy(other.gameObject);
        }
    }
}
