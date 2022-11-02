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
        if(GameManager.gameProgressContext != context)
            gameObject.GetComponent<Button>().enabled = false;
    }

    public void ToBattleScene(Enemy enemy)
    {
        GameManager.nextEnemy = enemy;
        SceneManager.LoadScene("BattleScene");
    }
}
