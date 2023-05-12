using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainMenuControls : MonoBehaviour
{
    [SerializeField]Canvas mainCanvas;
    [SerializeField]Canvas settingsCanvas;
    [SerializeField]Canvas creditsCanvas;
    
    void Awake()
    {
        
    }
    void Start()
    {

    }
    void DisableCanvas(Canvas canvas)
    {
       canvas.gameObject.SetActive(false);
    }
}
