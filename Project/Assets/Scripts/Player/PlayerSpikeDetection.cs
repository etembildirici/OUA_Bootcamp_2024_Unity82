using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement; // SceneManager kullanman�z gerekiyorsa yorum sat�r�ndan ��karabilirsiniz

public class PlayerSpikeDetection : MonoBehaviour
{
    public Transform[] characterList;
    public AudioSource deathSound; // �l�m sesi kayna�� referans�
    private bool isHitBySpike = false;
    private bool isSoundPlayed = false; // Sesin �al�p �almad���n� kontrol etmek i�in eklenen de�i�ken
    private Animator characterAnim;
    private PlayerTouchMovement playerTouchMovementScript;

    void Start()
    {
        playerTouchMovementScript = GetComponent<PlayerTouchMovement>();
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;
        characterAnim = characterList[index].GetComponent<Animator>();

        // E�er deathSound atanm��sa, do�rulu�unu kontrol et
        if (deathSound == null)
        {
            Debug.LogWarning("deathSound AudioSource bile�eni atanmad�.");
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
                    // �l�m animasyonunu tetikle
                    characterAnim.SetTrigger("Death");
                }

                if (deathSound != null && !isSoundPlayed)
                {
                    // �l�m sesi �al
                    deathSound.Play();
                    isSoundPlayed = true; // Sesi �ald�ktan sonra bu de�i�keni true yap
                }

                GetComponent<PlayerTouchMovement>().enabled = false;
                isHitBySpike = true;
                Debug.Log("Karakter m�zra�a �arpt�.");

                // Opsiyonel olarak sahneyi yeniden y�kleme veya oyun bitti i�lemi
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

    // Opsiyonel sahneyi yeniden y�kleme metodu
    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}



