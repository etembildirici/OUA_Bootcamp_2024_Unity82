using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement3 : MonoBehaviour
{
    private void Start()
    {
        // Gerekli baþlangýç ayarlarý yapýlabilir
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveCharacter(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveCharacter(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveCharacter(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveCharacter(Vector3.back);
        }
        DetectTile();
    }

    void DetectTile()
    {
        // Karakterin altýna doðru bir ray gönderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray,1f);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Grid"))
            {
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                Debug.Log("Karakter su üzerindeki gridin üzerine bindi.");
            }
            else if (hit.collider.CompareTag("Tile"))
            {
                transform.parent = hit.transform;
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.parent = null;
            }
            else
            {
                transform.parent = null;
            }
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Grid"))
        {
            transform.parent = collision.transform;
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
            Debug.Log("Karakter su üzerindeki gridin üzerine bindi.");
        }
        else if (collision.collider.CompareTag("Tile"))
        {
            transform.parent = collision.transform;
            transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z);
            transform.parent = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Grid"))
        {
            transform.parent = null;
            Debug.Log("Karakter su üzerindeki gridin üzerinden ayrýldý.");
        }
    }*/

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * 2f;
    }
    /*
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

            moveCharacter(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            moveCharacter(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            moveCharacter(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

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
            transform.parent = other.transform;
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
            transform.parent = null;
            Debug.Log("Karakter su üzerindeki gridin üzerinden ayrýldý.");
        }
    }

    private void moveCharacter(Vector3 direction)
    {
        transform.position += direction * 2f;
    }
    */
}
