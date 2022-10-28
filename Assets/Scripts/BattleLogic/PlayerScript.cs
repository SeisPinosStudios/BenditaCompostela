using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : Entity
{
    #region Player variables
    public Player playerData;
    public Weapon weapon;
    public List<CardData> playerDeck;
    #endregion

    public void Awake()
    {
        PlayerConfig();
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            Debug.Log("Hola");
            GameObject.Find("TurnButton").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OnTurnEnd()));
        }
    }
    
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
    public void PlayerConfig()
    {
        playerData = GameManager.playerData;
        HP = playerData.HP;
        currentHP = HP;
        energy = playerData.energy;
        currentEnergy = energy;
        playerDeck = playerData.playerDeck;
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
}
