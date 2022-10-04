using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData cardData;

    public void Start()
    {
        
    }

    public void UseCard()
    {
        cardData = gameObject.GetComponent<CardDisplay>().cardData;

        switch (cardData.effect)
        {
            case 0:
                Debug.Log(cardData.name + " equiped.");
                break;
            case 1:
                Debug.Log(cardData.attackDamage + " damage dealt.");
                break;
            default:
                Debug.Log("This should not appear, report this bug to the developer team.");
                break;
        }

        gameObject.GetComponent<CardDisplay>().EnableHandPanel();
        Destroy(gameObject);
    }
}
