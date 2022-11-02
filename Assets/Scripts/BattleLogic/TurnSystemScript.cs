using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject current;
    public GameObject next;

    public GameObject handPannel;
    public GameObject DefaultDeck;
    

    public void Start()
    {
        current = enemy;
        next = player;
        Turn();
    }

    public void Turn()
    {
        var temp = current;
        current = next;
        next = temp;

        Debug.Log("Turno de " + current);

        if (current == player)
        {
            StartCoroutine(player.GetComponent<PlayerScript>().OnTurnBegin());
        }
        else enemy.GetComponent<EnemyScript>().OnTurnBegin(); 
    }
}
