using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    Card[] cards_deck = new Card[0];
    void Start()
    {
        foreach (Card card in Resources.LoadAll<Card>("Cards")) cards.Add(card);
    }
}
