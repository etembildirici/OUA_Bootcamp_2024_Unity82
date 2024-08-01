using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Button toggleButton;

    private static MusicManager instance;
    private bool isMusicPlaying = true;

    // Müzik çalmasý gereken sahne adlarýný buraya ekleyin
    public string[] scenesWithMusic;

    void Awake()
    {
        // Singleton tasarýmý ile bu objeyi sahneler arasýnda koru
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
        // Eðer toggleButton atandýysa, butona týklama olayýný baðlayalým
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleMusic);
        }

        // Müzik çalma durumunu kontrol et
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
        // Sahne yüklendiðinde müzik durumunu güncelle
        // Sahne adýnýn müzik çalacak sahneler arasýnda olup olmadýðýný kontrol et
        if (IsMusicEnabledForScene(scene.name))
        {
            UpdateMusicState();
        }
        else
        {
            // Müzik çalmayacaksa durdur
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
        // Yeni sahne yüklendiðinde müzik durumunu güncellemek için olay dinleyiciyi ekle
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Bu objenin devre dýþý býrakýlmasý durumunda olay dinleyiciyi kaldýr
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


