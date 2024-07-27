
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public Image characterImage;
    public Sprite[] characterSprites;
    private int currentIndex = 0;

    void Start()
    {
        UpdateCharacterImage();
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characterSprites.Length)
        {
            currentIndex = 0;
        }
        UpdateCharacterImage();
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = characterSprites.Length - 1;
        }
        UpdateCharacterImage();
    }

    void UpdateCharacterImage()
    {
        characterImage.sprite = characterSprites[currentIndex];
    }

    public void OnStartGame()
    {
        GameManager.Instance.SelectCharacter(characterSprites[currentIndex].name);
        SceneManager.LoadScene("GameScene");
    }
}
