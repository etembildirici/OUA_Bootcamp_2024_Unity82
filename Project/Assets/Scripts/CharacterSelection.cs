
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);  
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter - 1].SetActive(false);
        if (selectedCharacter<0)
            selectedCharacter +=characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {  
        PlayerPrefs.SetInt("selectedCharacter",selectedCharacter );
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    
    }
}
