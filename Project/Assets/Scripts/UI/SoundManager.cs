using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Button soundToggleButton;
    public Image soundIcon;
    public Sprite soundOnIcon; // Ses açık simgesi
    public Sprite soundOffIcon; // Ses kapalı simgesi

    private bool isSoundOn = true;

    private void Start()
    {
        // Başlangıçta sesi açık olarak ayarlayın ve simgeyi gösterin
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundIcon();
        soundToggleButton.onClick.AddListener(ToggleSound);

    }
    
    public void ToggleSound()
    {
        
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSoundIcon();

        // Ses yönetimini burada yapabilirsiniz
        if (isSoundOn)
        {
            // Sesi aç
            AudioListener.volume = 1.0f;
        }
        else
        {
            // Sesi kapat
            AudioListener.volume = 0.0f;
        }
    }

    private void UpdateSoundIcon()
    {
        if (isSoundOn)
        {
            soundIcon.sprite = soundOnIcon;
        }
        else
        {
            soundIcon.sprite = soundOffIcon;
        }
    }
   
}
