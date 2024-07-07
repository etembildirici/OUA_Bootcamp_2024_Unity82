using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpikeDetection : MonoBehaviour
{
    private bool isHitBySpike = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            GetComponent<PlayerTouchMovement>().enabled = false;
            isHitBySpike = true;
            Debug.Log("Karakter m�zra�a �arpt�.");
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
