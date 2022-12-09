using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Entity : MonoBehaviour
{
    public int HP;
    public int currentHP;
    public int energy;
    public int currentEnergy;

    #region Altered Effects
    //public List<CardData.TAlteredEffects> alteredEffects;
    //public List<int> aEffectsValue;
    public GameObject alteredEffectsDisplayPrefab;
    public Sprite[] alteredEffectsImages;
    public Dictionary<CardData.TAlteredEffects, int> alteredEffects = new Dictionary<CardData.TAlteredEffects, int>();
    int poisonTurns;
    #endregion

    #region Synergy Variables
    public int damageBoost = 0;
    public int defense = 0;
    public int extraHealing = 0;
    #endregion

    #region Entity Setup
    public void SetupEntity()
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log((CardData.TAlteredEffects)i);
            alteredEffects.Add((CardData.TAlteredEffects)i, 0);
        }

        foreach (KeyValuePair<CardData.TAlteredEffects, int> effect in alteredEffects)
        {
            Debug.Log(effect.Key + " - " + effect.Value);
        }
    }
    #endregion

    #region Basic Entity Logic Methods
    public void RestoreEnergy(int energyRestored)
    {
        if (energyRestored == -1)
        {
            this.currentEnergy = this.energy;
            return;
        }

        this.currentEnergy += energyRestored;
    }
    public bool ConsumeEnergy(int energyConsumed)
    {
        if (this.currentEnergy < energyConsumed) return false;
        this.currentEnergy -= energyConsumed;
        Debug.Log(currentEnergy);
        return true;
    }

    public bool IsPlayable(int energyConsumed) {
        return this.currentEnergy >= energyConsumed;
    }
    public void RefillEnergy()
    {
        this.currentEnergy = this.energy;
    }
    public void SufferDamage(int damage)
    {
        damage = Vulnerable(damage);
        damage = Guarded(damage);
        damage = Invulnerable(damage);
        damage = Mathf.Clamp(damage, 0, 99);
        this.currentHP = Mathf.Clamp(this.currentHP - damage, 0, HP);

        if (this.GetType() == typeof(PlayerScript)) GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("CharacterDamage");

        if (IsDead()) GameObject.Find("GameManager").GetComponent<GameManager>().BattleEnd(gameObject.GetComponent<Entity>());
    }
    public void SufferEffectDamage(int damage)
    {
        damage = Invulnerable(damage);
        this.currentHP = Mathf.Clamp(this.currentHP - damage, 0, HP);

        if (this.GetType() == typeof(PlayerScript)) GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("CharacterDamage");

        if (IsDead()) GameObject.Find("GameManager").GetComponent<GameManager>().BattleEnd(gameObject.GetComponent<Entity>());
    }
    public void RestoreHealth(int healthRestored)
    {
        healthRestored += extraHealing;
        if(Suffering(CardData.TAlteredEffects.BLEED)) 
        {
            this.currentHP = Mathf.Clamp(this.currentHP + (healthRestored / 2), 0, HP);
            return;
        }
        this.currentHP = Mathf.Clamp(this.currentHP + healthRestored, 0, HP);
    }
    public bool IsDead()
    {
        if (this.currentHP <= 0) return true;
        else return false;
    }
    #endregion

    #region Altered Effect Logic
    /* Effect methods. This methods are used to check if the Entity suffers from various effects */
    public bool Suffering(CardData.TAlteredEffects checkingEffect)
    {
        if (alteredEffects[checkingEffect] >= 1) return true;
        return false;
    }
    public void ApplyAlteredEffect(CardData.TAlteredEffects alteredEffect, int value)
    {
        if (ResistanceTo(alteredEffect)) return;

        switch (alteredEffect)
        {
            case CardData.TAlteredEffects.INVULNERABLE:
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, 2);
                break;
            case CardData.TAlteredEffects.BURN:
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, 10);
                break;
            case CardData.TAlteredEffects.VULNERABLE:
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, 3);
                break;
            case CardData.TAlteredEffects.GUARDED:
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, 3);
                break;
            case CardData.TAlteredEffects.BLEED:
                var limit = !IsPlayer() && PlayerScript.activeSynergy == Armor.TSynergy.xBLEED ? (5 + 1 * (PlayerScript.chestArmor.upgradeLevel + 1)) : 5;
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, limit);
                break;
            default:
                alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] + value, 0, 5);
                break;
        }
        
        UpdateEffectsDisplay();
    }
    public void RemoveAlteredEffect(CardData.TAlteredEffects alteredEffect)
    {
        if (!Suffering(alteredEffect)) return;

        Debug.Log("INDEX IN LIST:" + Suffering(alteredEffect));

        if(alteredEffect == CardData.TAlteredEffects.POISON) poisonTurns = 0;

        alteredEffects[alteredEffect] = 0;

        UpdateEffectsDisplay();
    } 
    public void RemoveAlteredEffect()
    {
        List<CardData.TAlteredEffects> SufferingEffects = new List<CardData.TAlteredEffects>();
        foreach(KeyValuePair<CardData.TAlteredEffects,int> effect in alteredEffects)
        {
            if (effect.Key == CardData.TAlteredEffects.INVULNERABLE || effect.Key == CardData.TAlteredEffects.GUARDED) continue;
            if (Suffering(effect.Key)) SufferingEffects.Add(effect.Key);
        }

        foreach(CardData.TAlteredEffects alteredEffect in SufferingEffects)
        {
            RemoveAlteredEffect(alteredEffect);
        }

        poisonTurns = 0;

        UpdateEffectsDisplay();
    }
    public void ReduceAlteredEffect(CardData.TAlteredEffects alteredEffect, int charges)
    {
        if (!Suffering(alteredEffect)) return;

        alteredEffects[alteredEffect] = Mathf.Clamp(alteredEffects[alteredEffect] - charges, 0, 5);

        Debug.Log("REDUCED EFFECT: " + alteredEffect.ToString());

        UpdateEffectsDisplay();
    }

    /* Logical functioning of the different altered effects */
    public void Bleeding()
    {
        if (!Suffering(CardData.TAlteredEffects.BLEED)) return;

        var effectCharges = alteredEffects[CardData.TAlteredEffects.BLEED];

        if (IsBoss(Enemy.Boss.SANTIAGO)) effectCharges--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) effectCharges += 3;

        SufferEffectDamage(effectCharges);

        ReduceAlteredEffect(CardData.TAlteredEffects.BLEED, 1);

        Debug.Log("EFFECT: Bleed");
    }
    public void Poison()
    {
        if (!Suffering(CardData.TAlteredEffects.POISON)) { poisonTurns = 0; return; }
        
        poisonTurns = Mathf.Clamp(poisonTurns + 2, 0, 6);
        var effectCharges = poisonTurns;

        if (IsBoss(Enemy.Boss.SANTIAGO)) effectCharges--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) effectCharges += 3;

        SufferEffectDamage(effectCharges);

        ReduceAlteredEffect(CardData.TAlteredEffects.POISON, 1);

        Debug.Log("EFFECT: Poison");
    }
    public void Burn()
    {
        if (!Suffering(CardData.TAlteredEffects.BURN)) return;

        var effectCharges = alteredEffects[CardData.TAlteredEffects.BURN];

        SufferEffectDamage((int)GetBurn());

        RemoveAlteredEffect(CardData.TAlteredEffects.BURN);

        Debug.Log("EFFECT: Burn");
    }
    public bool Disarmed()
    {
        Debug.Log("EFFECT: Disarmed");
        if (!Suffering(CardData.TAlteredEffects.DISARMED)) return false;

        return true;
    }
    public int Vulnerable(int damage)
    {
 
        if (!Suffering(CardData.TAlteredEffects.VULNERABLE)) return damage; 

        Debug.Log("EFFECT: Vulnerable");

        RemoveAlteredEffect(CardData.TAlteredEffects.VULNERABLE);

        return damage += Mathf.Clamp((int)Mathf.Round(damage * GetVulnerable()), 1, 99);
    }
    public int Guarded(int damage)
    {
        if (!Suffering(CardData.TAlteredEffects.GUARDED)) return damage;

        Debug.Log("EFFECT: Guarded");

        ReduceAlteredEffect(CardData.TAlteredEffects.GUARDED, 1);

        return damage -= Mathf.Clamp((int)Mathf.Round(damage * GetGuarded()),1,99);
    }
    public int Invulnerable(int damage)
    {
        if (!Suffering(CardData.TAlteredEffects.INVULNERABLE)) return damage;

        Debug.Log("EFFECT: Invulnerable");

        ReduceAlteredEffect(CardData.TAlteredEffects.INVULNERABLE, 1);

        UpdateEffectsDisplay();

        return 0;
    }
    #endregion

    #region Display Methods
    public void Update()
    {
        float healthBar = (float)((float)currentHP / (float)HP) * 100;
        float energyBar = (float)((float)currentEnergy / (float)energy) * 100;
        gameObject.transform.GetChild(1).GetComponent<Slider>().value = healthBar;
        gameObject.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = currentHP.ToString();
        if (this.GetType() == typeof(PlayerScript))
        {
            gameObject.transform.GetChild(3).GetComponent<Slider>().value = energyBar;
            gameObject.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = currentEnergy.ToString();
        }
    }

    public void UpdateEffectsDisplay()
    {
        foreach(Transform child in transform.GetChild(2).GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<CardData.TAlteredEffects, int> effect in alteredEffects)
        {
            if (!Suffering(effect.Key)) continue;
            //alteredEffectsDisplayPrefab.GetComponentInChildren<Image>().sprite = alteredEffectsDisplayPrefab.GetComponent<AlteredEffectsSprites>().sprites[(int)effect.Key];
            alteredEffectsDisplayPrefab.GetComponent<AlteredEffectsSprites>().effect = effect.Key;
            alteredEffectsDisplayPrefab.GetComponent<AlteredEffectsSprites>().value = effect.Value;
            GameObject newImage = Instantiate(alteredEffectsDisplayPrefab, transform.GetChild(2));
            //newImage.GetComponentInChildren<TextMeshProUGUI>().text = "x" + alteredEffects[effect.Key].ToString();
        }
    }

    public void AttackAnimation(bool state)
    {
        GetComponent<Animator>().SetBool("Attack", state);
    }
    #endregion

    #region Boss And Resistances

    public bool ResistanceTo(CardData.TAlteredEffects aEffect)
    {
        if (this.GetType() != typeof(EnemyScript)) return false;

        if (this.GetComponent<EnemyScript>().enemyData.resistances.Contains(aEffect)) return true;

        return false;
    }
    public bool IsBoss(Enemy.Boss isBoss)
    {
        if (this.GetType() != typeof(EnemyScript)) return false;

        if (GetComponent<EnemyScript>().enemyData.IsBoss(isBoss)) return true;

        return false;
    }
    public bool BossDebuff(Enemy.Boss isBoss)
    {
        if (this.GetType() != typeof(PlayerScript)) return false;

        if (GetComponent<PlayerScript>().enemy.GetComponent<EnemyScript>().enemyData.IsBoss(isBoss)) return true;

        return false;
    }

    #endregion

    #region Entity Type
    public bool IsPlayer()
    {
        return this.GetType() == typeof(PlayerScript);
    }
    #endregion

    #region Math
    /* Various math methods placed here to avoid overcrowd of effect methods */
    public float GetVulnerable()
    {
        /*
         * 1 Stack = 10%, 2 stack = 25%, 3 stack = 50%
         * 1 armor = 15, 30, 55
         * 2 armor = 15, 33, 66
         * 3 armor = 25, 50, 75
         */

        var multiplier = 0.0f;

        switch (alteredEffects[CardData.TAlteredEffects.VULNERABLE])
        {
            case 1:
                multiplier += 0.1f;
                break;
            case 2:
                multiplier += 0.25f;
                break;
            case 3:
                multiplier += 0.50f;
                break;
        }

        if(!IsPlayer() && PlayerScript.activeSynergy == Armor.TSynergy.xVULNERABLE)
        {
            switch (PlayerScript.chestArmor.upgradeLevel)
            {
                case 0:
                    multiplier += 0.05f;
                    break;
                case 1:
                    multiplier += 0.08f;
                    break;
                case 2:
                    multiplier += 0.25f;
                    break;
            }
        }
        return multiplier;
    }
    public float GetGuarded()
    {
        var multiplier = 0.0f;

        switch (alteredEffects[CardData.TAlteredEffects.GUARDED])
        {
            case 1:
                multiplier += 0.1f;
                break;
            case 2:
                multiplier += 0.25f;
                break;
            case 3:
                multiplier += 0.50f;
                break;
        }

        if (!IsPlayer() && PlayerScript.activeSynergy == Armor.TSynergy.xGUARDED)
        {
            switch (PlayerScript.chestArmor.upgradeLevel)
            {
                case 0:
                    multiplier += 0.05f;
                    break;
                case 1:
                    multiplier += 0.08f;
                    break;
                case 2:
                    multiplier += 0.25f;
                    break;
            }
        }
        return multiplier;
    }
    public float GetBurn()
    {
        var multiplier = 1.0f;
        var stacks = alteredEffects[CardData.TAlteredEffects.BURN];

        if (stacks > 5) multiplier = 2.0f;

        if(!IsPlayer() && PlayerScript.activeSynergy == Armor.TSynergy.xBURN)
        {
            switch (PlayerScript.chestArmor.upgradeLevel)
            {
                case 0:
                    multiplier += 0.25f;
                    break;
                case 1:
                    multiplier += 0.5f;
                    break;
                case 2:
                    multiplier += 1.0f;
                    break;
            }
        }

        var damage = stacks * multiplier;

        if (IsBoss(Enemy.Boss.SANTIAGO)) damage--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) damage += 3;

        return Mathf.Round(damage);
    }
    #endregion
}
