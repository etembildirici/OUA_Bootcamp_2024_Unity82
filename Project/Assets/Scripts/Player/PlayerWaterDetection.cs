using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWaterDetection : MonoBehaviour
{
    public LayerMask waterLayer; // Su katmanýný belirleyin
    public float detectionDistance = 1f; // Su tespit mesafesi
    public float sinkingSpeed = 0.5f; // Karakterin suya düþme hýzý
    private bool isSinking = false;

    void Update()
    {
        if (!isSinking)
        {
            DetectWater();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player collided with obstacle!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void DetectWater()
    {
        // Karakterin altýna doðru bir ray gönderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, detectionDistance);

        bool isWaterDetected = false;
        bool isNonWaterDetected = false;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Door"))
            {
                continue; // "Door" tag'li nesneleri es geç
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

        // Eðer sadece suya temas ediyorsa ve altýnda baþka bir nesne yoksa
        if (isWaterDetected && !isNonWaterDetected)
        {
            // Karakterin yüksekliðine göre suya düþme iþlemlerini burada gerçekleþtirin
            Debug.Log("Suya düþtü!");
            StartSinking();
        }
    }

    void StartSinking()
    {
        // Hareket scriptini devre dýþý býrak
        GetComponent<PlayerTouchMovement>().enabled = false;

        // Karakteri suya batýrma iþlemini baþlat
        isSinking = true;
        StartCoroutine(Sink());
    }

    IEnumerator Sink()
    {
        while (true)
        {
            // Karakterin y pozisyonunu yavaþça azalt
            transform.position -= new Vector3(0, sinkingSpeed * Time.deltaTime, 0);

            // Belirli bir süre bekle
            yield return null;
        }
    }
}
