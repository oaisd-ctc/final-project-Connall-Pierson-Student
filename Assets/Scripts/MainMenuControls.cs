using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuControls : MonoBehaviour
{
    Canvas canvas;
    void Awake()
    {
        // i have no idea what im doing...
        canvas = FindObjectOfType<Canvas>();
    }
    void Start()
    {

    }
    void DisableCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}
