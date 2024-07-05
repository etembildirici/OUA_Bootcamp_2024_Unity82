using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followSpeed = 2f; // Kameran�n takip etme h�z�
    [SerializeField] private Vector3 offset = new Vector3(10, 10, -10); // Kameran�n oyuncuya g�re konumu

    private IEnumerator _followCoroutine;

    private void Start()
    {
        // Kameran�n ba�lang�� pozisyonunu ayarla
        transform.position = playerTransform.position + offset;

        // Kameran�n oyuncuyu takip etmesi i�in Coroutine ba�lat
        _followCoroutine = FollowPlayer();
        StartCoroutine(_followCoroutine);
    }

    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            // Kameran�n hedef pozisyonunu hesapla
            Vector3 targetPosition = playerTransform.position + offset;

            // Kameray� hedef pozisyona yumu�ak bir �ekilde hareket ettir
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
