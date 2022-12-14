using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentCard : MonoBehaviour, IPointerClickHandler
{
    public AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        audioManager.PlaySound("Armadura");
        var card = (Armor)gameObject.GetComponent<CardDisplay>().cardData;
        var cardList = GameObject.Find("EquipmentDisplayController").GetComponent<EquipmentDisplayController>().cards;
        var cardName = card.name;

        if (card.armorType == Armor.TArmor.CHEST) GameManager.playerData.chestArmor = card;
        else if(card.armorType == Armor.TArmor.FEET) GameManager.playerData.feetArmor = card;

        GameObject.Find("EquipmentDisplayController").GetComponent<EquipmentDisplayController>().UpdateDisplay();
    }
}
