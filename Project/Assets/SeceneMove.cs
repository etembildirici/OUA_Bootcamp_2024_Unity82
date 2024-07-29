using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeceneMove : MonoBehaviour
{
    public float startPositionX ; // Baþlangýç noktasý
    public float endPositionX ;    // Bitiþ noktasý
    public float speed ;           // Hareket hýzý

    private bool movingRight = true;   // Hangi yönde hareket ediyor

    void Update()
    {
        // Hata ayýklama için pozisyonlarý konsola yazdýrýn
        Debug.Log("Current Position: " + transform.position.x);
        Debug.Log("Moving Right: " + movingRight);

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= endPositionX)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= startPositionX)
            {
                movingRight = true;
            }
        }
    }
}
