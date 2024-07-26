using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    Animator characterAnim;

    private void Start()
    {
        // Assume the child GameObject with the Animator component is named "Character"
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

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveCharacter(Vector3.forward);
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveCharacter(Vector3.left);
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveCharacter(Vector3.right);
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveCharacter(Vector3.back);
            characterAnim.SetFloat("hiz", 0.4f);
        }
        else
        {
            characterAnim.SetFloat("hiz", 0.0f);
        }
    }

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * 2f;
    }
}

