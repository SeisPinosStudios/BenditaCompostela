using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : Entity
{
    #region Player variables
    public Weapon weapon;
    public List<CardData> playerDeck = new List<CardData>();

    public void Awake()
    {
        //DeactivateCombatControl();
    }

    #endregion
    public void OnTurnBegin()
    {
        currentEnergy = energy;
        ActivateCombatControl();
    }

    public void OnTurnEnd()
    {
        DeactivateCombatControl();
    }

    public void ActivateCombatControl()
    {
        foreach (Card card in FindObjectsOfType<Card>()) card.GetComponent<CardDragSystem>().enabled = true;
        FindObjectOfType<DefaultDeck>().GetComponent<Button>().enabled = true;
        FindObjectOfType<AttackDeck>().GetComponent<Button>().enabled = true;
    }

    public void DeactivateCombatControl()
    {
        foreach (Card card in FindObjectsOfType<Card>()) card.GetComponent<CardDragSystem>().enabled = false;
        FindObjectOfType<DefaultDeck>().GetComponent<Button>().enabled = false;
        FindObjectOfType<AttackDeck>().GetComponent<Button>().enabled = false;
    }
}
