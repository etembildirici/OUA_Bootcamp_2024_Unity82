using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro'yu kullanmak için ekleyin

public class GameManager : MonoBehaviour
{
    public GameObject restartButton; // Yeniden başlama butonu

    private void Start()
    {
        restartButton.SetActive(false); // Oyun başlarken butonu gizle
    }

    public void EndGame()
    {
        Time.timeScale = 0; // Oyunu durdur
        restartButton.SetActive(true); // Yeniden başlama butonunu göster
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi yeniden yükle
    }
}
