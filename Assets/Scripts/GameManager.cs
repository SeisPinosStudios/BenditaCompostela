using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Game Context And Map Nodes    
    public static List<int> completedNodes = new List<int>();
    public static int currentNode;

    public static bool auxInitialize = true;

    public static int gameProgressContext = 0;
    #endregion

    #region Tutorial
    public static bool hasTutorial;
    public static List<DialogueObject> tutorialText;
    #endregion

    public static Enemy nextEnemy;
    public static string ActualRoute;
    public GameObject[] BattleCompletedUI = new GameObject[2];
    public Player player;
    public static Player playerData;
    public List<CardData> Debuginventory;


    #region Debug
    static bool debug = false;
    #endregion

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "DebugScene") debug = true;

        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerData = player.copy();
            //if (HasSavedData()) LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }

        if (debug)
        {
            foreach (CardData card in Resources.LoadAll<CardData>("Assets"))
            {
                playerData.inventory.Add(card);
                playerData.inventory.Add(card);
            }
            Debuginventory = playerData.inventory;
        }

        if (auxInitialize && !debug)
        {
            auxInitialize = false;
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
                GameManager.playerData.currentHP = GameObject.Find("Player").GetComponent<PlayerScript>().currentHP;
                UpdateNodeProgress();
                break;
        }
    }
    public static void UpdateNodeProgress()
    {
        completedNodes.Add(currentNode);
        gameProgressContext++;
    }
    public static void SaveGame()
    {
        if (debug) return;

        StringBuilder inventoryData = new StringBuilder();
        StringBuilder playerDeckData = new StringBuilder();
        StringBuilder equipmentData = new StringBuilder();
        StringBuilder completedNodesData = new StringBuilder();
        foreach (CardData card in playerData.inventory) {
            var cardData = CardDataFilter.allCards().Find(cardData => cardData.name == card.name);
            Debug.Log(CardDataFilter.allCards().IndexOf(cardData));
            inventoryData.Append(CardDataFilter.allCards().IndexOf(cardData) + "\n");
        }
        foreach (CardData card in playerData.playerDeck) {
            var cardData = CardDataFilter.allCards().Find(cardData => cardData.name == card.name);
            Debug.Log(CardDataFilter.allCards().IndexOf(cardData));
            playerDeckData.Append(CardDataFilter.allCards().IndexOf(cardData) + "\n");
        }

        equipmentData.Append(CardDataFilter.allCards().IndexOf(CardDataFilter.allCards().Find(cardData => cardData.name == playerData.chestArmor.name))+"\n");
        equipmentData.Append(CardDataFilter.allCards().IndexOf(CardDataFilter.allCards().Find(cardData => cardData.name == playerData.feetArmor.name))+"\n");

        foreach(int node in completedNodes)
        {
            completedNodesData.Append(node + "\n");
        }

        PlayerPrefs.SetString("inventoryData", inventoryData.ToString());
        PlayerPrefs.SetString("playerDeckData", playerDeckData.ToString());
        PlayerPrefs.SetString("equipmentData", equipmentData.ToString());
        PlayerPrefs.SetString("route", ActualRoute);
        PlayerPrefs.SetInt("gameProgressContext", gameProgressContext);
        PlayerPrefs.SetInt("SavedData", 1);

        Debug.Log("Data saved");
    }
    public static void LoadGame()
    {
        playerData.playerDeck.Clear();
        playerData.inventory.Clear();

        if (PlayerPrefs.GetInt("SavedData") != 1) return;

        string[] inventoryData = PlayerPrefs.GetString("inventoryData").Split('\n');
        string[] playerDeckData = PlayerPrefs.GetString("playerDeckData").Split('\n');
        string[] equipmentData = PlayerPrefs.GetString("equipmentData").Split('\n');
        string[] completedNodesData = PlayerPrefs.GetString("completedNodesData").Split('\n');

        
        for(int i = 0; i < inventoryData.Length-1; i++)
        {
            Debug.Log(inventoryData[i]);
            playerData.inventory.Add(Instantiate(CardDataFilter.allCards()[int.Parse(inventoryData[i])]));
        }

        for (int i = 0; i < playerDeckData.Length - 1; i++)
        {
            Debug.Log(playerDeckData[i]);
            playerData.playerDeck.Add(Instantiate(CardDataFilter.allCards()[int.Parse(playerDeckData[i])]));
        }

        for(int i = 0; i < completedNodesData.Length - 1; i++)
        {
            completedNodes.Add(int.Parse(completedNodesData[i]));
        }

        playerData.chestArmor = (Armor)Instantiate(CardDataFilter.allCards()[int.Parse(equipmentData[0])]);
        playerData.feetArmor = (Armor)Instantiate(CardDataFilter.allCards()[int.Parse(equipmentData[1])]);

        gameProgressContext = PlayerPrefs.GetInt("gameProgressContext");
        
        Debug.Log("Data loaded");
    }
    public static bool HasSavedData()
    {
        if (PlayerPrefs.GetInt("SavedData") == 1) return true;
        return false;
    }
    public static void DumpSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}