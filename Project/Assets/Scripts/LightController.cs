using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform komponenti
    public Vector3 offset = new Vector3(0, 10, -10); // Oyuncuya göre ýþýðýn konumu
    public float smoothSpeed = 0.125f; // Takip hýzýnýn yumuþaklýðý

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Iþýðýn oyuncuya bakacak þekilde rotasyonunu ayarla
            transform.LookAt(player);
        }
    }
}
