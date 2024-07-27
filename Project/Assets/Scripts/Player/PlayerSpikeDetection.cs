using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerSpikeDetection : MonoBehaviour
{
    private bool isHitBySpike = false;
    private Animator characterAnim;

    void Start()
    {
        // Player GameObject'inin i�indeki "RogueHooded" adl� alt GameObject'teki Animator bile�enini bul
        Transform characterTransform = transform.Find("RogueHooded");
        if (characterTransform != null)
        {
            characterAnim = characterTransform.GetComponent<Animator>();
            if (characterAnim == null)
            {
                Debug.LogError("Animator bile�eni 'RogueHooded' alt GameObject'inde bulunamad�.");
            }
        }
        else
        {
            Debug.LogError("RogueHooded adl� alt GameObject bulunamad�. L�tfen do�ru isimlendirildi�inden emin olun.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            if (characterAnim != null)
            {
                // �l�m animasyonunu tetikle
                characterAnim.SetTrigger("Death");
            }

            GetComponent<PlayerTouchMovement>().enabled = false;
            isHitBySpike = true;
            Debug.Log("Karakter m�zra�a �arpt�.");

            // Opsiyonel olarak sahneyi yeniden y�kleme veya oyun bitti i�lemi
            // StartCoroutine(ReloadSceneAfterDelay(2f));
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

