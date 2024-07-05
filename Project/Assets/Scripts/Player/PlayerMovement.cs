using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveDistance = 1f; // Karakterin bir adým atýþ mesafesi
        private Vector3 nextPosition; // Karakterin bir sonraki hedef pozisyonu

        void Start()
        {
            nextPosition = transform.position; // Baþlangýçta karakterin bulunduðu pozisyon
        }

        void Update()
        {
            // Input alarak karakterin hareketini saðlama
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.back);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right);
            }
        }

        void Move(Vector3 direction)
        {
            // Hedef pozisyonu belirleme
            Vector3 targetPosition = nextPosition + direction * moveDistance;

            // Karakteri hedef pozisyona hareket ettirme
            transform.position = targetPosition;

            // Bir sonraki hedef pozisyonunu güncelleme
            nextPosition = targetPosition;
        }
    }
}
