using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    Animator characterAnim;

    private void Start()
    {
        // Assume the child GameObject with the Animator component is named "RogueHooded"
        Transform characterTransform = transform.Find("RogueHooded");
        if (characterTransform != null)
        {
            characterAnim = characterTransform.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Character GameObject not found. Make sure it is a child of the Player GameObject.");
        }
    }

    private void Update()
    {
        if (characterAnim == null)
        {
            Debug.LogError("Animator component not found on the Character GameObject.");
            return;
        }

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

    private void moveCharacter(Vector3 direction, Quaternion rotation)
    {
        // Move the character
        transform.position += direction * 2f;

        // Rotate the character
        transform.rotation = rotation;
    }
}


