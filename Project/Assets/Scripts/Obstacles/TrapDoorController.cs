﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTrapDoorController : MonoBehaviour
{
    public Animator trapDoorAnim; // TrapDoor için Animator
    public float openCloseDelay = 2f; // Açılıp kapanma arasındaki varsayılan gecikme süresi
    public float startDelay = 0f; // Başlangıç gecikmesi süresi
    public float trapCloseProbability = 0.3f; // Trapdoor kapalı kalma olasılığı
    public float trapDelayProbability = 0.5f; // Başlangıçta gecikme olasılığı

    void Awake()
    {
        trapDoorAnim = GetComponent<Animator>();

        // Rastgele bir sayı oluştur
        float randomValue = Random.value;

        if (randomValue <= trapCloseProbability) // Kapalı kalma olasılığına göre kontrol
        {
            CloseTrap();
        }
        else // Açık kalırsa başlangıçta gecikme olasılığına göre kontrol
        {
            if (Random.value <= trapDelayProbability)
            {
                StartCoroutine(StartWithDelay());
            }
            else
            {
                StartCoroutine(OpenCloseTrap());
            }
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
