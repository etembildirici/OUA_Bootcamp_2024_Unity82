using UnityEngine;

public class GemAudioActivator : MonoBehaviour
{
    public AudioSource moveSound; // Ses kayna�� referans�

    void Start()
    {
        // E�er moveSound atanm��sa, do�rulu�unu kontrol et
        if (moveSound == null)
        {
            Debug.LogWarning("moveSound AudioSource bile�eni atanmad�.");
        }
        else
        {
            Debug.Log("moveSound AudioSource bile�eni atanm��.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // E�er �arpan nesne "Player" tag'ine sahipse
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player tag'i ile �arp��ma alg�land�.");

            // AudioSource bile�enini etkinle�tir ve sesi �al
            if (moveSound != null)
            {
                Debug.Log("AudioSource bulunarak sesi �al.");
                moveSound.Play();
            }
            else
            {
                Debug.LogWarning("moveSound AudioSource bile�eni bulunamad�.");
            }
        }
    }
}



