using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();
    void Start()
    {
        foreach (CardData card in Resources.LoadAll<CardData>("Cards")) cards.Add(card);
        gameObject.GetComponent<Deck>().GenerateDeck();
    }
}
