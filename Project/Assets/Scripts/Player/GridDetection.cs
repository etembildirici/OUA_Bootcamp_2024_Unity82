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
        // Karakterin alt�na do�ru bir ray g�nderin
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1f);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Grid"))
            {
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                Debug.Log("Karakter su �zerindeki gridin �zerine bindi.");
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

