using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

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

    #region Combat and Routes
    public static Enemy nextEnemy;
    public static string ActualRoute = "Sevilla";
    public GameObject[] BattleCompletedUI = new GameObject[2];
    public Player player;
    public static Player playerData;
    public List<CardData> Debuginventory;
    public List<Sprite> backgrounds;
    public static Sprite activeBackground;
    #endregion

    #region Audio
    public static MusicManager musicManager;
    public static float musicVolume = 0.5f;
    public static float SFXVolume = 0.5f;
    #endregion

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
    public void Start()
    {
        musicManager = GetComponent<MusicManager>();
    }
    public void Update()
    {
        GetBackground();

        if (SceneManager.GetActiveScene().name == "Cinematic_1" && GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();

        if (SceneManager.GetActiveScene().name != "Cinematic_1" && !GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
    }

    public static void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    #region Sound
    public static void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicManager.SetVolume(musicVolume);
    }
    public static void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SetVolume(SFXVolume);
    }
    public static void PlayBattleMusic()
    {
        musicManager.BattleMusic();
    }
    public static void PlayShopMusic()
    {
        musicManager.ShopMusic();
    }
    #endregion


    #region Game Data
    public static void SaveGame()
    {
        if (debug) return;

        StringBuilder inventoryData = new StringBuilder();
        StringBuilder playerDeckData = new StringBuilder();
        StringBuilder equipmentData = new StringBuilder();
        StringBuilder completedNodesData = new StringBuilder();

        foreach (CardData card in playerData.inventory)
        {
            var cardData = CardDataFilter.allCards().Find(cardData => cardData.name == card.name);
            Debug.Log(CardDataFilter.allCards().IndexOf(cardData));
            inventoryData.Append(CardDataFilter.allCards().IndexOf(cardData) + "\n");
        }

        foreach (CardData card in playerData.playerDeck)
        {
            var cardData = CardDataFilter.allCards().Find(cardData => cardData.name == card.name);
            Debug.Log(CardDataFilter.allCards().IndexOf(cardData));
            playerDeckData.Append(CardDataFilter.allCards().IndexOf(cardData) + "\n");
        }

        equipmentData.Append(CardDataFilter.allCards().IndexOf(CardDataFilter.allCards().Find(cardData => cardData.name == playerData.chestArmor.name)) + "\n");
        equipmentData.Append(CardDataFilter.allCards().IndexOf(CardDataFilter.allCards().Find(cardData => cardData.name == playerData.feetArmor.name)) + "\n");

        foreach (int node in completedNodes)
        {
            Debug.Log(node);
            completedNodesData.Append(node + "\n");
            Debug.Log(completedNodesData.ToString());
        }

        PlayerPrefs.SetString("inventoryData", inventoryData.ToString());
        PlayerPrefs.SetString("playerDeckData", playerDeckData.ToString());
        PlayerPrefs.SetString("equipmentData", equipmentData.ToString());
        PlayerPrefs.SetString("completedNodesData", completedNodesData.ToString());
        PlayerPrefs.SetString("route", ActualRoute);
        PlayerPrefs.SetInt("gameProgressContext", gameProgressContext);
        PlayerPrefs.SetInt("SavedData", 1);
        PlayerPrefs.SetInt("HP", playerData.maxHP);
        PlayerPrefs.SetInt("energy", playerData.energy);
        PlayerPrefs.SetInt("coins", playerData.coins);
        PlayerPrefs.SetInt("currentHP", playerData.currentHP);
        PlayerPrefs.SetFloat("volume", AudioListener.volume);

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


        for (int i = 0; i < inventoryData.Length - 1; i++)
        {
            Debug.Log("INVENTORY: " + inventoryData[i]);
            playerData.inventory.Add(Instantiate(CardDataFilter.allCards()[int.Parse(inventoryData[i])]));
        }


        for (int i = 0; i < playerDeckData.Length - 1; i++)
        {
            Debug.Log("PLAYER DECK: " + playerDeckData[i]);
            playerData.playerDeck.Add(Instantiate(CardDataFilter.allCards()[int.Parse(playerDeckData[i])]));
        }

        Debug.Log("COMPLETED NODES COUNT: " + completedNodesData.Length + "CONTENT: " + completedNodesData);

        for (int i = 0; i < completedNodesData.Length - 1; i++)
        {
            Debug.Log("COMPLETED NODES: " + completedNodesData[i]);
            completedNodes.Add(int.Parse(completedNodesData[i]));
        }

        playerData.chestArmor = (Armor)Instantiate(CardDataFilter.allCards()[int.Parse(equipmentData[0])]);
        playerData.feetArmor = (Armor)Instantiate(CardDataFilter.allCards()[int.Parse(equipmentData[1])]);

        gameProgressContext = PlayerPrefs.GetInt("gameProgressContext");
        Debug.Log("GAME CONTEXT: " + gameProgressContext);

        playerData.maxHP = PlayerPrefs.GetInt("HP");
        playerData.coins = PlayerPrefs.GetInt("coins");
        playerData.energy = PlayerPrefs.GetInt("energy");
        playerData.currentHP = PlayerPrefs.GetInt("currentHP");
        GameManager.ActualRoute = PlayerPrefs.GetString("route");
        AudioListener.volume = PlayerPrefs.GetFloat("volume");

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
    public void ResetGameManager()
    {
        playerData = player.copy();
        ActualRoute = "Sevilla";
        completedNodes.Clear();
        gameProgressContext = 0;
    }
    #endregion

    #region Route And Game Progress
    public static void UpdateNodeProgress()
    {
        completedNodes.Add(currentNode);
        gameProgressContext++;
        SaveGame();
    }
    public void SetRoute(string route)
    {
        ActualRoute = route;
    }
    public static void NewRoute()
    {
        Debug.Log("NEW ROUTE");
        gameProgressContext = 0;
        completedNodes.Clear();
    }
    public void GetBackground()
    {
        switch (ActualRoute)
        {
            case "Sevilla":
                activeBackground = backgrounds[0];
                break;
            case "Extremadura":
                activeBackground = backgrounds[1];
                break;
            case "Leon":
                activeBackground = backgrounds[2];
                break;
            case "Galicia":
                activeBackground = backgrounds[3];
                break;
        }
    }
    public void BattleEnd(Entity loser)
    {
        switch (loser.GetType().ToString())
        {
            case "PlayerScript":
                DumpSavedData();
                SceneManager.LoadScene("DeathScene");
                //Instantiate(BattleCompletedUI[1], GameObject.Find("====CANVAS====").transform);
                break;
            case "EnemyScript":
                UpdateNodeProgress();
                Instantiate(BattleCompletedUI[0], GameObject.Find("====CANVAS====").transform);
                GameManager.playerData.currentHP = GameObject.Find("Player").GetComponent<PlayerScript>().currentHP;
                break;
        }
    }
    #endregion

    #region Mobile Detection
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public static bool isMobile()
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
    #endif
        return false;
    }
    #endregion

}