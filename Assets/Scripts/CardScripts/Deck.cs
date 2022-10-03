using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Queue<Card> deck = new Queue<Card>();
    public List<Card> cards = new List<Card>();
    public GameObject hand;
    public GameObject card;
    
    private void Start()
    {
        List<Card> cardsDatabase = gameObject.GetComponent<CardDatabase>().cards;

        /* Debug zone */

        for (int i = 0; i < 20; i++)
        {
            deck.Enqueue(cardsDatabase[Random.Range(0, cardsDatabase.Count)]);
        }

        CopyDeckOnList();

        /* End of debug zone */

        gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);
    }

    public void DrawCard()
    {
        Debug.Log("Deck clicked.");
        card.GetComponent<CardDisplay>().card = deck.Dequeue();
        Instantiate(card, hand.transform);
        CopyDeckOnList();
    }

    /* For debug purposes */
    public void CopyDeckOnList()
    {
        cards.Clear();
        foreach (Card card in deck) cards.Add(card);
    }
}
