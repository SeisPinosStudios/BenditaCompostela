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
    private void Awake()
    {
        deck = GameObject.Find("DefaultDeck");
        hand = GameObject.Find("HandPanel");
    }
    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        originalPosition = gameObject.transform.localPosition;

        deck.GetComponent<DefaultDeck>().SetInspection(false);
    }

    public void OnDrag(PointerEventData pointerEvent)
    {
        gameObject.transform.position = pointerEvent.position;
    }
    public void OnEndDrag(PointerEventData pointerEvent)
    {
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
        deck.GetComponent<DefaultDeck>().SetInspection(true);
    }

    public void ModifyDescription()
    {

    }
    public void OriginalDescription()
    {

    }
}
