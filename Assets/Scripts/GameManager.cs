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

    public static Enemy nextEnemy;
    public static Scene ActualRoute;
    public GameObject BattleCompletedUI;
    public Player player;
    public static Player playerData;

    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerData = player.copy();
    }

    public void GameEnd(Entity loser)
    {
        GameObject BattleCompleted = Instantiate(BattleCompletedUI, GameObject.Find("====CANVAS====").transform);

        if (loser.GetType() == typeof(EnemyScript))
        {
            PlayerWin(BattleCompleted);
        }
    }

    public void PlayerWin(GameObject BattleCompleted)
    {
        BattleCompleted.GetComponent<Transform>().GetChild(0).GetComponent<TextMeshProUGUI>().text = "¡Enhorabuena, has vencido!";
        BattleCompleted.GetComponent<Transform>().GetChild(1).GetComponent<TextMeshProUGUI>().text 
            = "Has logrado derrotar a tu enemigo, vuelve al mapa para seguir avanzando por la ruta.";
        BattleCompleted.GetComponent<Transform>().GetChild(2).GetComponent<Button>().enabled = true;
        BattleCompleted.GetComponent<Transform>().GetChild(3).GetComponent<Button>().enabled = false;
    }
}
