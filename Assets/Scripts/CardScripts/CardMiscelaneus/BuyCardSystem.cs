using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuyCardSystem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int cost;
    bool inCard;
    GameObject shopText;
    private void Awake()
    {
        shopText = GameObject.Find("ShopTextSlot");
    }
    public void OnPointerClick(PointerEventData pointer) {
        var card = GetComponent<CardDisplay>().cardData;
        if (pointer.button == PointerEventData.InputButton.Left)
        {
            BuyCard(card.money);
        }
    }
    public void BuyCard(int price)
    {
        var card = GetComponent<CardDisplay>().cardData;
        if (GameManager.playerData.CoinDecrease(price))
        {
            switch (card.GetType().ToString())
            {
                case "ObjectCard":
                    shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().normalBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().normalBuy.Count)];
                    break;
                case "Special":
                    shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().specialBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().specialBuy.Count)];
                    break;
                case "Armor":
                    shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().armorBuy.Count)];
                    break;
                case "Weapon":
                    shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().weaponBuy.Count)];
                    break;
            }
            GameManager.playerData.inventory.Add(gameObject.GetComponent<CardDisplay>().cardData);
            Destroy(gameObject);
        }
        else
        {
            shopText.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney[Random.Range(0, GameObject.Find("===SHOP===").GetComponent<Shop>().noMoney.Count)];
        }
    }
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        var card = GetComponent<CardDisplay>().cardData;
        if (inCard) return;
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "Esa carta cuesta " + card.money + " monedas de oro... ¿Tenemos trato?";
        inCard = true;
    }
    public void OnPointerExit(PointerEventData pointerEvent)
    {
        shopText.GetComponentInChildren<TextMeshProUGUI>().text = "";
        inCard = false;
    }
}
