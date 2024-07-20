using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement3 : MonoBehaviour
{
    Animator characterAnim;

    private void Start()
    {
        characterAnim = GetComponent<Animator>();
    }

    private void Update()
    {
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
