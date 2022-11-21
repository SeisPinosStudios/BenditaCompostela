using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCardScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int cost;
    bool inCard;
    GameObject shopText;
    public void Awake()
    {
        var card = GetComponent<CardDisplay>().cardData;
        if (card.GetType() == typeof(Armor))
        {
            var armor = (Armor)card;
            cost = 15 * (armor.upgradeLevel + 1);
        }
        else
        {
            var weapon = (Weapon)card;
            cost = 15 * (weapon.upgradeLevel + 1);
        }
        shopText = GameObject.Find("ShopTextSlot");
    }
    public void OnPointerClick(PointerEventData pointerEvet)
    {
        var cardData = GetComponent<CardDisplay>().cardData;

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                var weapon = (Weapon)cardData;
                if (GameManager.playerData.CoinDecrease(15 * (weapon.upgradeLevel + 1))) UpgradeWeapon();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
            case "Armor":
                var armor = (Armor)cardData;
                if(GameManager.playerData.CoinDecrease(15 * (armor.upgradeLevel + 1))) UpgradeArmor();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
        }
    }

    public void UpgradeWeapon()
    {
        var card = (Weapon)GetComponent<CardDisplay>().cardData;

        if (GameManager.playerData.inventory.Find(weapon => weapon == card) != null)
        {
            GameManager.playerData.inventory.Remove(card);
            GameManager.playerData.inventory.Add(card);
            return;
        }

        if (GameManager.playerData.playerDeck.Find(weapon => weapon == card) != null)
        {
            GameManager.playerData.playerDeck.Remove(card);
            GameManager.playerData.inventory.Add(card);
            return;
        }

        shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy.Count)];

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

        shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy.Count)];

        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (inCard) return;
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "Esa mejora cuesta " + cost + " monedas de oro... ¿Tenemos trato?";
        inCard = true;
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "";
        inCard = false;
    }
}
