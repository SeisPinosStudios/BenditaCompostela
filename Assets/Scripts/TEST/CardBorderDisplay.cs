using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBorderDisplay : MonoBehaviour
{
    private Animator cardAn;

    private void Awake()
    {
        cardAn = gameObject.GetComponent<Animator>();
    }

    public void CardActive() 
    {
        Debug.Log("ACTIVE ANIMATION");
        cardAn.SetBool("isActive", true);
    }

    public void CardInactive()
    {
        Debug.Log("INACTIVE ANIMATION");
        cardAn.SetBool("isActive", false);
    }

    public void CardPlayable() 
    {
        cardAn.SetBool("isPlayable", true);
    }

    public void CardUnplayable()
    {
        cardAn.SetBool("isPlayable", false);
    }
}
