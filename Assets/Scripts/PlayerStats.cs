using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    public Image playerSprite;
    public float playerSpeedSkillPointsSpent = 1f;
    public float projectileSpeedSkillPointsSpent = 1f;
    public float playerSizeSkillPointsSpent = 1f;
    public float fireRateSkillPointsSpent = 1f;
    public float projectileLifetimeSkillPointsSpent = 1f;
    public int skillPoints = 0;
    public int startingSkillPoints = 15;
    [SerializeField] CharacterSelection player;

    public int GetSkillPointsNumber()
    {
        // return skillPoints - (int)player.playerSpeed.value - (int)player.playerSize.value - (int)player.projectileSpeed.value;
        return skillPoints;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        skillPoints = startingSkillPoints;
    }
    void Update()
    {
        // skillPoints = startingSkillPoints;
        UpdateSkillPoints();
    }
    void UpdateSkillPoints()
    {
        if (SceneManager.GetActiveScene().name != "Game")
        {
            playerSpeedSkillPointsSpent = player.playerSpeed.value;
            playerSizeSkillPointsSpent = player.playerSize.value;
            projectileSpeedSkillPointsSpent = player.projectileSpeed.value;
            fireRateSkillPointsSpent = player.fireRate.value;
            projectileLifetimeSkillPointsSpent = player.projectileLifeTime.value;

            skillPoints = startingSkillPoints
             - (int)(playerSpeedSkillPointsSpent + 0.001f)
             - (int)(playerSizeSkillPointsSpent + 0.001f)
             - (int)(projectileSpeedSkillPointsSpent + 0.001f)
             - (int)(fireRateSkillPointsSpent + 0.001f)
             - (int)(projectileLifetimeSkillPointsSpent + 0.001f);
        }
    }



}
