/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
    private bool isOnGrid = false; // Flag to check if the character is already on a grid
    private bool isOnTile = false; // Flag to check if the character is already on a tile

    void Update()
    {
        DetectSurface();
    }

    void DetectSurface()
    {
        // If the character is already on a grid or tile, do not perform further checks
        if (isOnGrid || isOnTile)
        {
            return;
        }

        // Karakterin altýna doðru bir ray gönderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.CompareTag("Grid"))
            {
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                Debug.Log("Karakter su üzerindeki gridin üzerine bindi.");
                isOnGrid = true; // Character is now on a grid
                isOnTile = false; // Character is not on a tile
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
            {
                Debug.Log("Karakter tile üzerinde.");
                transform.parent = hit.transform;
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.parent = null;
                isOnTile = true; // Character is now on a tile
                isOnGrid = false; // Character is not on a grid
            }
            else
            {
                transform.parent = null;
                isOnGrid = false; // Character is not on a grid
                isOnTile = false; // Character is not on a tile
            }
        }
    }

    // To reset the flags when the character moves off the current surface
    public void ResetSurfaceFlags()
    {
        isOnGrid = false;
        isOnTile = false;
    }
}
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
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
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                Debug.Log("Karakter su üzerindeki gridin üzerine bindi.");
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
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
}
