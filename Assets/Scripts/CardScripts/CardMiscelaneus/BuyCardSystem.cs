using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCardSystem : MonoBehaviour, IPointerClickHandler
{
    private PlayerScript player;
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerScript>();
    }
    public void OnPointerClick(PointerEventData pointer) {
        var card = GetComponent<CardDisplay>().cardData;

        if (pointer.button == PointerEventData.InputButton.Left)
        {
            if(card.GetType() == typeof(Armor) || card.GetType() == typeof(Weapon)) BuyCard(5);

            if (card.GetType() == typeof(ObjectCard)) BuyCard(card.cost);

            if(card.GetType() == typeof(Special)) BuyCard(card.cost + 2);
        }
    }

    public void BuyCard(int price)
    {
        if (GameManager.playerData.CoinDecrease(price))
        {
            GameManager.playerData.inventory.Add(gameObject.GetComponent<CardDisplay>().cardData);
            Destroy(gameObject);
        }
    }
}
