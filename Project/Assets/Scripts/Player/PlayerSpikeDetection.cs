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
        // Player GameObject'inin içindeki "RogueHooded" adlý alt GameObject'teki Animator bileþenini bul
        Transform characterTransform = transform.Find("RogueHooded");
        if (characterTransform != null)
        {
            characterAnim = characterTransform.GetComponent<Animator>();
            if (characterAnim == null)
            {
                Debug.LogError("Animator bileþeni 'RogueHooded' alt GameObject'inde bulunamadý.");
            }
        }
        else
        {
            Debug.LogError("RogueHooded adlý alt GameObject bulunamadý. Lütfen doðru isimlendirildiðinden emin olun.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            if (characterAnim != null)
            {
                // Ölüm animasyonunu tetikle
                characterAnim.SetTrigger("Death");
            }

            GetComponent<PlayerTouchMovement>().enabled = false;
            isHitBySpike = true;
            Debug.Log("Karakter mýzraða çarptý.");

            // Opsiyonel olarak sahneyi yeniden yükleme veya oyun bitti iþlemi
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

    // Opsiyonel sahneyi yeniden yükleme metodu
    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

