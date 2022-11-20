using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DeckBuilderDisplay: MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckSlotPrefab;
    public List<CardData> cards;
    int cardAmount;
    float scrollPosition;

    public void Start()
    {
        foreach(CardData card in GameManager.playerData.inventory.Where(card => card.GetType() != typeof(Armor)).ToList())
        {
            cardPrefab.GetComponent<CardDisplay>().cardData = card;
            if(cards.Find(dupCard => dupCard.name == card.name) == null) Instantiate(cardPrefab, GameObject.Find("Content").transform);
            cards.Add(card);
        }

        foreach(CardData card in GameManager.playerData.playerDeck)
        {
            InsertCardInDeck(card);
        }

        cardAmount = GameObject.Find("Content").transform.childCount;
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

    private void Update()
    {
        if (cardAmount != GameObject.Find("Content").transform.childCount)
        {
            GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = scrollPosition;
            cardAmount = GameObject.Find("Content").transform.childCount;
        }
        else scrollPosition = GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition;
    }
}
