using UnityEngine;

public class GemAudioActivator : MonoBehaviour
{
    public AudioSource moveSound; // Ses kaynaðý referansý

    void Start()
    {
        // Eðer moveSound atanmýþsa, doðruluðunu kontrol et
        if (moveSound == null)
        {
            Debug.LogWarning("moveSound AudioSource bileþeni atanmadý.");
        }
        else
        {
            Debug.Log("moveSound AudioSource bileþeni atanmýþ.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Eðer çarpan nesne "Player" tag'ine sahipse
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player tag'i ile çarpýþma algýlandý.");

            // AudioSource bileþenini etkinleþtir ve sesi çal
            if (moveSound != null)
            {
                Debug.Log("AudioSource bulunarak sesi çal.");
                moveSound.Play();
            }
            else
            {
                Debug.LogWarning("moveSound AudioSource bileþeni bulunamadý.");
            }
        }
    }
}



