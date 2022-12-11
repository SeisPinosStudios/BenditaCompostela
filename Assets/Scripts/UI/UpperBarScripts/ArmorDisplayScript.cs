using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDisplayScript : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform display;
    bool showing = false;
    public void ShowArmor()
    {
        if (!showing)
        {
            cardPrefab.GetComponent<CardDisplay>().cardData = GameManager.playerData.chestArmor;
            Instantiate(cardPrefab, display);
            cardPrefab.GetComponent<CardDisplay>().cardData = GameManager.playerData.feetArmor;
            Instantiate(cardPrefab, display);
            showing = !showing;
        }
        else
        {
            foreach(Transform child in display) Destroy(child.gameObject);
            showing = !showing;
        }
    }
}
