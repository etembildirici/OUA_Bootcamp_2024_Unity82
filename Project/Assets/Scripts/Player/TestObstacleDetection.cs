using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObstacleDetection : MonoBehaviour
{
    public float moveDistance = 2f; // Karakterin bir ad�m at�� mesafesi
    public LayerMask obstacleLayer; // Engellerin bulundu�u katman

    private float maxZPosition; // Karakterin ula�t��� en y�ksek z pozisyonu

    private void Start()
    {
        maxZPosition = transform.position.z; // Ba�lang�� z pozisyonu
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryMove(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMove(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMove(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TryMove(Vector3.back);
        }
    }

    private void TryMove(Vector3 direction)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Hedef pozisyonda bir engel olup olmad���n� kontrol et
        if (!IsObstacleInDirection(direction))
        {
            // E�er engel yoksa karakteri hedef pozisyona hareket ettir
            moveCharacter(direction);
        }
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, moveDistance, obstacleLayer))
        {
            Debug.Log("Engel alg�land�: " + hit.collider.name);
            return true;
        }
        return false;
    }

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * moveDistance;

        // E�er ileriye do�ru hareket ettiyse ve yeni pozisyon en y�ksek z pozisyonundan daha b�y�kse
        if (direction == Vector3.forward && transform.position.z > maxZPosition)
        {
            maxZPosition = transform.position.z;
            // Burada ileri hareket olay� tetiklenebilir
        }
    }
}
