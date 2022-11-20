using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCardScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEvet)
    {
        var cardData = GetComponent<CardDisplay>().cardData;

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                var weapon = (Weapon)cardData;
                if (GameManager.playerData.CoinDecrease(15 * (weapon.upgradeLevel + 1))) UpgradeWeapon();
                break;
            case "Armor":
                var armor = (Armor)cardData;
                if(GameManager.playerData.CoinDecrease(15 * (armor.upgradeLevel + 1))) UpgradeArmor();
                break;
        }
    }

    public void UpgradeWeapon()
    {
        var card = (Weapon)GetComponent<CardDisplay>().cardData;

        if (GameManager.playerData.inventory.Find(weapon => weapon == card) != null)
        {
            GameManager.playerData.inventory.Remove(card);
            GameManager.playerData.inventory.Add(card.improvedWeapon);
            return;
        }

        if (GameManager.playerData.playerDeck.Find(weapon => weapon == card) != null)
        {
            GameManager.playerData.playerDeck.Remove(card);
            GameManager.playerData.inventory.Add(card.improvedWeapon);
            return;
        }

        Destroy(gameObject);
    }

    public void UpgradeArmor()
    {
        var card = (Armor)GetComponent<CardDisplay>().cardData;

        if (GameManager.playerData.inventory.Find(weapon => weapon == card) != null)
        {
            GameManager.playerData.inventory.Remove(card);
            GameManager.playerData.inventory.Add(card.improvedArmor);
            return;
        }

        Destroy(gameObject);
    }
}
