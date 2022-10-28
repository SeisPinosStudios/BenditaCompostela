using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardInspection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Other Variables
    /* Functionality variables. */
    public int siblingIndex;
    public float scaleMultiplier = 1.5f;
    Vector3 originalScale;
    private bool inspecting = false;
    #endregion

    public void Awake()
    {
        if(SceneManager.GetActiveScene().name != "BattleScene")
        {
            this.gameObject.transform.localScale = (this.transform.localScale * 2) / 3;
        }
    }

    #region Inspection methods
    /* This methods control the inspection functionality of the cards. When
     * a player hover over a card of the hand it wil get bigger for better and
     * easier inspection */
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) return;

        originalScale = transform.localScale;
        transform.localScale = (transform.localScale)*scaleMultiplier;

        if (SceneManager.GetActiveScene().name == "BattleScene") 
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 250, 0.0f);
            DisableHandPanel(); 
        }

        transform.SetAsLastSibling();
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        if (SystemInfo.deviceType == DeviceType.Handheld) return;

        transform.localScale = originalScale;

        if (SceneManager.GetActiveScene().name == "BattleScene") 
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 250, 0.0f);
            EnableHandPanel(); 
        }

        
    } 
    #endregion
    void Update()
    {
        if(SystemInfo.deviceType == DeviceType.Handheld)
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
                inspecting = !inspecting;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended && inspecting == true)
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
    }

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
}
