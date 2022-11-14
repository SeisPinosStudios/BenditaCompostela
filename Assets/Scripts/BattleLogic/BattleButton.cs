using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    public Enemy enemy;
    public int context;

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => ToBattleScene(enemy));
    }

    public void ToBattleScene(Enemy enemy)
    {
        GameManager.nextEnemy = enemy;
        GameManager.currentLevelNodeGoName = gameObject.name;
        SceneManager.LoadScene("BattleScene");
    }
}
