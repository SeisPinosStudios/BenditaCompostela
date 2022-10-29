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
    public PlayerScript player;
    
    private void Start()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(DrawCard);

        CopyDeck();
        Shuffle();
        foreach(CardData card in playerDeck) deckQueue.Enqueue(card);
    }

    public void DrawCard()
    {
        if (deckQueue.Count <= 0) return;
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
    public void CopyDeck()
    {
        foreach(CardData card in GameManager.playerData.playerDeck) playerDeck.Add(card);
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
