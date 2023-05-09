using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Image playerSprite;
    public float playerSpeedSkillPointsSpent = 1f;
    public float projectileSpeedSkillPointsSpent = 1f;
    public float playerSizeSkillPointsSpent = 1f;
    public float fireRateSkillPointsSpent = 1f;
    public int skillPoints = 10;
    public int startingSkillPoints = 10;
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
        playerSpeedSkillPointsSpent = player.playerSpeed.value; 
        playerSizeSkillPointsSpent = player.playerSize.value;
        projectileSpeedSkillPointsSpent = player.projectileSpeed.value;
        skillPoints = startingSkillPoints
         - (int)(playerSpeedSkillPointsSpent + 0.001f)
         - (int)(playerSizeSkillPointsSpent + 0.001f)
         - (int)(projectileSpeedSkillPointsSpent + 0.001f)
         + 3; // <----- Number of stats that are customizable
    }



}
