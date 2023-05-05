using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    // public GameObject[] characters;
    // public int selectedCharacter = 0;
    public bool isReady = false;
    LevelManager levelManager;

    [Header("SkillPoint related stuff")]
    [SerializeField] PlayerStats playerStats;
    public Slider playerSpeed;
    public Slider projectileSpeed;
    public Slider playerSize;
    //If more sliders are created some other stuff needs to be done. Like making more textmesh pro stuff and so on. just dont add any without letting me know.--connall
    [Header("Text changing stuff")]
    [SerializeField] TextMeshProUGUI skillpointsnumber;
    [SerializeField] TextMeshProUGUI playerSpeedNumberdisplay;
    [SerializeField] TextMeshProUGUI playerSizeNumberdisplay;

    [SerializeField] TextMeshProUGUI projectileSpeedNumberdisplay;



    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        DisplayThingsOnUI();
    }
    void DisplayThingsOnUI()
    {
        skillpointsnumber.text = "You have " + playerStats.GetSkillPointsNumber() + " left";
        playerSpeedNumberdisplay.text = "You have used " + (playerStats.playerSpeedSkillPointsSpent - 1) + " for player speed.";
        projectileSpeedNumberdisplay.text = "You have used " + (playerStats.projectileSpeedSkillPointsSpent - 1) + " for bullet speed.";
        playerSizeNumberdisplay.text = "You have used " + (playerStats.playerSizeSkillPointsSpent - 1) + " for player size.";
    }
    // public void NextCharacter()
    // {
    //     characters[selectedCharacter].SetActive(false);
    //     selectedCharacter = (selectedCharacter + 1) % characters.Length;
    //     characters[selectedCharacter].SetActive(true);
    // }
    // public void PreviousCharacter()
    // {
    //     characters[selectedCharacter].SetActive(false);
    //     selectedCharacter--;
    //     if (selectedCharacter < 0)
    //     {
    //         selectedCharacter += characters.Length;
    //     }
    //     characters[selectedCharacter].SetActive(true);
    // }

    public void IsReadyToGo()
    {
        if (playerStats.GetSkillPointsNumber() >= 0)
        {
            isReady = true;
            Debug.Log("Ready");
        }
        else
        {
            Debug.Log("Too many skill points used");
        }

    }


    // public void StartGame()
    // {
    //     PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    //     // PlayerPrefs.SetInt("selectedCharacterPlayerTwo", selectedCharacterPlayerTwo);
    //     SceneManager.LoadScene("Game");
    // }
}
