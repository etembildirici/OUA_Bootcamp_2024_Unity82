using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;

public class ScoreManager : MonoBehaviour
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
        scoreText.text = "Score: " + score;
        maxScoreText.text = "Max: " + maxScore;
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
}



