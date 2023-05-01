using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform playerOneSpawnPoint;
    [SerializeField] Transform playerTwoSpawnPoint;
    [SerializeField] PlayerController playerOne;
    [SerializeField] PlayerController playerTwo;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI broadcastText;
    [HideInInspector] public int playerOneScore = 0;
    [HideInInspector] public int playerTwoScore = 0;


    void Awake() 
    {
        scoreText.text = playerOneScore + " - " + playerTwoScore;
        broadcastText.text = "";
    }
    public void Win(PlayerController player)
    {
        if (player.playerNum == 1)
        {
            playerOneScore++;
            if (playerOneScore >= 11)
            {
                broadcastText.text = "Player One Wins!";
                Invoke("LevelManager.LoadMainMenu", 2f);
                return;
            }
            else
            {
                broadcastText.text = "Player One Won This Round!";
                Invoke("NextRound", 2f);
            }
        }
        else
        {
            playerTwoScore++;
            if (playerTwoScore >= 11)
            {
                broadcastText.text = "Player Two Wins!";
                Invoke("LevelManager.LoadMainMenu", 2f);
                return;
            }
            else
            {
                broadcastText.text = "Player Two Won This Round!";
                Invoke("NextRound", 2f);
            }
        }
        scoreText.text = playerOneScore + " - " + playerTwoScore;
    }

    public void NextRound()
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
