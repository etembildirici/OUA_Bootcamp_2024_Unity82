using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 nextDir;
    Rigidbody rb;
    public float JumpForce = 100;
    public Vector3 curPosition;
    public float speed = 5;
    public float speedRot = 100;
    public float rotationOffset;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curPosition = transform.position;
    }
    void Update()
    {
        if (transform.position != new Vector3(curPosition.x, transform.position.y, curPosition.z) + nextDir)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(curPosition.x, transform.position.y, curPosition.z) + nextDir, speed * Time.deltaTime);

        }
        else
        {
            nextDir = Vector3.zero;
            curPosition = transform.position;
            curPosition.x = Mathf.Round(curPosition.x);
            curPosition.z = Mathf.Round(curPosition.z);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            nextDir.z = -Input.GetAxisRaw("Horizontal");
            Move();
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            nextDir.x = Input.GetAxisRaw("Vertical");
            Move();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            Debug.Log("Player collided with obstacle!");

            // Oyunu yeniden baþlat
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("Water"))
        {
            Debug.Log("Player collided with obstacle!");

            // Oyunu yeniden baþlat
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Move()
    {
        if (IsGrounded())  // Eðer karakter yerdeyse, yalnýzca o zaman zýplama kuvveti uygula
        {
            rb.AddForce(0, JumpForce, 0);
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float distanceToGround = 0.1f;  // Raycast mesafesi, yerin ne kadar altýnda olduðuna baðlý olarak ayarlanmalýdýr.

        // Raycast'i atýp karakterin yerde olup olmadýðýný kontrol etmek için aþaðýdaki satýrý kullanabilirsiniz.
        bool grounded = Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround);

        return grounded;
    }
}
