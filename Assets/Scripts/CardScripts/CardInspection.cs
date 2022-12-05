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
    public float progress;
    public float speed = 0.5f;
    Vector3 originalScale;
    Vector3 originalPosition;
    public int offset;
    public bool inspecting = false;
    public bool canInspect = false;
    #endregion

    public void Start()
    {
        originalPosition = transform.position;
    }

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
        originalPosition.x = transform.localPosition.x;

        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + offset, 1.0f);
            DisableHandPanel();
        }

        progress = 0.0f;
        /*if (siblingIndex != (GetComponentInParent<Transform>().childCount))*/ transform.SetAsLastSibling();
        inspecting = !inspecting;
    }
    void NotInspecting()
    {
        Debug.Log("NOT INSPECTING");
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            //transform.localPosition = originalPosition;
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

    void Update()
    {
        //if(progress >= 1 && canInspect) transform.position = new Vector3(transform.localPosition.x, originalPosition.y, 1.0f); 

        if(progress < 1 && !inspecting)
        {
            Debug.Log("MOVING CARD");
            var position = Mathf.Lerp(originalPosition.y + offset, originalPosition.y, progress);
            progress += speed * Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x, position, 1.0f);
            Debug.Log("PROGRESS: " + progress);
        }
    }

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
