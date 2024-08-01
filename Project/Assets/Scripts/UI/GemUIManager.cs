using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemUIManager : MonoBehaviour
{
    public TextMeshProUGUI gemCountText;
    private int gemCount;

    private void Start()
    {
        // Önceden kaydedilmiş gem sayısını al
        gemCount = PlayerPrefs.GetInt("GemCount", 99);
        UpdateUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateUI();

        // Gem sayısını kaydet
        PlayerPrefs.SetInt("GemCount", gemCount);
    }

    private void UpdateUI()
    {
        gemCountText.text = " " + gemCount;
    }
}