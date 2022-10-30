using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class CardInspection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Other Variables
    /* Functionality variables. */
    public int siblingIndex;
    public float scaleMultiplier = 1.5f;
    Vector3 originalScale;
    Vector3 originalPosition;
    private bool inspecting = false;
    #endregion

    public void Awake()
    {

    }

    #region Inspection methods
    /* This methods control the inspection functionality of the cards. When
     * a player hover over a card of the hand it wil get bigger for better and
     * easier inspection */
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (!isMobile() || (isMobile() && !inspecting))
        {
            originalScale = transform.localScale;
            originalPosition = transform.localPosition;
            transform.localScale = (transform.localScale) * scaleMultiplier;

            if (SceneManager.GetActiveScene().name == "BattleScene")
            {
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 320, 2.0f);
                DisableHandPanel();
            }

            if(siblingIndex != (GetComponentInParent<Transform>().childCount)) transform.SetAsLastSibling();
            inspecting = !inspecting;
        }

        if(isMobile() && inspecting)
        {
            transform.localScale = originalScale;

            if (SceneManager.GetActiveScene().name == "BattleScene")
            {
                transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 250, 0.0f);
                EnableHandPanel();
            }

            inspecting = !inspecting;
        }
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (!isMobile())
        {
            transform.localScale = originalScale;
            

            if (SceneManager.GetActiveScene().name == "BattleScene")
            {
                transform.localPosition = originalPosition;
                EnableHandPanel();
            }
        } 
    } 
    #endregion
    /*
    void Update()
    {
        if(isMobile())
        {
            if(Input.GetTouch(0).phase == TouchPhase.Ended && inspecting == false)
            {
                originalScale = transform.localScale;
                transform.localScale = (transform.localScale) * scaleMultiplier;

                if (SceneManager.GetActiveScene().name == "BattleScene")
                {
                    transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 250, 0.0f);
                    DisableHandPanel();
                }

                transform.SetAsLastSibling();
                
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended && inspecting == true)
            {
                
            }
        }
    }*/

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

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
        #endif
        return false;
    }
}
