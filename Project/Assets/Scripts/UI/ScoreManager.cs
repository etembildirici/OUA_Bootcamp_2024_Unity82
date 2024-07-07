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

    private int score;
    private int maxScore;
    private bool isPaused;

    private void OnEnable()
    {
        PlayerMovement2.OnMove += MoveHandler;
    }

    private void OnDisable()
    {
        PlayerMovement2.OnMove -= MoveHandler;
    }

    private void Start()
    {
        score = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        isPaused = false;
        UpdateUI();

        // Butonun başlangıç metnini ayarlayın
        buttonText.text = "Pause";
    }

    private void MoveHandler()
    {
        score++;
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        maxScoreText.text = "Max Score: " + maxScore;
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
        buttonText.text = "Resume";
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        buttonText.text = "Pause";
    }
}



