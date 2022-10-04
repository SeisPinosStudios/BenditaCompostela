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
        Debug.Log("Drag begins.");
        originalPosition = gameObject.transform.position;
    }

    public void OnDrag(PointerEventData pointerEvent)
    {
        gameObject.transform.position = pointerEvent.position;
    }

    public void OnEndDrag(PointerEventData pointerEvent)
    {
        Debug.Log("Drag ends.");
        if (pointerEvent.position.y <= 320)
        {
            gameObject.transform.position = originalPosition;
        }
        else
        {
            gameObject.GetComponent<Card>().UseCard();
        }
    }
}
