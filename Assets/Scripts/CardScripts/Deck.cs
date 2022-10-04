using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Queue<CardData> deck = new Queue<CardData>();
    public List<CardData> cards = new List<CardData>();
    public List<CardData> cardsDatabase;
    public GameObject hand;
    public GameObject card;
    
    private void Awake()
    {
        cardsDatabase = gameObject.GetComponent<CardDatabase>().cards;

        

        gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);
    }

    public void DrawCard()
    {
        Debug.Log("Deck clicked.");
        card.GetComponent<CardDisplay>().cardData = deck.Dequeue();
        Instantiate(card, hand.transform);
        CopyDeckOnList();
    }

    /* For debug purposes */
    public void CopyDeckOnList()
    {
        cards.Clear();
        foreach (CardData card in deck) cards.Add(card);
    }

    public void GenerateDeck()
    {
        /* Debug zone */

        for (int i = 0; i < 20; i++)
        {
            deck.Enqueue(cardsDatabase[Random.Range(0, cardsDatabase.Count-1)]);
        }

        CopyDeckOnList();

        /* End of debug zone */
    }
}
