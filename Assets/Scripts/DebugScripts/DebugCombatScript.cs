using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugCombatScript : MonoBehaviour
{
    List<Enemy> enemies;
    public GameObject button;
    public TMP_InputField HPInput;
    public TMP_InputField EnergyInput;
    public Button accept;

    public void Awake()
    {
        foreach(Enemy enemy in Resources.LoadAll<Enemy>("Assets"))
        {
            button.GetComponent<BattleButton>().enemy = enemy;
            button.GetComponentInChildren<TextMeshProUGUI>().text = enemy.name;
            Instantiate(button, GameObject.Find("Content").transform);
        }

        accept.onClick.AddListener(SetStats);
    }

    public void SetStats()
    {
        if (HPInput.text == null) return; 
        GameManager.playerData.maxHP = int.Parse(HPInput.text);
        GameManager.playerData.currentHP = int.Parse(HPInput.text);
        if (EnergyInput.text == null) return;
        GameManager.playerData.energy = int.Parse(EnergyInput.text);
    }
}
