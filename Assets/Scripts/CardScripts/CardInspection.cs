using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInspection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Other Variables
    /* Functionality variables. */
    public int siblingIndex;
    #endregion

    #region Inspection methods
    /* This methods control the inspection functionality of the cards. When
     * a player hover over a card of the hand it wil get bigger for better and
     * easier inspection */
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) return;

        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 200, 0.0f);
        DisableHandPanel();
        transform.SetAsLastSibling();
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) return;

        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 200, 0.0f);
        EnableHandPanel();
    } 
    #endregion

    #region Panel-related methods
    /* Panel-related methods. This methods disable and enable the hand panel everytime
     * you inspect or drag a card to avoid reconfiguration of hand and to keep the card you're
     * interested in on top of the other elements of the UI */
    public void DisableHandPanel()
    {
        siblingIndex = transform.GetSiblingIndex();
        GameObject.Find("HandPanel").GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
    public void EnableHandPanel()
    {
        transform.SetSiblingIndex(siblingIndex);
        GameObject.Find("HandPanel").GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
    #endregion
}
