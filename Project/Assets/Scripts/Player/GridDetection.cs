using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetection : MonoBehaviour
{
    private Animator characterAnim;

    void Start()
    {
        // Karakterin alt�ndaki "RogueHooded" GameObject'inde Animator bile�enini bul
        Transform rogueHoodedTransform = transform.Find("RogueHooded");
        if (rogueHoodedTransform != null)
        {
            characterAnim = rogueHoodedTransform.GetComponent<Animator>();
            if (characterAnim == null)
            {
                Debug.LogError("Animator bile�eni 'RogueHooded' GameObject'inde bulunamad�.");
            }
        }
        else
        {
            Debug.LogError("RogueHooded adl� alt GameObject bulunamad�.");
        }
    }

    void Update()
    {
        DetectGrid();
    }

    void DetectGrid()
    {
        // Karakterin alt�na do�ru bir ray g�nderin
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
                Debug.Log("Karakter gridin �zerine bindi.");

                // �l�m animasyonunu tetikleyin
                if (characterAnim != null)
                {
                    characterAnim.SetTrigger("Death");
                }
                break; // Grid'e �arpt���nda ba�ka bir etiket kontrol�ne gerek yok
            }
            else if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Door"))
            {
                // Di�er etiketler i�in transform ayarlar�
                transform.parent = hit.transform;
                transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                transform.parent = null;
            }
            else
            {
                transform.parent = null;
            }
        }

        // E�er grid'e dokunulmam��sa, parent'� null yap
        if (!hitGrid)
        {
            transform.parent = null;
        }
    }
}

