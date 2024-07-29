using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameManager gameManager; // GameManager referansý
    private bool isPlayerAlive = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike") || other.CompareTag("Water"))
        {
            isPlayerAlive = false;
            Debug.Log("Player collided with obstacle!");
            gameManager.EndGame(); // Oyunu durdur ve yeniden baþlama butonunu göster
        }
    }
}