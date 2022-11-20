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
            case "Route_Andalucia":
                return "Andalucia";
            case "1":
                return "Extremadura";
            case "2":
                return "León";
            case "3":
                return "Galicia";
            case "BattleScene":
                return RouteCompute(GameManager.ActualRoute);
            case "EquipmentScene":
                return RouteCompute(GameManager.ActualRoute);
            case "DeckBuilderScene":
                return RouteCompute(GameManager.ActualRoute);
            default:
                return SceneManager.GetActiveScene().name;
        }
    }
}
