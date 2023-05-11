using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
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

    MapManager mapManager;


    void Awake() 
    {
        scoreText.text = playerOneScore + " - " + playerTwoScore;
        broadcastText.text = "";
        mapManager = FindObjectOfType<MapManager>();
    }
    void Start()
    {
        RespawnPlayers();
    }
    public void WinRound(PlayerController player)
    {
        //Destroy all active projectiles
        DestroyAllProjectiles();

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

    void DestroyAllProjectiles()
    {
        ProjectileBehavior[] projectiles = FindObjectsOfType<ProjectileBehavior>();
        foreach(ProjectileBehavior projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }
    
    void LoadMainMenu()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadMainMenu();
    }
    void NextRound()
    {
        DestroyAllProjectiles();
        broadcastText.text = "";
        mapManager.ChangeMap();
        RespawnPlayers();
    }

    void RespawnPlayers()
    {
        playerOne.Respawn(mapManager.currentMap.playerOneRespawnPoint.transform.position);
        playerTwo.Respawn(mapManager.currentMap.playerTwoRespawnPoint.transform.position);
    }
}
