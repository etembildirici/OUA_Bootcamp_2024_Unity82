using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    private int leftSteps = 0;
    private int rightSteps = 0;

    private void Start()
    {

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (moveDirection.sqrMagnitude > 0.0024f)
            {
                var desiredRotation = Quaternion.LookRotation(Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 60);
            }
            moveCharacter(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (leftSteps <= 11)
            {
                leftSteps++;
                rightSteps--;
                if (moveDirection.sqrMagnitude > 0.001f)
                {
                    var desiredRotation = Quaternion.LookRotation(Vector3.left);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 60);
                }
                moveCharacter(Vector3.left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (rightSteps <= 11)
            {
                leftSteps--;
                rightSteps++;
                if (moveDirection.sqrMagnitude > 0.002f)
                {
                    var desiredRotation = Quaternion.LookRotation(Vector3.right);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 60);
                }
                moveCharacter(Vector3.right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveDirection.sqrMagnitude > 0.00f)
            {
                var desiredRotation = Quaternion.LookRotation(Vector3.back);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 60);
            }
            moveCharacter(Vector3.back);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            transform.parent = other.transform;
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
            Debug.Log("Karakter su üzerindeki gridin üzerine bindi.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            transform.parent = null;
            Debug.Log("Karakter su üzerindeki gridin üzerinden ayrýldý.");
        }
    }

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * 2f;
    }
}
