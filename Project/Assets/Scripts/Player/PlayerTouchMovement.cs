using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    public float moveDistance = 2f; // Karakterin bir ad�m at�� mesafesi
    private Vector3 nextPosition; // Karakterin bir sonraki hedef pozisyonu
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kayd�rma mesafesi
    private bool isSwiping = false; // Kayd�rma hareketini takip etmek i�in bayrak

    private float maxZPosition; // Karakterin ula�t��� en y�ksek z pozisyonu

    // Karakter hareket etti�inde �a�r�lacak olay
    public static event Action OnMoveForward;

    void Start()
    {
        nextPosition = transform.position; // Ba�lang��ta karakterin bulundu�u pozisyon
        maxZPosition = transform.position.z; // Ba�lang�� z pozisyonu
    }

    void Update()
    {
        // Dokunmatik ekran kontrolleri
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwiping = false;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                isSwiping = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                if (isSwiping && swipeDirection.magnitude > swipeThreshold) // Minimum kayd�rma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kayd�rma
                        if (swipeDirection.x > 0)
                        {
                            Move(Vector3.right);
                        }
                        else
                        {
                            Move(Vector3.left);
                        }
                    }
                    else
                    {
                        // Dikey kayd�rma
                        if (swipeDirection.y > 0)
                        {
                            Move(Vector3.forward);
                        }
                        else
                        {
                            Move(Vector3.back);
                        }
                    }
                }
                else if (!isSwiping)
                {
                    // Sadece ekrana t�klama durumunda ileri gitme
                    Move(Vector3.forward);
                }
            }
        }
    }
    
    /* private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            transform.parent = other.transform;
            Debug.Log("Karakter su �zerindeki gridin �zerine bindi.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            transform.parent = null;
            Debug.Log("Karakter su �zerindeki gridin �zerine bindi.");
        }
    } */

    void Move(Vector3 direction)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = nextPosition + direction * moveDistance;

        // Karakteri hedef pozisyona hareket ettirme
        transform.position = targetPosition;

        // Bir sonraki hedef pozisyonunu g�ncelleme
        nextPosition = targetPosition;

        // E�er ileriye do�ru hareket ettiyse ve yeni pozisyon en y�ksek z pozisyonundan daha b�y�kse hareket olay�n� tetikle
        if (direction == Vector3.forward && transform.position.z > maxZPosition)
        {
            maxZPosition = transform.position.z;
            OnMoveForward?.Invoke();
        }
    }
    /*
    public float moveDistance = 2f; // Karakterin bir ad�m at�� mesafesi
    private Vector3 nextPosition; // Karakterin bir sonraki hedef pozisyonu
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kayd�rma mesafesi

    private bool isOnRiverGrid = false;
    private Transform currentRiverGrid;
    private bool isSinking = false;

    private void Start()
    {
        nextPosition = transform.position; // Ba�lang��ta karakterin bulundu�u pozisyon
    }

    private void Update()
    {
        if (isSinking)
        {
            // Karakter batma hareketini ger�ekle�tirir
            transform.position -= new Vector3(0, 0.5f * Time.deltaTime, 0);
            return;
        }

        if (isOnRiverGrid && currentRiverGrid != null)
        {
            // Karakterin pozisyonunu gridin merkezine ayarlamak
            Vector3 gridCenter = currentRiverGrid.position;
            transform.position = new Vector3(gridCenter.x, transform.position.y, gridCenter.z);
        }

        // Dokunmatik ekran kontrolleri
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;

                Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                if (swipeDirection.magnitude > swipeThreshold) // Minimum kayd�rma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kayd�rma
                        if (swipeDirection.x > 0)
                        {
                            Move(Vector3.right);
                        }
                        else
                        {
                            Move(Vector3.left);
                        }
                    }
                    else
                    {
                        // Dikey kayd�rma
                        if (swipeDirection.y > 0)
                        {
                            Move(Vector3.forward);
                        }
                        else
                        {
                            Move(Vector3.back);
                        }
                    }
                }
            }
        }
    }

    private void Move(Vector3 direction)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = nextPosition + direction * moveDistance;

        // Karakteri hedef pozisyona hareket ettirme
        transform.position = targetPosition;

        // Bir sonraki hedef pozisyonunu g�ncelleme
        nextPosition = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            isOnRiverGrid = true;
            currentRiverGrid = other.transform;
            Debug.Log("Karakter su �zerindeki gridin �zerine bindi.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            isOnRiverGrid = false;
            currentRiverGrid = null;
            Debug.Log("Karakter su �zerindeki gridin �zerinden ayr�ld�.");
        }
    }
    */
}
