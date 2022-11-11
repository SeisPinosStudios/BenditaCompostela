using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckSlotDisplay : MonoBehaviour
{
    #region Card information variables
    /* Printable information of the card. */
    public CardData cardData;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cost;
    #endregion

    void Start()
    {
        nameText.text = cardData.name;
        cost.text = cardData.cost.ToString();
    }
}
