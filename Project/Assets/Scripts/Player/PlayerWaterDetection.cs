using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWaterDetection : MonoBehaviour
{
    public LayerMask waterLayer; // Su katmanýný belirleyin
    public float detectionDistance = 1f; // Su tespit mesafesi
    public float sinkingSpeed = 0.5f; // Karakterin suya düþme hýzý
    public float sinkingDepth = 8f; // Karakterin düþeceði mesafe
    public AudioSource waterSound; // Suya düþme ses kaynaðý
    public float soundDelay = 0.5f; // Sese gecikme süresi (saniye)

    private bool isSinking = false;
    private float initialYPosition;

    void Update()
    {
        if (!isSinking)
        {
            DetectWater();
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
        initialYPosition = transform.position.y;

        // Eðer su sesi atanmýþsa çal
        if (waterSound != null)
        {
            StartCoroutine(PlaySoundWithDelay(soundDelay));
        }

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

    IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        waterSound.Play();
    }
}


