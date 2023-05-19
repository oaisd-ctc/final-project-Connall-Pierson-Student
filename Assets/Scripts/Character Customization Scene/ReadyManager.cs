using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyManager : MonoBehaviour
{
    [SerializeField] CharacterSelection player1;
    [SerializeField] CharacterSelection player2;
    SoundManager soundManager;
    public AudioClip clip;
    LevelManager levelManager;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    void Update()
    {
        if (player1.isReady && player2.isReady)
        {
            levelManager.LoadGame();
               soundManager.PlayMusic(clip);
           
        }
    }
}
