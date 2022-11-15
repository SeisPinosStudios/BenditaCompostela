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
        if (pointer.button == PointerEventData.InputButton.Left)
        {
            if (GameManager.playerData.CoinDecrease(3))
            {
                GameManager.playerData.inventory.Add(gameObject.GetComponent<CardDisplay>().cardData);
                Destroy(gameObject);
            }
        }
    }
}
