using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followSpeed = 2f; // Kameranýn takip etme hýzý
    [SerializeField] private Vector3 offset = new Vector3(10, 10, -10); // Kameranýn oyuncuya göre konumu

    private IEnumerator _followCoroutine;

    private void Start()
    {
        // Kameranýn baþlangýç pozisyonunu ayarla
        transform.position = playerTransform.position + offset;

        // Kameranýn oyuncuyu takip etmesi için Coroutine baþlat
        _followCoroutine = FollowPlayer();
        StartCoroutine(_followCoroutine);
    }

    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            // Kameranýn hedef pozisyonunu hesapla
            Vector3 targetPosition = playerTransform.position + offset;

            // Kamerayý hedef pozisyona yumuþak bir þekilde hareket ettir
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
