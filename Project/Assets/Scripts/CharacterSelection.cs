using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < characterList.Length; i++)
            characterList[i] = transform.GetChild(i).gameObject;
        foreach (GameObject go in characterList)
            go.SetActive(false);
        if (characterList[index])
            characterList[index].SetActive(true);
    }

    void Update()
    {
        // Sadece "Selection" sahnesindeyken kaydýrma iþlemini kontrol et
        if (SceneManager.GetActiveScene().name == "Selection")
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position;
                    DetectSwipe();
                }
            }
        }
    }

    void DetectSwipe()
    {
        float swipeDistance = (endTouchPosition - startTouchPosition).magnitude;
        if (swipeDistance > swipeThreshold)
        {
            float swipeDirection = endTouchPosition.x - startTouchPosition.x;
            if (swipeDirection > 0)
            {
                ToggleRight();
            }
            else
            {
                ToggleLeft();
            }
        }
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

/*
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

        for (int i = 0; i < characterList.Length; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in characterList)
            go.SetActive(false);

        if (characterList[0])
            characterList[0].SetActive(true);
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
*/




