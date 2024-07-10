using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            Debug.Log("Player collided with obstacle!");

            // Oyunu yeniden baþlat
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
