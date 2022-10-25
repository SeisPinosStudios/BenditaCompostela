using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();
    void Awake()
    {
        foreach (CardData card in Resources.LoadAll<CardData>("Assets")) cards.Add(card);
    }
}
