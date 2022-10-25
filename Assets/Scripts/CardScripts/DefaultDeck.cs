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

    public void DrawCard(int drawnCards)
    {
        StartCoroutine(DrawCardCorroutine(drawnCards));
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
    public IEnumerator DrawCardCorroutine(int drawnCards)
    {
        for (int j = 0; j < drawnCards; j++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.2f);
            if (deckQueue.Count <= 0) break;
        }
    }
    public IEnumerator DiscardCorroutine()
    {
        while(hand.transform.childCount > 0)
        {
            deckQueue.Enqueue(hand.transform.GetChild(0).GetComponent<CardDisplay>().cardData);
            Destroy(hand.transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Salió del bucle");
    }
}
