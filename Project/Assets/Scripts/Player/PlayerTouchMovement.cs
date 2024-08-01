using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    public Animator characterAnim;
    public Transform character;
    public Transform[] characterList;
    public float moveDistance = 2f; // Karakterin bir adým atýþ mesafesi
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kaydýrma mesafesi
    private bool isSwiping = false; // Kaydýrma hareketini takip etmek için bayrak
    public LayerMask obstacleLayer; // Engellerin bulunduðu katman

    private float maxZPosition; // Karakterin ulaþtýðý en yüksek z pozisyonu
    private bool isMoving = false; // Karakterin hareket edip etmediðini kontrol için bayrak
    private float currentBackwardSteps = 0f; // Þu anki geri adým sayýsý
    private float maxBackwardSteps = 4f; // Maksimum geri adým sayýsý

    // Karakter hareket ettiðinde çaðrýlacak olay
    public static event Action OnMoveForward;

    // AudioSource bileþeni
    public AudioSource moveSound; // Hareket sesini çalacak AudioSource

    // Hareket süresi
    public float moveDuration = 0.2f;

    void Start()
    {
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;
        character = characterList[index];
        characterAnim = character.GetComponent<Animator>();

        maxZPosition = transform.position.z; // Baþlangýç z pozisyonu

        // Eðer AudioSource bileþeni atanmýþsa, karakterin hareket ettiðinde ses çalacak
        if (moveSound == null)
        {
            Debug.LogWarning("moveSound AudioSource bileþeni atanmadý!");
        }
    }

    void Update()
    {
        if (isMoving)
            return; // Karakter hareket halindeyse baþka bir hareket baþlatma

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

                if (isSwiping && swipeDirection.magnitude > swipeThreshold) // Minimum kaydýrma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kaydýrma
                        if (swipeDirection.x > 0)
                        {
                            TryMove(Vector3.right, Quaternion.Euler(0, 90, 0));
                        }
                        else
                        {
                            TryMove(Vector3.left, Quaternion.Euler(0, -90, 0));
                        }
                    }
                    else
                    {
                        // Dikey kaydýrma
                        if (swipeDirection.y > 0)
                        {
                            TryMove(Vector3.forward, Quaternion.Euler(0, 0, 0));
                        }
                        else
                        {
                            TryMove(Vector3.back, Quaternion.Euler(0, 180, 0));
                        }
                    }
                }
                else if (!isSwiping)
                {
                    // Sadece ekrana týklama durumunda ileri gitme
                    TryMove(Vector3.forward, Quaternion.Euler(0, 0, 0));
                }
            }
        }

        // Karakter hareket etmiyorsa animasyon hýzýný sýfýrla
        if (!isMoving && characterAnim != null)
        {
            characterAnim.SetFloat("hiz", 0.0f);
        }
    }

    private void TryMove(Vector3 direction, Quaternion rotation)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Geri gitme sýnýrýný kontrol et
        if (direction == Vector3.back && transform.position.z - moveDistance < maxZPosition - maxBackwardSteps * moveDistance)
        {
            Debug.Log("Geri gitme sýnýrýna ulaþýldý.");
            return;
        }

        // Hedef pozisyonda bir engel olup olmadýðýný kontrol et
        if (!IsObstacleInDirection(direction))
        {
            // Eðer engel yoksa karakteri hedef pozisyona hareket ettir
            StartCoroutine(MoveCharacter(direction, rotation, targetPosition));
        }
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, moveDistance, obstacleLayer))
        {
            Debug.Log("Engel algýlandý: " + hit.collider.name);
            return true;
        }
        return false;
    }

    private IEnumerator MoveCharacter(Vector3 direction, Quaternion rotation, Vector3 targetPosition)
    {
        isMoving = true; // Karakterin hareket ettiðini belirt

        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        if (characterAnim != null)
        {
            characterAnim.SetFloat("hiz", 0.4f);
        }

        // Hareket sesi
        if (moveSound != null)
        {
            moveSound.Play();
        }

        // Hareketi lineer interpolasyonla gerçekleþtir
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            transform.rotation = rotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Nihai pozisyon
        transform.rotation = rotation;

        // Hareket tamamlandý
        isMoving = false;

        // Eðer ileriye doðru hareket ettiyse ve yeni pozisyon en yüksek z pozisyonundan daha büyükse hareket olayýný tetikle
        if (direction == Vector3.forward)
        {
            if (transform.position.z > maxZPosition)
            {
                maxZPosition = transform.position.z;
                currentBackwardSteps = 0; // Geri adým sayýsýný sýfýrla
                OnMoveForward?.Invoke();
            }
        }
        else if (direction == Vector3.back)
        {
            currentBackwardSteps++; // Geri adým sayýsýný artýr
        }

        // Karakter hareket etmiyorsa animasyon hýzýný sýfýrla
        if (!isMoving && characterAnim != null)
        {
            characterAnim.SetFloat("hiz", 0.0f);
        }
    }
}






/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    public Animator characterAnim;
    public Transform character;
    public Transform[] characterList;
    public float moveDistance = 2f; // Karakterin bir adým atýþ mesafesi
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kaydýrma mesafesi
    private bool isSwiping = false; // Kaydýrma hareketini takip etmek için bayrak
    public LayerMask obstacleLayer; // Engellerin bulunduðu katman

    private float maxZPosition; // Karakterin ulaþtýðý en yüksek z pozisyonu
    private bool isMoving = false; // Karakterin hareket edip etmediðini kontrol için bayrak

    // Karakter hareket ettiðinde çaðrýlacak olay
    public static event Action OnMoveForward;

    void Start()
    {
        CharacterSelection characterSelectionScript = transform.Find("Characters").GetComponent<CharacterSelection>();
        int index = characterSelectionScript.index;
        character = characterList[index];
        characterAnim = character.GetComponent<Animator>();

        maxZPosition = transform.position.z; // Baþlangýç z pozisyonu
    }

    void Update()
    {
        isMoving = false; // Her karede hareket bayraðýný sýfýrla

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

                if (isSwiping && swipeDirection.magnitude > swipeThreshold) // Minimum kaydýrma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kaydýrma
                        if (swipeDirection.x > 0)
                        {
                            TryMove(Vector3.right, Quaternion.Euler(0, 90, 0));
                        }
                        else
                        {
                            TryMove(Vector3.left, Quaternion.Euler(0, -90, 0));
                        }
                    }
                    else
                    {
                        // Dikey kaydýrma
                        if (swipeDirection.y > 0)
                        {
                            TryMove(Vector3.forward, Quaternion.Euler(0, 0, 0));
                        }
                        else
                        {
                            TryMove(Vector3.back, Quaternion.Euler(0, 180, 0));
                        }
                    }
                }
                else if (!isSwiping)
                {
                    // Sadece ekrana týklama durumunda ileri gitme
                    TryMove(Vector3.forward, Quaternion.Euler(0, 0, 0));
                }
            }
        }

        // Karakter hareket etmiyorsa animasyon hýzýný sýfýrla
        if (!isMoving && characterAnim != null)
        {
            characterAnim.SetFloat("hiz", 0.0f);
        }
    }

    private void TryMove(Vector3 direction, Quaternion rotation)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Hedef pozisyonda bir engel olup olmadýðýný kontrol et
        if (!IsObstacleInDirection(direction))
        {
            // Eðer engel yoksa karakteri hedef pozisyona hareket ettir
            moveCharacter(direction, rotation);
        }
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, moveDistance, obstacleLayer))
        {
            Debug.Log("Engel algýlandý: " + hit.collider.name);
            return true;
        }
        return false;
    }

    private void moveCharacter(Vector3 direction, Quaternion rotation)
    {
        transform.position += direction * moveDistance;
        transform.rotation = rotation;

        if (characterAnim != null)
        {
            characterAnim.SetFloat("hiz", 0.4f);
        }

        isMoving = true; // Karakter hareket ediyor

        // Eðer ileriye doðru hareket ettiyse ve yeni pozisyon en yüksek z pozisyonundan daha büyükse hareket olayýný tetikle
        if (direction == Vector3.forward && transform.position.z > maxZPosition)
        {
            maxZPosition = transform.position.z;
            OnMoveForward?.Invoke();
        }
    }
}
*/


/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    Animator characterAnim;
    public float moveDistance = 2f; // Karakterin bir ad?m at?? mesafesi
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kayd?rma mesafesi
    private bool isSwiping = false; // Kayd?rma hareketini takip etmek i?in bayrak
    public LayerMask obstacleLayer; // Engellerin bulundu?u katman

    private float maxZPosition; // Karakterin ula?t??? en y?ksek z pozisyonu

    // Karakter hareket etti?inde ?a?r?lacak olay
    public static event Action OnMoveForward;

    void Start()
    {
        characterAnim = GetComponent<Animator>();
        maxZPosition = transform.position.z; // Ba?lang?? z pozisyonu
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

                if (isSwiping && swipeDirection.magnitude > swipeThreshold) // Minimum kayd?rma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kayd?rma
                        if (swipeDirection.x > 0)
                        {
                            TryMove(Vector3.right);
                        }
                        else
                        {
                            TryMove(Vector3.left);
                        }
                    }
                    else
                    {
                        // Dikey kayd?rma
                        if (swipeDirection.y > 0)
                        {
                            TryMove(Vector3.forward);
                        }
                        else
                        {
                            TryMove(Vector3.back);
                        }
                    }
                }
                else if (!isSwiping)
                {
                    // Sadece ekrana t?klama durumunda ileri gitme
                    TryMove(Vector3.forward);
                }
            }
        }
    }

    private void TryMove(Vector3 direction)
    {
        // Hedef pozisyonu belirleme
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Hedef pozisyonda bir engel olup olmad???n? kontrol et
        if (!IsObstacleInDirection(direction))
        {
            // E?er engel yoksa karakteri hedef pozisyona hareket ettir
            moveCharacter(direction);
        }
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, moveDistance, obstacleLayer))
        {
            Debug.Log("Engel alg?land?: " + hit.collider.name);
            return true;
        }
        return false;
    }

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * moveDistance;

        // E?er ileriye do?ru hareket ettiyse ve yeni pozisyon en y?ksek z pozisyonundan daha b?y?kse hareket olay?n? tetikle
        if (direction == Vector3.forward && transform.position.z > maxZPosition)
        {
            maxZPosition = transform.position.z;
            OnMoveForward?.Invoke();
        }
    }
}

*/

/*
public class PlayerTouchMovement : MonoBehaviour
{

    public float moveDistance = 2f; // Karakterin bir ad?m at?? mesafesi
    private Vector3 nextPosition; // Karakterin bir sonraki hedef pozisyonu
    private Vector2 startTouchPosition, endTouchPosition;
    public float swipeThreshold = 50f; // Minimum kayd?rma mesafesi
    private bool isSwiping = false; // Kayd?rma hareketini takip etmek i?in bayrak

    private float maxZPosition; // Karakterin ula?t??? en y?ksek z pozisyonu

    // Karakter hareket etti?inde ?a?r?lacak olay
    public static event Action OnMoveForward;

    void Start()
    {
        nextPosition = transform.position; // Ba?lang??ta karakterin bulundu?u pozisyon
        maxZPosition = transform.position.z; // Ba?lang?? z pozisyonu
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

                if (isSwiping && swipeDirection.magnitude > swipeThreshold) // Minimum kayd?rma mesafesi
                {
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Yatay kayd?rma
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
                        // Dikey kayd?rma
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
                    // Sadece ekrana t?klama durumunda ileri gitme
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

        // Bir sonraki hedef pozisyonunu g?ncelleme
        nextPosition = targetPosition;

        // E?er ileriye do?ru hareket ettiyse ve yeni pozisyon en y?ksek z pozisyonundan daha b?y?kse hareket olay?n? tetikle
        if (direction == Vector3.forward && transform.position.z > maxZPosition)
        {
            maxZPosition = transform.position.z;
            OnMoveForward?.Invoke();
        }
    }
}
*/
