using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Game Context And Map Nodes
    [SerializeField] private List<Node> aux;
    [SerializeField] private GameObject auxGo;

    public static bool auxInitialize = true;

    public static List<Node> mapNodeList;
    public static string currentLevelNodeGoName;
    public static int gameProgressContext = 0;
    #endregion


    public static Enemy nextEnemy;
    public static string ActualRoute;
 
    public GameObject[] BattleCompletedUI = new GameObject[2];
    public Player player;
    public static Player playerData;

    public void Awake()
    {
        if (auxInitialize)
        {
            currentLevelNodeGoName = auxGo.name;
            mapNodeList = aux;
            auxInitialize = false;
        }
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerData = player.copy();
        }
        else
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "DebugScene")
        {
            foreach (CardData card in Resources.LoadAll<CardData>("Assets"))
            {
                playerData.inventory.Add(card);
                playerData.inventory.Add(card);
            }
        }
    }
    public void BattleEnd(Entity loser)
    {
        switch (loser.GetType().ToString())
        {
            case "PlayerScript":
                Instantiate(BattleCompletedUI[1], GameObject.Find("====CANVAS====").transform);
                break;
            case "EnemyScript":
                Instantiate(BattleCompletedUI[0], GameObject.Find("====CANVAS====").transform);
                mapNodeList.Find(n => n.currentNodeGoName == currentLevelNodeGoName).isCompleted = true;
                gameProgressContext++;

                break;
        }
    }

    public static GameObject GetCurrentLevelNode()
    {
        return GameObject.Find("===ROUTE MAP===").FindObject(currentLevelNodeGoName);
    }

    public static GameObject GetAnyLevelNode(string name)
    {
        return GameObject.Find("===ROUTE MAP===").FindObject(name);
    }

}
