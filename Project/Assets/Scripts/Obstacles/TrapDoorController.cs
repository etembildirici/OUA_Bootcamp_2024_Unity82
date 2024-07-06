using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTrapDoorController : MonoBehaviour
{
    public Animator trapDoorAnim; // TrapDoor için Animator
    public float openCloseDelay = 2f; // Açılıp kapanma arasındaki varsayılan gecikme süresi
    public float startDelay = 0f; // Başlangıç gecikmesi süresi

    void Awake()
    {
        trapDoorAnim = GetComponent<Animator>();

        // Rastgele bir sayı oluştur
        float randomValue = Random.value;

        if (randomValue <= 0.2f) // %20 ihtimalle tamamen kapalı
        {
            CloseTrap();
        }
        else if (randomValue <= 0.6f) // %40 ihtimalle başlangıçta belirli bir süre gecikme
        {
            StartCoroutine(StartWithDelay());
        }
        else // %40 ihtimalle normal çalışma
        {
            StartCoroutine(OpenCloseTrap());
        }
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        while (true)
        {
            OpenTrap();
            yield return new WaitForSeconds(openCloseDelay);
            CloseTrap();
            yield return new WaitForSeconds(openCloseDelay);
        }
    }

    void OpenTrap()
    {
        trapDoorAnim.SetTrigger("open");
    }

    void CloseTrap()
    {
        trapDoorAnim.SetTrigger("close");
    }
}