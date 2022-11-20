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
    public List<CardData.TAlteredEffects> alteredEffects;
    public List<int> aEffectsValue;
    public GameObject alteredEffectsDisplayPrefab;
    public Sprite[] alteredEffectsImages;
    int poisonTurns;
    #endregion

    #region Synergy Variables
    public int damageBoost = 0;
    public int defence = 0;
    public int extraHealing = 0;
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
        this.currentHP = Mathf.Clamp(this.currentHP - damage, 0, HP);

        if (this.GetType() == typeof(PlayerScript)) GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("CharacterDamage");

        if (IsDead()) GameObject.Find("GameManager").GetComponent<GameManager>().BattleEnd(gameObject.GetComponent<Entity>());
    }
    public void RestoreHealth(int healthRestored)
    {
        healthRestored += extraHealing;
        if(Suffering(CardData.TAlteredEffects.BLEED) != -1) 
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
    public int Suffering(CardData.TAlteredEffects checkingEffect)
    {
        if(alteredEffects.Contains(checkingEffect)) return alteredEffects.IndexOf(checkingEffect);
        return -1;
    }
    public void ApplyAlteredEffect(CardData.TAlteredEffects alteredEffect, int value)
    {
        if (ResistanceTo(alteredEffect)) return;

        if(Suffering(alteredEffect) >= 0)
        {
            if(alteredEffect == CardData.TAlteredEffects.INVULNERABLE) aEffectsValue[Suffering(alteredEffect)] = Mathf.Clamp(aEffectsValue[Suffering(alteredEffect)] + value, 0, 2);
            else aEffectsValue[Suffering(alteredEffect)] = Mathf.Clamp(aEffectsValue[Suffering(alteredEffect)] + value, 0, 5);
            UpdateEffectsDisplay();
            return;
        }

        alteredEffects.Add(alteredEffect);
        aEffectsValue.Add(value);

        UpdateEffectsDisplay();
    }
    public void RemoveAlteredEffect(CardData.TAlteredEffects alteredEffect)
    {
        if (Suffering(alteredEffect) < 0) return;

        Debug.Log("INDEX IN LIST:" + Suffering(alteredEffect));

        aEffectsValue.Remove(Suffering(alteredEffect));
        alteredEffects.Remove(alteredEffect);

        if (alteredEffect == CardData.TAlteredEffects.POISON) poisonTurns = 0;

        Debug.Log("REMOVED EFFECT: " + alteredEffect.ToString());

        if(aEffectsValue.Contains(0)) aEffectsValue.Remove(aEffectsValue.IndexOf(0));

        UpdateEffectsDisplay();
    } 
    public void RemoveAlteredEffect()
    {
        foreach(CardData.TAlteredEffects effect in alteredEffects)
        {
            if (effect == CardData.TAlteredEffects.INVULNERABLE || effect == CardData.TAlteredEffects.GUARDED) continue;
            RemoveAlteredEffect(effect);
        }
        UpdateEffectsDisplay();
    }
    public void ReduceAlteredEffect(CardData.TAlteredEffects alteredEffect, int charges)
    {
        if (Suffering(alteredEffect) < 0) return;

        aEffectsValue[Suffering(alteredEffect)] -= charges;

        Debug.Log("REDUCED EFFECT: " + alteredEffect.ToString());

        if (aEffectsValue[Suffering(alteredEffect)] <= 0)
            RemoveAlteredEffect(alteredEffect);

        UpdateEffectsDisplay();
    }

    /* Logical functioning of the different altered effects */
    public void Bleeding()
    {
        if (Suffering(CardData.TAlteredEffects.BLEED) < 0) return;

        var effectCharges = aEffectsValue[Suffering(CardData.TAlteredEffects.BLEED)];

        if (IsBoss(Enemy.Boss.SANTIAGO)) effectCharges--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) effectCharges += 3;

        SufferEffectDamage(effectCharges);

        ReduceAlteredEffect(CardData.TAlteredEffects.BLEED, 1);

        Debug.Log("EFFECT: Bleed");
    }
    public void Poison()
    {
        if (Suffering(CardData.TAlteredEffects.POISON) < 0) return;

        poisonTurns++;
        var effectCharges = poisonTurns;

        if (IsBoss(Enemy.Boss.SANTIAGO)) effectCharges--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) effectCharges += 3;

        SufferEffectDamage(effectCharges);

        ReduceAlteredEffect(CardData.TAlteredEffects.POISON, 1);

        Debug.Log("EFFECT: Poison");
    }
    public void Burn()
    {
        if (Suffering(CardData.TAlteredEffects.BURN) < 0) return;

        var effectCharges = aEffectsValue[Suffering(CardData.TAlteredEffects.BURN)];

        if (IsBoss(Enemy.Boss.SANTIAGO)) effectCharges--;
        if (BossDebuff(Enemy.Boss.SANTIAGO)) effectCharges += 3;

        SufferEffectDamage(effectCharges);

        ReduceAlteredEffect(CardData.TAlteredEffects.BURN, effectCharges);

        Debug.Log("EFFECT: Burn");
    }
    public bool Disarmed()
    {
        Debug.Log("EFFECT: Disarmed");
        if (Suffering(CardData.TAlteredEffects.DISARMED) < 0) return false;

        return true;
    }
    public int Vulnerable(int damage)
    {
        
        if (this.Suffering(CardData.TAlteredEffects.VULNERABLE) < 0) return damage;

        Debug.Log("EFFECT: Vulnerable");

        ReduceAlteredEffect(CardData.TAlteredEffects.VULNERABLE, 1);

        UpdateEffectsDisplay();

        if(this.GetType() == typeof(Enemy) && GameObject.Find("Player").GetComponent<PlayerScript>().activeSynergy == Armor.TSynergy.xVULNERABLE)
            return damage += (int)Mathf.Round((float)damage * 0.6f);

        return damage += (int)Mathf.Round((float)damage * 0.5f);
    }
    public int Guarded(int damage)
    {
        if (this.Suffering(CardData.TAlteredEffects.GUARDED) < 0) return damage;

        Debug.Log("EFFECT: Guarded");

        ReduceAlteredEffect(CardData.TAlteredEffects.GUARDED, 1);

        UpdateEffectsDisplay();

        if (this.GetType() == typeof(Enemy) && GameObject.Find("Player").GetComponent<PlayerScript>().activeSynergy == Armor.TSynergy.xGUARDED)
            return damage -= (int)Mathf.Round((float)damage * 0.3f);

        return damage -= (int)Mathf.Round((float)damage * 0.5f);
    }
    public int Invulnerable(int damage)
    {
        if (this.Suffering(CardData.TAlteredEffects.INVULNERABLE) < 0) return damage;

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

        foreach (CardData.TAlteredEffects effect in alteredEffects)
        {
            alteredEffectsDisplayPrefab.GetComponentInChildren<Image>().sprite = alteredEffectsDisplayPrefab.GetComponent<AlteredEffectsSprites>().sprites[(int)effect];
            GameObject newImage = Instantiate(alteredEffectsDisplayPrefab, transform.GetChild(2));
            newImage.GetComponentInChildren<TextMeshProUGUI>().text = "x" + aEffectsValue[Suffering(effect)].ToString();
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

        if (GetComponent<EnemyScript>().enemyData.IsBoss(Enemy.Boss.SANTIAGO)) return true;

        return false;
    }

    public bool BossDebuff(Enemy.Boss isBoss)
    {
        if (this.GetType() != typeof(PlayerScript)) return false;

        if (GetComponent<PlayerScript>().enemy.GetComponent<EnemyScript>().enemyData.IsBoss(Enemy.Boss.SANTIAGO)) return true;

        return false;
    }

    #endregion
}
