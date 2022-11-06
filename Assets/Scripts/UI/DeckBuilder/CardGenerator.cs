using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<CardData> cards;

    public void Start()
    {
        foreach(CardData card in GameManager.playerData.inventory)
        {
            cardPrefab.GetComponent<CardDisplay>().cardData = card;
            if(!cards.Contains(typeof()) Instantiate(cardPrefab, GameObject.Find("Content").transform);
            cards.Add(Instantiate(card));
            
        }
    }
}
