using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float timeDelay = 1f;
    // ScoreKeeper scoreKeeper;
    // void Awake()
    // {
    //     scoreKeeper = FindObjectOfType<ScoreKeeper>();
    // }
    public void LoadGame()
    {
        FindObjectsOfType<CharacterManager>()[0].gameObject.transform.SetParent(null);
        FindObjectsOfType<CharacterManager>()[1].gameObject.transform.SetParent(null);
        DontDestroyOnLoad(FindObjectsOfType<CharacterManager>()[0].gameObject);
        DontDestroyOnLoad(FindObjectsOfType<CharacterManager>()[1].gameObject);
        FindObjectsOfType<PlayerStats>()[0].playerSprite = FindObjectsOfType<CharacterManager>()[1].artworkSprite;
        FindObjectsOfType<PlayerStats>()[1].playerSprite = FindObjectsOfType<CharacterManager>()[0].artworkSprite;
        // scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }
    public void LoadChararcterMenu(){
        SceneManager.LoadScene("CharacterSelection");
    }
    public void LoadMainMenu()
    {
        foreach(PlayerStats playerStats in FindObjectsOfType<PlayerStats>())
        {
            Destroy(playerStats.gameObject);
        }
        foreach(CharacterManager characterManager in FindObjectsOfType<CharacterManager>())
        {
            Destroy(characterManager.gameObject);
        }
        SceneManager.LoadScene("Main Menu");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", timeDelay));
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    IEnumerator WaitAndLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
