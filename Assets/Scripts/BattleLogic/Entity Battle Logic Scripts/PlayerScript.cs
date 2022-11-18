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
    public Armor feetArmor;
    public Armor chestArmor;
    public GameObject enemy;
    #endregion

    #region Synergy Variables
    /* The variables inside this region manage the
     * synergies between the weapons and the equiped armor.
     * Pending future modifications */
    int tempDefence = 0;
    public Armor.TSynergy activeSynergy;
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
        yield return StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DrawCardCorroutine(5));
        this.Poison();
        ActivateCombatControl();
    }
    public IEnumerator OnTurnEnd()
    {
        DeactivateCombatControl();
        yield return StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DiscardCorroutine());
        GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().Turn();
    }
    public void PlayerConfig()
    {
        playerData = GameManager.playerData;
        HP = playerData.maxHP;
        currentHP = playerData.currentHP;
        energy = playerData.energy;
        currentEnergy = energy;
        playerDeck = playerData.playerDeck;
        chestArmor = playerData.chestArmor;
        feetArmor = playerData.feetArmor;
        if (chestArmor != null) defence += chestArmor.defenseValue;
        if (feetArmor != null) defence += feetArmor.defenseValue;
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
    public void OnWeaponEquiped(Weapon newWeapon)
    {
        GameObject.Find("AttackDeck").GetComponent<Button>().enabled = true;
        foreach (Transform card in GameObject.Find("HandPanel").transform)
        {
            if (card.GetComponent<CardDisplay>().cardData.GetType().ToString() == "Attack") Destroy(card.gameObject);
        }

        weapon = newWeapon;

        CheckForSynergy();
    }
    public void CheckForSynergy()
    {
        defence -= tempDefence;
        damageBoost = 0;
        extraHealing = 0;

        ChestSynergy();
        FeetSynergy();

        defence += tempDefence;
    }
    public void ChestSynergy()
    {
        if (chestArmor == null || chestArmor.synergyWeapon == null) return;

        if (weapon.name == chestArmor.synergyWeapon.name)
        {
            switch (chestArmor.synergy.ToString())
            {
                case "DAMAGE":
                    damageBoost += chestArmor.damageBonus;
                    break;
                case "DEFENCE":
                    tempDefence += chestArmor.extraDefence;
                    break;
                case "HEALING":
                    extraHealing += 5;
                    break;
                default:
                    activeSynergy = chestArmor.synergy;
                    break;
            }
        }
    }
    public void FeetSynergy()
    {
        if (feetArmor == null || feetArmor.synergyWeapon == null) return;

        if (weapon.name == feetArmor.synergyWeapon.name)
        {
            switch (feetArmor.synergy.ToString())
            {
                case "DAMAGE":
                    damageBoost += feetArmor.damageBonus;
                    break;
                case "DEFENCE":
                    tempDefence += feetArmor.extraDefence;
                    break;
                case "HEALING":
                    extraHealing += 1;
                    break;
            }
        }
    }
}
