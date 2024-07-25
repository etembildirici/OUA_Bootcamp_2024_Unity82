using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefab;
    public Transform spawnPoint;
    public TMP_Text label;
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefab = characterPrefab[selectedCharacter];
        GameObject clone = Instantiate(prefab,spawnPoint.position,Quaternion.identity);
        label.text = prefab.name;
    }

    
}
