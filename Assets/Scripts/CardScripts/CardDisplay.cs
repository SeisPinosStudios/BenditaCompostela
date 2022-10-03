using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descText;
    public Image artworkImage;
    public Text cost;
    void Start()
    {
        nameText.text = card.name;
        descText.text = card.description;
        //artworkImage.sprite = card.artwork;
        cost.text = card.cost.ToString();
    }
}
