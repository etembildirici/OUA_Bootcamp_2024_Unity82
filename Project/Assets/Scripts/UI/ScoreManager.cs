using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;

/*public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public Button pauseResumeButton;
    public TextMeshProUGUI buttonText;
    public PlayerTouchMovement playerTouchMovement; // PlayerTouchMovement referansı

    private int score;
    private int maxScore;
    private bool isPaused;

    private void OnEnable()
    {
        PlayerTouchMovement.OnMoveForward += MoveForwardHandler;
    }

    private void OnDisable()
    {
        PlayerTouchMovement.OnMoveForward -= MoveForwardHandler;
    }

    private void Start()
    {
        score = 0;
        maxScore = PlayerPrefs.GetInt("Max", 0);
        isPaused = false;
        UpdateUI();

        // Butonun başlangıç metnini ayarlayın
        buttonText.text = " ";
    }

    private void MoveForwardHandler()
    {
        score++;
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("Max", maxScore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = " " + score;
        maxScoreText.text = "Max  " + maxScore;
    }

    public void TogglePauseResume()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        buttonText.text = " ";

        // PlayerTouchMovement scriptini devre dışı bırak
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = false;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        buttonText.text = " ";

        // PlayerTouchMovement scriptini etkinleştir
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = true;
        }
    }
}*/

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public Button pauseResumeButton;
    public Image icon; // Butonun simgesi için Image bileşeni
    public Sprite pauseIcon; // Pause simgesi
    public Sprite resumeIcon; // Resume simgesi
    public PlayerTouchMovement playerTouchMovement; // PlayerTouchMovement referansı

    private int score;
    private int maxScore;
    private bool isPaused;

    private void OnEnable()
    {
        PlayerTouchMovement.OnMoveForward += MoveForwardHandler;
    }

    private void OnDisable()
    {
        PlayerTouchMovement.OnMoveForward -= MoveForwardHandler;
    }

    private void Start()
    {
        score = 0;
        maxScore = PlayerPrefs.GetInt("Max", 0);
        isPaused = false;
        UpdateUI();

        // Başlangıçta pause simgesini ayarla
        icon.sprite = pauseIcon;
        pauseResumeButton.gameObject.SetActive(false); // Başlangıçta butonu gizle
    }

    private void MoveForwardHandler()
    {
        if (!pauseResumeButton.gameObject.activeInHierarchy)
        {
            pauseResumeButton.gameObject.SetActive(true); // Oyuncu hareket ettiğinde butonu göster
        }

        score++;
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("Max", maxScore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = " " + score;
        maxScoreText.text = "Top " + maxScore;
    }

    public void TogglePauseResume()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        icon.sprite = resumeIcon; // Resume simgesini göster

        // PlayerTouchMovement scriptini devre dışı bırak
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = false;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        icon.sprite = pauseIcon; // Pause simgesini göster

        // PlayerTouchMovement scriptini etkinleştir
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = true;
        }
    }

    /*public void GameOver()
    {
        pauseResumeButton.gameObject.SetActive(false); // Oyun bittiğinde butonu gizle
    }*/
}

