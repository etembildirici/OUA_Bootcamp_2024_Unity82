using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    // Animator referanslarý
    private Animator rogueHoodedAnim;
    private Animator mageAnim;
    private Animator barbarianAnim;
    private Animator knightAnim;

    // Aktif karakteri belirlemek için bir deðiþken
    private int activeCharacterIndex;

    private void Start()
    {
        // Karakterlerin Animator komponentlerini bul
        rogueHoodedAnim = transform.Find("RogueHooded")?.GetComponent<Animator>();
        mageAnim = transform.Find("Mage")?.GetComponent<Animator>();
        barbarianAnim = transform.Find("Barbarian")?.GetComponent<Animator>();
        knightAnim = transform.Find("Knight")?.GetComponent<Animator>();

        // Eðer bir karakter bulunamazsa hata mesajý ver
        if (rogueHoodedAnim == null || mageAnim == null || barbarianAnim == null || knightAnim == null)
        {
            Debug.LogError("Bir veya daha fazla karakter GameObject'i bulunamadý. Lütfen tüm karakterlerin adlarýný kontrol edin.");
        }

        // Seçilen karakterin index deðerini almak için PlayerPrefs kullan
        activeCharacterIndex = PlayerPrefs.GetInt("CharacterSelected", 0);
    }

    private void Update()
    {
        // Aktif karakterin Animator'ýný seç
        Animator characterAnim = GetActiveCharacterAnimator();
        if (characterAnim == null)
        {
            Debug.LogError("Aktif karakterin Animator'ý bulunamadý.");
            return;
        }

        // Karakter hareketi ve animasyon güncellemesi
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveCharacter(Vector3.forward, Quaternion.Euler(0, 0, 0));
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveCharacter(Vector3.left, Quaternion.Euler(0, -90, 0));
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveCharacter(Vector3.right, Quaternion.Euler(0, 90, 0));
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveCharacter(Vector3.back, Quaternion.Euler(0, 180, 0));
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else
        {
            characterAnim.SetFloat("hiz", 0.0f);
        }
    }

    private Animator GetActiveCharacterAnimator()
    {
        // Aktif karakter index'ine göre doðru Animator'ý döndür
        switch (activeCharacterIndex)
        {
            case 0: // RogueHooded
                return rogueHoodedAnim;
            case 1: // Mage
                return mageAnim;
            case 2: // Barbarian
                return barbarianAnim;
            case 3: // Knight
                return knightAnim;
            default:
                return null;
        }
    }

    private void moveCharacter(Vector3 direction, Quaternion rotation)
    {
        // Karakteri hareket ettir
        transform.position += direction * 2f;

        // Karakteri döndür
        transform.rotation = rotation;
    }
}




