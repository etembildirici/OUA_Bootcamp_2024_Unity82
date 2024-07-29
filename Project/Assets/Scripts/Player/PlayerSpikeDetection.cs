using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
//using UnityEngine.SceneManagement;

public class PlayerSpikeDetection : MonoBehaviour
{
    public Transform[] characterList;
    private bool isHitBySpike = false;
    private Animator characterAnim;
    private PlayerTouchMovement playerTouchMovementScript;

    void Start()
    {
        playerTouchMovementScript = GetComponent<PlayerTouchMovement>();
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;
        characterAnim = characterList[index].GetComponent<Animator>();

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

