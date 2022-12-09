using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 originalPosition;
    GameObject deck;
    GameObject hand;
    Card card;
    private CardBorderDisplay cardBorderDisplay;
    public bool cardOnDrag;

    private void Awake()
    {
        deck = GameObject.Find("DefaultDeck");
        hand = GameObject.Find("HandPanel");
        card = gameObject.GetComponent<Card>();
        cardBorderDisplay = gameObject.GetComponentInChildren<CardBorderDisplay>();
    }
    public void OnBeginDrag(PointerEventData pointerEvent)
    {        
        originalPosition = gameObject.transform.localPosition;
        cardOnDrag = true;
        deck.GetComponent<DefaultDeck>().SetInspection(false);
    }
    public void OnDrag(PointerEventData pointerEvent)
    {        
        if (card.self.IsPlayable(card.cardData.cost))
        {            
            if (pointerEvent.position.y >= 200)
            {
                cardBorderDisplay.CardActive();
                cardBorderDisplay.CardUnplayable();
            }
            else
            {
                cardBorderDisplay.CardPlayable();
                cardBorderDisplay.CardInactive();
            }
        }        
        gameObject.transform.position = pointerEvent.position;
    }
    public void OnEndDrag(PointerEventData pointerEvent)
    {
        Debug.Log("ON END DRAG");
        if (pointerEvent.position.y <= 200)
        {
            Debug.Log(originalPosition.y.ToString());
            gameObject.transform.position = originalPosition;            
        }
        else
        {            
            gameObject.GetComponent<Card>().UseCard();
            hand.GetComponent<HorizontalLayoutGroup>().enabled = true;
        }
        cardOnDrag = false;
        cardBorderDisplay.CardInactive();
        deck.GetComponent<DefaultDeck>().SetInspection(true);
    }
}
