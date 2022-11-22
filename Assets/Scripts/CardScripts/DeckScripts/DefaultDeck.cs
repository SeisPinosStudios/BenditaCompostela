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

        bool weaponOnTop = false;

        CopyDeck();
        while (!weaponOnTop)
        {
            Shuffle();
            weaponOnTop = WeaponOnTop();
        }
        
        foreach(CardData card in playerDeck) deckQueue.Enqueue(card);
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
    public bool WeaponOnTop()
    {
        for(int i = 0; i < 5; i++)
        {
            if (playerDeck[i].GetType() == typeof(Weapon)) return true;
        }

        return false;
    }

    #region Card Drawing Methods
    public void DrawCard()
    {
        if (deckQueue.Count <= 0) return;
        card.GetComponent<CardDisplay>().cardData = deckQueue.Dequeue();
        Instantiate(card, hand.transform);

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("DrawCard");
    }
    public IEnumerator DrawCardCorroutine(int drawnCards)
    {
        for (int j = 0; j < drawnCards; j++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.2f);
            if (deckQueue.Count <= 0) break;
        }

        foreach (Transform card in hand.transform)
        {
            card.GetComponent<CardInspection>().canInspect = true;
        }
    }
    public void StartDrawCoroutine(int drawnCards)
    {
        StartCoroutine(DrawCardCorroutine(drawnCards));
    }
    public IEnumerator DiscardCorroutine()
    {
        while (hand.transform.childCount > 0)
        {
            if (hand.transform.GetChild(0).GetComponent<CardDisplay>().cardData.GetType() != typeof(Attack))
                deckQueue.Enqueue(hand.transform.GetChild(0).GetComponent<CardDisplay>().cardData);
            Destroy(hand.transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(0.3f);
        }

        Debug.Log("Salió del bucle");
    }
    #endregion
}
