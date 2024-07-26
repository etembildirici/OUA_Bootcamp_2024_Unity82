using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
    private Animator characterAnim;

    void Start()
    {
        // Karakterin altýndaki "RogueHooded" GameObject'inde Animator bileþenini bul
        Transform rogueHoodedTransform = transform.Find("RogueHooded");
        if (rogueHoodedTransform != null)
        {
            characterAnim = rogueHoodedTransform.GetComponent<Animator>();
            if (characterAnim == null)
            {
                Debug.LogError("Animator bileþeni 'RogueHooded' GameObject'inde bulunamadý.");
            }
        }
        else
        {
            Debug.LogError("RogueHooded adlý alt GameObject bulunamadý.");
        }
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

        bool hitGrid = false;

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Grid"))
            {
                // Grid'e dokundu
                hitGrid = true;
                transform.parent = hit.transform;
                transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Karakteri gridin merkezine ve y=0.5 konumuna ayarla
                Debug.Log("Karakter gridin üzerine bindi.");

                // Ölüm animasyonunu tetikleyin
                if (characterAnim != null)
                {
                    characterAnim.SetTrigger("Death");
                }
                break; // Grid'e çarptýðýnda baþka bir etiket kontrolüne gerek yok
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
            {
                // Diðer etiketler için transform ayarlarý
                transform.parent = hit.transform;
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.parent = null;
            }
            else
            {
                transform.parent = null;
            }
        }

        // Eðer grid'e dokunulmamýþsa, parent'ý null yap
        if (!hitGrid)
        {
            transform.parent = null;
        }
    }
}

