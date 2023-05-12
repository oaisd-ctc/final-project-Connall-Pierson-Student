using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSelectionScript : MonoBehaviour
{
    public CharacterDataBase characterDB;
    public Image artworkSprite;
    private int selectedOption = 0;
    PlayerStats playerOne;
    PlayerStats playerTwo;
    void Awake() 
    {
        playerOne = FindObjectsOfType<PlayerStats>()[0];
        playerTwo = FindObjectsOfType<PlayerStats>()[1];
    }
    void Start()
    {
if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.charcterSprite;
    }
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
