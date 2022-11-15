using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EquipmentDisplayController : MonoBehaviour
{
    public GameObject equipmentCard;
    List<CardData> cardList;

    public void Awake()
    {
        foreach (CardData card in GameManager.playerData.inventory.Where(card => card.GetType() == typeof(Armor))){
            cardList.Add(card);
            equipmentCard.GetComponent<CardDisplay>().cardData = card;

        }

    }
}
