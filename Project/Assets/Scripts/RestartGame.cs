using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameManager gameManager; // GameManager referans?

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike") || other.CompareTag("Water"))
        {
            Debug.Log("Player collided with obstacle!");
            gameManager.EndGame();// Oyunu durdur ve yeniden ba?lama butonunu g?ster

        }
    }
}