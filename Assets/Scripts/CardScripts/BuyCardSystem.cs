using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyCardSystem : MonoBehaviour, IPointerClickHandler
{
    private PlayerScript player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerScript>();
    }
    public void OnPointerClick(PointerEventData pointer) {
        if (pointer.button == PointerEventData.InputButton.Left)
        {            
            player.substractPlayerCoins(3);
            Destroy(gameObject);
        }
    }
}
