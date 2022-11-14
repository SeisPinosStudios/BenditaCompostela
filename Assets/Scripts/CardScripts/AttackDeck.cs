using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDeck : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public GameObject card;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);
    }

    public void Update()
    {
        if(player.GetComponent<PlayerScript>().weapon == null) gameObject.GetComponent<Button>().enabled = false;
        else gameObject.GetComponent<Button>().enabled = true;
    }

    public void DrawCard()
    {
        
        Weapon weapon = player.GetComponent<PlayerScript>().weapon;
        card.GetComponent<CardDisplay>().cardData = weapon.attackList[Random.Range(0, weapon.attackList.Count)];
        if (player.GetComponent<PlayerScript>().ConsumeEnergy(1)) Instantiate(card, hand.transform); ;    
    }
}
