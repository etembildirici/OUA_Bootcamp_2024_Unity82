using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement2 : MonoBehaviour
    {
        public float moveDistance = 1f; // Karakterin bir adım atış mesafesi
        private Vector3 nextPosition; // Karakterin bir sonraki hedef pozisyonu

        // Karakter hareket ettiğinde çağrılacak olay
        public static event Action OnMove;

        void Start()
        {
            nextPosition = transform.position; // Başlangıçta karakterin bulunduğu pozisyon
        }

        void Update()
        {
            // Input alarak karakterin hareketini sağlama
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

            // Hareket olayını tetikleme
            OnMove?.Invoke();
        }
    }
}
