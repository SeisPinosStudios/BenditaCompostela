using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 originalPosition;
    public void OnBeginDrag(PointerEventData pointerEvent)
    {
        originalPosition = gameObject.transform.localPosition;
    }

    public void OnDrag(PointerEventData pointerEvent)
    {
        gameObject.transform.position = pointerEvent.position;
    }

    public void OnEndDrag(PointerEventData pointerEvent)
    {
        if (pointerEvent.position.y <= 200)
        {            
            gameObject.transform.position = originalPosition;
        }
        else
        {
            gameObject.GetComponent<Card>().UseCard();
        }

        GameObject.Find("HandPanel").GetComponent<HorizontalLayoutGroup>().enabled = true;
    }
}
