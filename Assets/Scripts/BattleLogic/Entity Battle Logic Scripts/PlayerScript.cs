using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : Entity
{
    #region Player variables
    public Player playerData;
    public Weapon weapon;
    public List<CardData> playerDeck;
    public static Armor feetArmor;
    public static Armor chestArmor;
    public GameObject enemy;
    #endregion

    #region Synergy Variables
    /* The variables inside this region manage the
     * synergies between the weapons and the equiped armor.
     * Pending future modifications */
    int tempDefence = 0;
    int extraEnergy = 0;
    public static Armor.TSynergy activeSynergy;
    public  Armor.TSynergy synergy;
    #endregion

    #region Other Variables
    public Transform equipedWeapon;
    public GameObject cardPrefab;
    #endregion
    public void Awake()
    {
        PlayerConfig();
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            GameObject.Find("TurnButton").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OnTurnEnd()));            
        }
    }
    public IEnumerator OnTurnBegin()
    {
        DeactivateCombatControl();
        currentEnergy = energy + extraEnergy;
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DrawCardCorroutine(5));
        this.Poison();
        ActivateCombatControl();
    }
    public IEnumerator OnTurnEnd()
    {
        foreach (Transform child in GameObject.Find("HandPanel").transform) child.GetComponent<CardInspection>().canInspect = false;
        DeactivateCombatControl();
        yield return StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DiscardCorroutine());
        Burn();
        if (Suffering(CardData.TAlteredEffects.BURN))
        {
            yield return new WaitForSeconds(1);
            if (IsDead()) yield return new WaitForSeconds(2);
        }
        GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().Turn();
    }
    public void PlayerConfig()
    {
        SetupEntity();
        playerData = GameManager.playerData;
        HP = playerData.maxHP;
        currentHP = playerData.currentHP;
        energy = playerData.energy;
        currentEnergy = energy;
        playerDeck = playerData.playerDeck;
        chestArmor = playerData.chestArmor;
        feetArmor = playerData.feetArmor;
        if (chestArmor != null) defense += chestArmor.defenseValue;
        if (feetArmor != null) defense += feetArmor.defenseValue;
    }
    public void ActivateCombatControl()
    {
        foreach (Transform card in GameObject.Find("HandPanel").transform) card.gameObject.GetComponent<CardDragSystem>().enabled = true;
        GameObject.Find("AttackDeck").GetComponent<Button>().enabled = true;
        GameObject.Find("TurnButton").GetComponent <Button>().enabled = true;
    }
    public void DeactivateCombatControl()
    {
        foreach (Transform card in GameObject.Find("HandPanel").transform) card.gameObject.GetComponent<CardDragSystem>().enabled = false;
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

        if (equipedWeapon.childCount > 0) Destroy(equipedWeapon.GetChild(0).gameObject);

        cardPrefab.GetComponent<CardDisplay>().cardData = weapon;
        Instantiate(cardPrefab, equipedWeapon);

        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("EquipWeapon");
    }
    public void CheckForSynergy()
    {
        defense -= tempDefence;
        damageBoost = 0;
        extraHealing = 0;
        extraEnergy = 0;

        ChestSynergy();
        FeetSynergy();

        defense += tempDefence;
        synergy = activeSynergy;
    }
    public void ChestSynergy()
    {
        if (chestArmor == null || chestArmor.synergyWeapon.Count < 3) return;

        foreach(Weapon weaponSynergy in chestArmor.synergyWeapon)
        {
            Debug.Log(weaponSynergy.name);
            if(weapon.name == weaponSynergy.name)
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
                        extraHealing += 3*(Mathf.Clamp(chestArmor.upgradeLevel, 1, 2));
                        break;
                    default:
                        activeSynergy = chestArmor.synergy;
                        break;
                }

                return;
            }
        }
    }
    public void FeetSynergy()
    {
        if (feetArmor == null || feetArmor.synergyWeapon.Count < 3) return;

        foreach (Weapon weaponSynergy in feetArmor.synergyWeapon)
        {
            if (weapon.name == weaponSynergy.name)
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
                        extraHealing += (1+feetArmor.upgradeLevel);
                        break;
                    case "ENERGY":
                        extraEnergy += (2 + feetArmor.upgradeLevel);
                        break;
                }
            }
        }
    }
    public CardData GetStolableCard()
    {
        var specialDeck = playerDeck.Where((card) => card.GetType() == typeof(Special)).ToList().OfType<Special>().Where((card) => card.effects.Contains(CardData.TEffects.rHEALTH) || card.effects.Contains(CardData.TEffects.CLEANSE) || card.effects.Length == 0).ToList().OfType<CardData>();
        var objectDeck = playerDeck.Where((card) => card.GetType() == typeof(ObjectCard)).ToList().OfType<ObjectCard>().Where((card) => card.effects.Contains(CardData.TEffects.rHEALTH) || card.effects.Contains(CardData.TEffects.CLEANSE) || card.effects.Length == 0).ToList().OfType<CardData>();
        var deck = specialDeck.Concat(objectDeck).ToList();

        return deck[Random.Range(0, deck.Count)];
    }
}
