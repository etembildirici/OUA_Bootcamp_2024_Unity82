using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWaterDetection : MonoBehaviour
{
    public LayerMask waterLayer; // Su katman?n? belirleyin
    public float detectionDistance = 1f; // Su tespit mesafesi
    public float sinkingSpeed = 0.5f; // Karakterin suya d??me h?z?
    public float sinkingDepth = 8f; // Karakterin düþeceði mesafe
    private bool isSinking = false;
    private float initialYPosition;

    void Update()
    {
        if (!isSinking)
        {
            DetectWater();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player collided with obstacle!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/

    void DetectWater()
    {
        // Karakterin alt?na do?ru bir ray g?nderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

        bool isWaterDetected = false;
        bool isNonWaterDetected = false;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Door"))
            {
                continue; // "Door" tag'li nesneleri es ge?
            }

            if (hit.collider.CompareTag("Water"))
            {
                isWaterDetected = true;
            }
            else
            {
                isNonWaterDetected = true;
            }
        }

        // E?er sadece suya temas ediyorsa ve alt?nda ba?ka bir nesne yoksa
        if (isWaterDetected && !isNonWaterDetected)
        {
            // Karakterin y?ksekli?ine g?re suya d??me i?lemlerini burada ger?ekle?tirin
            Debug.Log("Suya d??t?!");
            StartSinking();
        }
    }

    void StartSinking()
    {
        // Hareket scriptini devre d??? b?rak
        GetComponent<PlayerTouchMovement>().enabled = false;

        // Karakteri suya bat?rma i?lemini ba?lat
        isSinking = true;
        initialYPosition = transform.position.y;
        StartCoroutine(Sink());
    }

    IEnumerator Sink()
    {
        while (transform.position.y > initialYPosition - sinkingDepth)
        {
            // Karakterin y pozisyonunu yavaþça azalt
            transform.position -= new Vector3(0, sinkingSpeed * Time.deltaTime, 0);

            // Belirli bir süre bekle
            yield return null;
        }

        // Belirli bir mesafe düþtükten sonra batmayý durdur
        isSinking = false;
    }
}
