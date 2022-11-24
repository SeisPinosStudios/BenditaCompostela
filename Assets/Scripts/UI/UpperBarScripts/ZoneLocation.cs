using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ZoneLocation : MonoBehaviour
{
    TextMeshProUGUI textComponent;
    public void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        textComponent.text = SceneManager.GetActiveScene().name;
        textComponent.text = RouteCompute(SceneManager.GetActiveScene().name);
    }

    public string RouteCompute(string scene)
    {
        switch (scene)
        {
            case "BattleScene":
                return GameObject.Find("Enemy").GetComponent<EnemyScript>().name;
            case "EquipmentScene":
                return RouteCompute(GameManager.ActualRoute);
            case "DeckBuilderScene":
                return RouteCompute(GameManager.ActualRoute);
            default:
                return SceneManager.GetActiveScene().name;
        }
    }
}
