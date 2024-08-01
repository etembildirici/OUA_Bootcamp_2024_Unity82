using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement; // SceneManager kullanmanýz gerekiyorsa yorum satýrýndan çýkarabilirsiniz

public class PlayerSpikeDetection : MonoBehaviour
{
    public Transform[] characterList;
    public AudioSource deathSound; // Ölüm sesi kaynaðý referansý
    private bool isHitBySpike = false;
    private bool isSoundPlayed = false; // Sesin çalýp çalmadýðýný kontrol etmek için eklenen deðiþken
    private Animator characterAnim;
    private PlayerTouchMovement playerTouchMovementScript;

    void Start()
    {
        playerTouchMovementScript = GetComponent<PlayerTouchMovement>();
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;
        characterAnim = characterList[index].GetComponent<Animator>();

        // Eðer deathSound atanmýþsa, doðruluðunu kontrol et
        if (deathSound == null)
        {
            Debug.LogWarning("deathSound AudioSource bileþeni atanmadý.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            if (!isHitBySpike)
            {
                if (characterAnim != null)
                {
                    // Ölüm animasyonunu tetikle
                    characterAnim.SetTrigger("Death");
                }

                if (deathSound != null && !isSoundPlayed)
                {
                    // Ölüm sesi çal
                    deathSound.Play();
                    isSoundPlayed = true; // Sesi çaldýktan sonra bu deðiþkeni true yap
                }

                GetComponent<PlayerTouchMovement>().enabled = false;
                isHitBySpike = true;
                Debug.Log("Karakter mýzraða çarptý.");

                // Opsiyonel olarak sahneyi yeniden yükleme veya oyun bitti iþlemi
                // StartCoroutine(ReloadSceneAfterDelay(2f));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            isHitBySpike = false;
        }
    }

    public bool IsHitBySpike()
    {
        return isHitBySpike;
    }

    // Opsiyonel sahneyi yeniden yükleme metodu
    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}



