using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float playerSpeedSkillPointsSpent = 1f;
    [SerializeField] public float projectileSpeedSkillPointsSpent = 1f;
    [SerializeField] public float playerSizeSkillPointsSpent = 1f;
    [SerializeField] public int skillPoints = 10;
    [SerializeField] public int startingSkillPoints = 10;
    [SerializeField] CharacterSelection player;

    public int GetSkillPointsNumber()
    {
        // return skillPoints - (int)player.playerSpeed.value - (int)player.playerSize.value - (int)player.projectileSpeed.value;
        return skillPoints;
    }

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update() 
    {
        // skillPoints = startingSkillPoints;
        UpdateSkillPoints();
    }
    void UpdateSkillPoints()
    {
        playerSpeedSkillPointsSpent = 1f + player.playerSpeed.value; 
        playerSizeSkillPointsSpent = 1f + player.playerSize.value;
        projectileSpeedSkillPointsSpent = 1f + player.projectileSpeed.value;
        skillPoints = startingSkillPoints
         - (int)(playerSpeedSkillPointsSpent + 0.001f)
         - (int)(playerSizeSkillPointsSpent + 0.001f)
         - (int)(projectileSpeedSkillPointsSpent + 0.001f)
         + 3; // <----- Number of stats that are customizable
    }



}
