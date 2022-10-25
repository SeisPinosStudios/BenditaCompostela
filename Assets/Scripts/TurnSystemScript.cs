using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject turnButton;

    public GameObject current;
    public GameObject next;

    public void Awake()
    {
        current = player;
        next = enemy;

        turnButton.GetComponent<Button>().onClick.AddListener(Turn);
    }

    public void Turn()
    {
        var temp = current;
        current = next;
        next = temp;

        Debug.Log("Turno de " + current);

        if (current == player) player.GetComponent<PlayerScript>().OnTurnBegin();
        else enemy.GetComponent<EnemyScript>().OnTurnBegin();
    }
}
