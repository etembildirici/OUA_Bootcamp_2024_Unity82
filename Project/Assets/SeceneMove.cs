using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeceneMove : MonoBehaviour
{
    public float startPositionX ; // Ba�lang�� noktas�
    public float endPositionX ;    // Biti� noktas�
    public float speed ;           // Hareket h�z�

    private bool movingRight = true;   // Hangi y�nde hareket ediyor

    void Update()
    {
        // Hata ay�klama i�in pozisyonlar� konsola yazd�r�n
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
