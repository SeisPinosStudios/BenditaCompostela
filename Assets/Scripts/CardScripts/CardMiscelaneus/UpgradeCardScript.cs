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
    CardData originalCard;

    public TextMeshProUGUI costText;
    public AudioManager audioManager;
    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        shopText = GameObject.Find("ShopTextSlot");
        originalCard = gameObject.GetComponent<CardDisplay>().cardData;
        ShowUpgrade();
    }
    public void OnPointerClick(PointerEventData pointerEvet)
    {
        var cardData = GetComponent<CardDisplay>().cardData;

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                audioManager.PlaySound("MejoraArma");
                var weapon = (Weapon)cardData;
                if (GameManager.playerData.CoinDecrease(weapon.money)) UpgradeWeapon();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
            case "Armor":
                audioManager.PlaySound("MejoraArmadura");
                var armor = (Armor)cardData;
                if(GameManager.playerData.CoinDecrease(armor.money)) UpgradeArmor();
                else shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
                break;
        }
    }
    public void UpgradeWeapon()
    {
        var weapon = (Weapon)GetComponent<CardDisplay>().cardData;

        if (GameManager.playerData.inventory.Contains(originalCard))
        {
            GameManager.playerData.inventory.Remove(originalCard);
            GameManager.playerData.inventory.Add(weapon);
        }
        else if (GameManager.playerData.playerDeck.Contains(originalCard))
        {
            GameManager.playerData.playerDeck.Remove(originalCard);
            GameManager.playerData.playerDeck.Add(weapon);
        }

        shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy.Count)];

        Destroy(gameObject);
    }
    public void UpgradeArmor()
    {
        var armor = (Armor)GetComponent<CardDisplay>().cardData;

        if (GameManager.playerData.inventory.Contains(originalCard))
        {
            GameManager.playerData.inventory.Remove(originalCard);
            GameManager.playerData.inventory.Add(armor);
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
    public void ShowUpgrade()
    {
        
        if(originalCard.GetType() == typeof(Weapon))
        {
            var card = (Weapon)originalCard;
            gameObject.GetComponent<CardDisplay>().cardData = card.improvedWeapon;
            costText.text = "x" + GetComponent<CardDisplay>().cardData.money.ToString();
        }
        else if(originalCard.GetType() == typeof(Armor))
        {
            var card = (Armor)originalCard;
            gameObject.GetComponent<CardDisplay>().cardData = card.improvedArmor;
            costText.text = "x" + GetComponent<CardDisplay>().cardData.money.ToString();
        }
    }
}
