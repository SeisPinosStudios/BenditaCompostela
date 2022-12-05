using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class UpgradeCardScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int cost;
    bool inCard;
    GameObject shopText;
    public void Awake()
    {
        shopText = GameObject.Find("ShopTextSlot");
    }
    public void OnPointerClick(PointerEventData pointerEvet)
    {
        var cardData = GetComponent<CardDisplay>().cardData;

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                var weapon = (Weapon)cardData;
                if (GameManager.playerData.CoinDecrease(weapon.money)) UpgradeWeapon();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
            case "Armor":
                var armor = (Armor)cardData;
                if(GameManager.playerData.CoinDecrease(armor.money)) UpgradeArmor();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
        }
    }
    public void UpgradeWeapon()
    {
        var weapon = (Weapon)GetComponent<CardDisplay>().cardData;
        var previousWeapon = weapon.previousWeapon;

        if (GameManager.playerData.inventory.Find(weapon => weapon == previousWeapon) != null)
        {
            GameManager.playerData.inventory.Remove(previousWeapon);
            GameManager.playerData.inventory.Add(weapon);
            return;
        }

        if (GameManager.playerData.playerDeck.Find(weapon => weapon == previousWeapon) != null)
        {
            GameManager.playerData.playerDeck.Remove(previousWeapon);
            GameManager.playerData.playerDeck.Add(weapon);
            return;
        }

        shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy.Count)];

        Destroy(gameObject);
    }
    public void UpgradeArmor()
    {
        var armor = (Armor)GetComponent<CardDisplay>().cardData;
        var previousArmor = armor.previousArmor;

        if (GameManager.playerData.inventory.Find(armor => armor == previousArmor) != null)
        {
            GameManager.playerData.inventory.Remove(previousArmor);
            GameManager.playerData.inventory.Add(armor);
            return;
        }

        shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy.Count)];

        Destroy(gameObject);
    }
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        if (inCard) return;
        var card = GetComponent<CardDisplay>().cardData;
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "Esa mejora cuesta " + card.money + " monedas de oro... ¿Tenemos trato?";
        inCard = true;
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "";
        inCard = false;
    }
    public static Weapon ConvertToWeapon(CardData card)
    {
        return (Weapon)card;
    }
}
