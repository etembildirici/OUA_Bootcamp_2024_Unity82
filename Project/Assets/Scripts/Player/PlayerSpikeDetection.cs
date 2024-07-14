using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerSpikeDetection : MonoBehaviour
{
    private bool isHitBySpike = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            GetComponent<PlayerTouchMovement>().enabled = false;
            isHitBySpike = true;
            Debug.Log("Karakter mýzraða çarptý.");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
