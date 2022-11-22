using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class CardInspection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Other Variables
    /* Functionality variables. */
    public int siblingIndex;
    public float scaleMultiplier = 2.0f;
    Vector3 originalScale;
    Vector3 originalPosition;
    private bool inspecting = false;
    public bool canInspect = false;
    #endregion

    #region Inspection methods
    /* This methods control the inspection functionality of the cards. When
     * a player hover over a card of the hand it wil get bigger for better and
     * easier inspection */
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (!canInspect || inspecting) return;
        if (!isMobile()) Inspecting();
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (!canInspect || !inspecting) return;
        if (!isMobile()) NotInspecting();
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        if (!isMobile()) return;
        if (!canInspect) return;

        if (inspecting) { NotInspecting(); inspecting = !inspecting; }
        else if (!inspecting) { Inspecting(); inspecting = !inspecting; }
    }
    void Inspecting()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        transform.localScale = (transform.localScale) * scaleMultiplier;

        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 220, 2.0f);
            DisableHandPanel();
        }

        if (siblingIndex != (GetComponentInParent<Transform>().childCount)) transform.SetAsLastSibling();

        inspecting = !inspecting;
    }
    void NotInspecting()
    {
        transform.localScale = originalScale;

        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            transform.localPosition = originalPosition;
            EnableHandPanel();
        }

        inspecting = !inspecting;
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
        GameObject.Find("HandPanel").GetComponent<HorizontalLayoutGroup>().enabled = true;
    }
    #endregion

    #region Mobile Detection
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
        #endif
        return false;
    }
    #endregion
}
