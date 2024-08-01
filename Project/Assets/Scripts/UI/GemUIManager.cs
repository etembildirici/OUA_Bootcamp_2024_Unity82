using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemUIManager : MonoBehaviour
{
    public TextMeshProUGUI gemCountText;
    public int gemCount;


    private void Start()
    {
        // Önceden kaydedilmiş gem sayısını al
        gemCount = PlayerPrefs.GetInt("GemCount", 0);
        UpdateUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        gemCountText.text = " " + gemCount;
    }
}
