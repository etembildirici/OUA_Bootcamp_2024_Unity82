using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
    private PlayerTouchMovement playerTouchMovementScript;

    private void Start()
    {
        playerTouchMovementScript = GetComponent<PlayerTouchMovement>();

    }

    void Update()
    {
        DetectGrid();
    }

    void DetectGrid()
    {
        // Karakterin altýna doðru bir ray gönderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1f);


        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Grid"))
            {
                playerTouchMovementScript.moveDuration = 0;
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                return;
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    playerTouchMovementScript.moveDuration = 0;
                }
                else
                {
                    playerTouchMovementScript.moveDuration = 0.1f;
                }
                    
                transform.parent = hit.transform;
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.parent = null;
                return;
            }

        }

        transform.SetParent(null);
        playerTouchMovementScript.moveDuration = 0.1f;
    }
}

/*using UnityEngine;

public class GridDetection : MonoBehaviour
{
    private string currentGridID = "";

    void Update()
    {
        DetectSurface();
    }

    void DetectSurface()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1f);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Grid"))
            {
                GridIdentifier gridIdentifier = hit.collider.GetComponent<GridIdentifier>();
                if (gridIdentifier != null && gridIdentifier.gridID != currentGridID)
                {
                    currentGridID = gridIdentifier.gridID;
                    // Burada grid deðiþikliði ile ilgili iþlemler yapýlabilir
                    
                }
                Debug.Log($"Karakter grid'e bindi. Grid ID: {currentGridID}");
                transform.SetParent(hit.transform);
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
                return;
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
            {
                transform.SetParent(hit.transform);
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.SetParent(null);
                currentGridID = "";
                return;
            }
        }

        // Hiçbir yüzey bulunamadýysa
        transform.SetParent(null);
        currentGridID = "";
    }
}

public class GridIdentifier : MonoBehaviour
{
    public string gridID;

    void Awake()
    {
        if (string.IsNullOrEmpty(gridID))
        {
            gridID = System.Guid.NewGuid().ToString();
        }
    }
}

*/
