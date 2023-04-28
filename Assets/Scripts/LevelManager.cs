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
        // scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }
    public void LoadChararcterMenu(){
        SceneManager.LoadScene("CharacterSelection");
    }
    public void LoadMainMenu()
    {
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
