using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    public Button buyButton;
    public Button selectButton;
    public TextMeshProUGUI buyButtonText;
    private int[] characterPrices = { 0, 20,1, 400 }; // Her karakterin fiyatý
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected", 0);
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < characterList.Length; i++)
            characterList[i] = transform.GetChild(i).gameObject;
        foreach (GameObject go in characterList)
            go.SetActive(false);
        if (characterList[index])
            characterList[index].SetActive(true);

        UpdateButtons();
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
        UpdateButtons();
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);
        index++;
        if (index == characterList.Length)
            index = 0;
        characterList[index].SetActive(true);
        UpdateButtons();
    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Level");
    }

    public void BuyingButton()
    {
        int gems = PlayerPrefs.GetInt("GemCount", 0);
        if (gems >= characterPrices[index] && !IsCharacterOwned(index))
        {
            gems -= characterPrices[index];
            PlayerPrefs.SetInt("GemCount", gems);
            PlayerPrefs.SetInt("CharacterOwned" + index, 1);
            UpdateButtons();
        }
    }

    private bool IsCharacterOwned(int characterIndex)
    {
        return PlayerPrefs.GetInt("CharacterOwned" + characterIndex, 0) == 1;
    }

    private void UpdateButtons()
    {
        if (IsCharacterOwned(index) || characterPrices[index] == 0)
        {
            buyButton.interactable = false;
            buyButtonText.text = "-";
            selectButton.interactable = true;
            selectButton.image.color = Color.white;
        }
        else
        {
            buyButton.interactable = true;
            buyButtonText.text = characterPrices[index].ToString();
            selectButton.interactable = false;
            selectButton.image.color = Color.gray;
        }
    }
}

