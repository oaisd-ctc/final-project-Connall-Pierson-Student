using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] maps;
    GameManager gameManager;
    public Map currentMap;


    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        ChangeMap();
    }

    public void ChangeMap()
    {
        currentMap = maps[Random.Range(0, 5)].GetComponent<Map>();

        foreach(GameObject map in maps)
        {
            if(map.GetComponent<Map>() != currentMap)
            {
                map.SetActive(false);
            }
            else
            {
                map.SetActive(true);
            }
        }
    }
    public void ChangeMap(int mapIndex)
    {
        currentMap = maps[mapIndex].GetComponent<Map>();;

        foreach(GameObject map in maps)
        {
            if(map != currentMap)
            {
                map.SetActive(false);
            }
            else
            {
                map.SetActive(true);
            }
        }
    }
}
