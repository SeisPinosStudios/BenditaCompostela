using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : Entity
{
    #region Player variables
    public Weapon weapon;
    public List<CardData> playerDeck = new List<CardData>();
    
    [SerializeField] private int coins;
    [SerializeField] private CoinCounter coinCounter;
   

    public void Awake()
    {
        addPlayerCoins(20);
        currentHP = HP;
        if (OnCombat)
        {
            GameObject.Find("TurnButton").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OnTurnEnd()));
        }
        
    }

    #endregion

    #region Scene and Dialogue Interact Setup
    static public bool OnCombat;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInterctable Interctable { get; set; }

    public void DialogueActivation()
    {
        Interctable?.Interact(this);
    }
    #endregion
    public IEnumerator OnTurnBegin()
    {
        DeactivateCombatControl();
        currentEnergy = energy;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DrawCard(5);
        yield return new WaitForSeconds(3.0f);
        this.Poison();
        ActivateCombatControl();
    }

    public IEnumerator OnTurnEnd()
    {
        yield return StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DiscardCorroutine());
        DeactivateCombatControl();
        GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().Turn();
    }

    public void ActivateCombatControl()
    {
        foreach (Card card in FindObjectsOfType<Card>()) card.GetComponent<CardDragSystem>().enabled = true;
        GameObject.Find("DefaultDeck").GetComponent<Button>().enabled = true;
        GameObject.Find("AttackDeck").GetComponent<Button>().enabled = true;
        GameObject.Find("TurnButton").GetComponent <Button>().enabled = true;
    }

    public void DeactivateCombatControl()
    {
        foreach (Card card in FindObjectsOfType<Card>()) card.GetComponent<CardDragSystem>().enabled = false;
        GameObject.Find("DefaultDeck").GetComponent<Button>().enabled = false;
        GameObject.Find("AttackDeck").GetComponent<Button>().enabled = false;
        GameObject.Find("TurnButton").GetComponent<Button>().enabled = false;
    }



    #region Coin Getters & Setters
    public void addPlayerCoins(int newCoins) {
        coins += newCoins;
        coinCounter.updateCoinCounter(coins.ToString());
    }
    public void substractPlayerCoins(int newCoins) {
        coins -= newCoins;
        if (coins < 0)
        {
            coins = 0;
        }
        coinCounter.updateCoinCounter(coins.ToString());
    }
    public int getPlayerCoins() {
        return coins;
    }
    #endregion
}
