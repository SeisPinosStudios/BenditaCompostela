using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    public Enemy enemy;
    public int context;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => ToBattleScene(enemy));
    }

    public void ToBattleScene(Enemy enemy)
    {
        if (GameManager.playerData.playerDeck.Count < 5) return;
        GameManager.currentLevelNodeGoName = gameObject.name;
        GameManager.nextEnemy = enemy;
        SceneManager.LoadScene("BattleScene");
    }
}
