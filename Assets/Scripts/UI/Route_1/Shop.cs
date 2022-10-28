using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    [SerializeField] private Transform[] cardPivots;
    [SerializeField] private GameObject cardPrefab;


    void Start()
    {
        for (int i = 0; i < cardPivots.Length; i++)
        {
            GameObject go = Instantiate(cardPrefab, cardPivots[i]);
            CardDisplay cardDisplay = go.GetComponent<CardDisplay>();
            go.GetComponent<CardInspection>().scaleMultiplier = 1.5f;
            
            cardDisplay.cardData = GetComponent<CardDatabase>().cards[Random.Range(1, 40)];
        }
        

    }

}
