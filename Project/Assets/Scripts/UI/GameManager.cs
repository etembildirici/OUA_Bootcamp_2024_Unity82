using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro'yu kullanmak için ekleyin


public class GameManager : MonoBehaviour
{
    public GameObject restartButton; // Yeniden başlama butonu
    public GameObject pauseResumeButton;
    public TextMeshProUGUI maxScoreText;
    public GameObject characterButton;
    public GameObject soundButton;
    public PlayerTouchMovement playerTouchMovement;


    //public string characterSelectionSceneName = "Selection"; // Karakter değiştirme sahnesinin adı

    

    private void Start()
    {
        restartButton.SetActive(false); // Oyun başlarken butonu gizle
        maxScoreText.gameObject.SetActive(false);
        characterButton.SetActive(true);
        soundButton.SetActive(true);
    }
    public void GoToCharacterSelection()
    {
       
        SceneManager.LoadScene("Selection");
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = false;

            // Belirli bir süre sonra veya belirli bir koşul altında hareketi yeniden etkinleştirin
            Invoke("EnablePlayerMovement", 0.5f); // Örneğin, 0.5 saniye sonra
        }

    }
    private void EnablePlayerMovement()
    {
        if (playerTouchMovement != null)
        {
            playerTouchMovement.enabled = true;
        }
    }
   
    public void EndGame()
    {
        //Time.timeScale = 0; // Oyunu durdur
        restartButton.SetActive(true); // Yeniden başlama butonunu göster
        pauseResumeButton.gameObject.SetActive(false);
        maxScoreText.gameObject.SetActive(true);
        characterButton.SetActive(false);
        soundButton.SetActive(false);

    }

    public void RestartGame()
    {
        //Time.timeScale = 1; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi yeniden yükle
    }
}

/*public class GameManager : MonoBehaviour
{
    public GameObject restartButton; // Yeniden başlama butonu
    public GameObject pauseResumeButton;
    public TextMeshProUGUI maxScoreText;
   

    //public string characterSelectionSceneName = "Selection"; // Karakter değiştirme sahnesinin adı

    public void GoToCharacterSelection()
    {
        SceneManager.LoadScene("Selection");
    }


    private void Start()
    {
        restartButton.SetActive(false); // Oyun başlarken butonu gizle
        maxScoreText.gameObject.SetActive(false);
       
    }

    public void EndGame()
    {
        //Time.timeScale = 0; // Oyunu durdur
        restartButton.SetActive(true); // Yeniden başlama butonunu göster
        pauseResumeButton.gameObject.SetActive(false);
        maxScoreText.gameObject.SetActive(true);
      

    }

    public void RestartGame()
    {
        //Time.timeScale = 1; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi yeniden yükle
    }
}*/
