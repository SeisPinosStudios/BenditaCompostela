using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Card information variables
    /* Printable information of the card. */
    public CardData cardData;
    public Text nameText;
    public Text descText;
    public Image artworkImage;
    public Text cost;
    #endregion

    #region Other Variables
    /* Functionality variables. */
    public int siblingIndex;
    Vector3 auxScale;
    float scaleMultiplier = 1.5f; // antes 2
    float transformRespos = 1f; // antes 250
    #endregion

    void Start()
    {
        nameText.text = cardData.name;
        descText.text = cardData.description;
        //artworkImage.sprite = card.artwork;
        cost.text = cardData.cost.ToString();

        auxScale = transform.localScale;
    }

    #region Inspection methods
    /* This methods control the inspection functionality of the cards. When
     * a player hover over a card of the hand it wil get bigger for better and
     * easier inspection */
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        Debug.Log("Ha entrado en carta" + name);
        // Cambio para que el multiplicador de la carta sea con su tamaño local y no el original
        //transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        
        transform.localScale = new Vector3(auxScale.x*scaleMultiplier, auxScale.y * scaleMultiplier, auxScale.y * scaleMultiplier);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + transformRespos, -2.0f);
        //DisableHandPanel();
        //transform.SetAsLastSibling();
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        transform.localScale = new Vector3(auxScale.x, auxScale.y, auxScale.z);
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - transformRespos, 0.0f);
        //EnableHandPanel();
    }
    #endregion

    #region Panel-related methods
    /* Panel-related methods. This methods disable and enable the hand panel everytime
     * you inspect or drag a card to avoid reconfiguration of hand and to keep the card you're
     * interested in on top of the other elements of the UI */
    public void DisableHandPanel()
    {
        siblingIndex = transform.GetSiblingIndex();
        gameObject.GetComponentInParent<HorizontalLayoutGroup>().enabled = false;
    }
    public void EnableHandPanel()
    {
        transform.SetSiblingIndex(siblingIndex);
        gameObject.GetComponentInParent<HorizontalLayoutGroup>().enabled = true;
    }
    #endregion
}
