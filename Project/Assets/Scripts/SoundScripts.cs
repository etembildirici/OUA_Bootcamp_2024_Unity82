using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Button toggleButton;

    private static MusicManager instance;
    private bool isMusicPlaying = true;

    // M�zik �almas� gereken sahne adlar�n� buraya ekleyin
    public string[] scenesWithMusic;

    void Awake()
    {
        // Singleton tasar�m� ile bu objeyi sahneler aras�nda koru
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // E�er toggleButton atand�ysa, butona t�klama olay�n� ba�layal�m
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleMusic);
        }

        // M�zik �alma durumunu kontrol et
        UpdateMusicState();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Selection" && SceneManager.GetActiveScene().name== "Level")
        {
            DontDestroyOnLoad (gameObject);
        }
    }
    void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;
        UpdateMusicState();
    }

    void UpdateMusicState()
    {
        if (musicSource == null) return;

        if (isMusicPlaying)
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            if (musicSource.isPlaying)
            {
                musicSource.Pause();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahne y�klendi�inde m�zik durumunu g�ncelle
        // Sahne ad�n�n m�zik �alacak sahneler aras�nda olup olmad���n� kontrol et
        if (IsMusicEnabledForScene(scene.name))
        {
            UpdateMusicState();
        }
        else
        {
            // M�zik �almayacaksa durdur
            musicSource.Stop();
        }
    }

    bool IsMusicEnabledForScene(string sceneName)
    {
        foreach (string scene in scenesWithMusic)
        {
            if (scene == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    void OnEnable()
    {
        // Yeni sahne y�klendi�inde m�zik durumunu g�ncellemek i�in olay dinleyiciyi ekle
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Bu objenin devre d��� b�rak�lmas� durumunda olay dinleyiciyi kald�r
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


