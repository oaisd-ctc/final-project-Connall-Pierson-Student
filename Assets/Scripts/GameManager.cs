using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform playerOneSpawnPoint;
    [SerializeField] Transform playerTwoSpawnPoint;

    [Header("Player Controllers")]
    [SerializeField] PlayerController playerOne;
    [SerializeField] PlayerController playerTwo;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI broadcastText;

    [Header("Variables")]
    [SerializeField] float delayBetweenRounds = 2f;
    [SerializeField] float delayToMainMenu = 2f;
    [SerializeField] int scoreToWin = 3;

    [HideInInspector] public int playerOneScore = 0;
    [HideInInspector] public int playerTwoScore = 0;


    void Awake() 
    {
        scoreText.text = playerOneScore + " - " + playerTwoScore;
        broadcastText.text = "";
    }
    void Start()
    {
        playerOne.gameObject.transform.position = playerOneSpawnPoint.position;
        playerTwo.gameObject.transform.position = playerTwoSpawnPoint.position;
    }
    public void WinRound(PlayerController player)
    {
        if (player.playerNum == 1)
        {
            playerOneScore++;
            if (playerOneScore >= scoreToWin)
            {
                broadcastText.text = "Player One Wins!";
                Invoke("LoadMainMenu", delayToMainMenu);
            }
            else
            {
                broadcastText.text = "Player One Won This Round!";
                Invoke("NextRound", delayBetweenRounds);
            }
        }
        else
        {
            playerTwoScore++;
            if (playerTwoScore >= scoreToWin)
            {
                broadcastText.text = "Player Two Wins!";
                Invoke("LoadMainMenu", delayToMainMenu);
            }
            else
            {
                broadcastText.text = "Player Two Won This Round!";
                Invoke("NextRound", delayBetweenRounds);
            }
        }
        scoreText.text = playerOneScore + " - " + playerTwoScore;
    }
    
    void LoadMainMenu()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadMainMenu();
    }
    void NextRound()
    {
        broadcastText.text = "";
        RespawnPlayers();
    }

    void RespawnPlayers()
    {
        playerOne.Respawn(playerOneSpawnPoint);
        playerTwo.Respawn(playerTwoSpawnPoint);
    }
}
