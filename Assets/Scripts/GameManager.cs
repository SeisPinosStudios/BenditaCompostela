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

    #region Game Context
    public static int gameProgressContext = 0;
    #endregion

    #region Card Database
    public List<CardData> cardsList;

    public static Enemy nextEnemy;
    public static Scene ActualRoute;
    public GameObject[] BattleCompletedUI = new GameObject[2];
    public Player player;
    public static Player playerData;

    public void Awake()
    {
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

        foreach (CardData card in Resources.LoadAll<CardData>("Assets")) cardsList.Add(card);
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
                break;
        }
    }
}
