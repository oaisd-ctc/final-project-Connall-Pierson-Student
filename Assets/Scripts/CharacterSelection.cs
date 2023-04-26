using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    // public GameObject[] charactersPlayerTwo;
    // public int selectedCharacterPlayerTwo = 0;
    // [SerializeField] bool isPlayer1;
    // [SerializeField] bool isPlayer2;
    public bool isReady = false;
    // public bool isReadyPlayer2 = false;
    LevelManager levelManager;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }
    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }
    
    public void IsReadyToGo()
    {
        isReady = true;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        // PlayerPrefs.SetInt("selectedCharacterPlayerTwo", selectedCharacterPlayerTwo);
        SceneManager.LoadScene("Game");
    }
}
