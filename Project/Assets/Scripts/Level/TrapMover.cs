using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMover : MonoBehaviour
{
    public float speed = 1f; // Izgaralarýn hareket hýzý
    public float resetPositionX = -10f; // Izgara resetlenecek pozisyon
    public float startPositionX = 10f; // Izgara baþlangýç pozisyonu

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}
