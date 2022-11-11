using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckBuilderDisplay: MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckSlotPrefab;
    public List<CardData> cards;

    public void Start()
    {
        foreach(CardData card in GameManager.playerData.inventory)
        {
            cardPrefab.GetComponent<CardDisplay>().cardData = card;
            Debug.Log(card.name);
            if(cards.Find(dupCard => dupCard.name == card.name) == null) Instantiate(cardPrefab, GameObject.Find("Content").transform);
            cards.Add(card);
        }
    }

    public void InsertCardInDeck(CardData card)
    {
        deckSlotPrefab.GetComponent<DeckSlotDisplay>().cardData = card;
        Instantiate(deckSlotPrefab, GameObject.Find("DeckPanel").transform);
    }

    public void RemoveCardFromDeck(CardData card)
    {
        cardPrefab.GetComponent<CardDisplay>().cardData = card;
        var duplicatedCard = cards.Find(dupCard => dupCard.name == card.name);
        if (duplicatedCard == null) Instantiate(cardPrefab, GameObject.Find("Content").transform);
    }
}
