using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttacksDisplay : MonoBehaviour
{
    public Transform display;
    public GameObject cardPrefab;
    public PlayerScript player;
    bool showing = false;
    int index;

    public void ShowAttacks()
    {
        if (!showing)
        {
            if (player.weapon == null) return;
            foreach (CardData card in player.weapon.attackList)
            {
                cardPrefab.GetComponent<CardDisplay>().cardData = card;
                Instantiate(cardPrefab, display.transform);
            }
            showing = !showing;
        }
        else
        {
            foreach (Transform child in display.transform) Destroy(child.gameObject);
            showing = !showing;
        }
    }


}
