using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultDeck : MonoBehaviour
{
    public Queue<CardData> deckQueue = new Queue<CardData>();
    public List<CardData> playerDeck = new List<CardData>();

    public GameObject hand;
    public GameObject card;
    public GameObject player;
    
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);

        playerDeck = player.GetComponent<PlayerScript>().playerDeck;
        Shuffle();
        foreach(CardData card in playerDeck) deckQueue.Enqueue(card);
        Debug.Log(deckQueue.Peek());
    }

    public void DrawCard()
    {
        if (deckQueue.Count <= 0) return;
        Debug.Log("Deck clicked.");
        card.GetComponent<CardDisplay>().cardData = deckQueue.Dequeue();
        Instantiate(card, hand.transform);
    }

    public void DrawCard(int cardsToDraw)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            if (deckQueue.Count <= 0) return;
            Debug.Log("Deck clicked.");
            card.GetComponent<CardDisplay>().cardData = deckQueue.Dequeue();
            Instantiate(card, hand.transform);
        }
    }

    public void Shuffle()
    { 
        for(int i = 0; i < playerDeck.Count; i++)
        {
            var randomPos = Random.Range(0, playerDeck.Count);
            var temporalValue = playerDeck[i];
            playerDeck[i] = playerDeck[randomPos];
            playerDeck[randomPos] = temporalValue;
        }
    }
}
