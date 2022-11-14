using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCombatScript : MonoBehaviour
{
    List<Enemy> enemies;
    public GameObject button;

    public void Awake()
    {
        foreach(Enemy enemy in Resources.LoadAll<Enemy>("Assets"))
        {
            button.GetComponent<BattleButton>().enemy = enemy;
            button.GetComponentInChildren<TextMeshProUGUI>().text = enemy.name;
            Instantiate(button, GameObject.Find("Content").transform);
        }
    }
}
