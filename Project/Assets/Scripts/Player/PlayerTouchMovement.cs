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

    void Start()
    {
        nextPosition = transform.position; // Ba�lang��ta karakterin bulundu�u pozisyon
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

    void Move(Vector3 direction)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = nextPosition + direction * moveDistance;

        // Karakteri hedef pozisyona hareket ettirme
        transform.position = targetPosition;

        // Bir sonraki hedef pozisyonunu g�ncelleme
        nextPosition = targetPosition;
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
