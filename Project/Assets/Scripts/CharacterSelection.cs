
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;



    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

       characterList = new GameObject[transform.childCount];    

        for(int i = 0; i < characterList.Length; i++)        
            characterList[i]= transform.GetChild(i).gameObject;
        
        foreach(GameObject go in characterList) 
            go.SetActive(false);

        if (characterList[index])
            characterList[index].SetActive(true);
    }
    public void ToggleLeft()
    {
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(true);

    }
    public void ToggleRight()
    {
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;

        characterList[index].SetActive(true);

    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Level");
    }

}
