using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    #region Card information variables
    /* Printable information of the card. */
    public CardData cardData;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public Image sprite;
    public Text cost;
    #endregion

    void Start()
    {
        nameText.text = cardData.name;
        descText.text = cardData.description;
        sprite.sprite = cardData.artwork;
        if(cardData.cost != 0) cost.text = cardData.cost.ToString();
    }
}
