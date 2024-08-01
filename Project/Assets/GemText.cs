using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemText : MonoBehaviour
{
    public TextMeshProUGUI gemCountText;
    int gemCountLast;

    void Start()
    {
        gemCountLast = PlayerPrefs.GetInt("GemCount", 300);
        gemCountText.text = " " + gemCountLast;
        
    }

    private void Update()
    {
        UpdateGemCount();
    }

    public void UpdateGemCount()
    {
        gemCountLast = PlayerPrefs.GetInt("GemCount", 300);
        gemCountText.text = " " + gemCountLast;
    }
}
