using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Entity : MonoBehaviour
{
    public int HP;
    public int currentHP;
    public int energy;
    public int currentEnergy;

    public List<CardData.TAlteredEffects> alteredEffects;
    public List<int> aEffectsValue;

    public GameObject alteredEffectsDisplayPrefab;
    public Sprite[] alteredEffectsImages;

    #region Logic Methods
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
        damage =
        this.currentHP = Mathf.Clamp(this.currentHP - damage, 0, HP);
    }

    public void RestoreHealth(int healthRestored)
    {
        if(CheckIfSufferingEffect(CardData.TAlteredEffects.BLEED) != -1) 
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

    /* Effect methods. This methods are used to check if the Entity suffers from various effects */
    public int CheckIfSufferingEffect(CardData.TAlteredEffects checkingEffect)
    {
        if(alteredEffects.Contains(checkingEffect)) return alteredEffects.IndexOf(checkingEffect);
        return -1;
    }

    public void ApplyAlteredEffect(CardData.TAlteredEffects alteredEffect, int value)
    {
        if(CheckIfSufferingEffect(alteredEffect) >= 0)
        {
            aEffectsValue[CheckIfSufferingEffect(alteredEffect)] += value;
            return;
        }

        alteredEffects.Add(alteredEffect);
        aEffectsValue.Add(value);

        UpdateEffectsDisplay();
    }

    public void RemoveAlteredEffect(CardData.TAlteredEffects alteredEffect)
    {
        if (CheckIfSufferingEffect(alteredEffect) < 0) return;
        aEffectsValue.Remove(CheckIfSufferingEffect(alteredEffect));
        alteredEffects.Remove(alteredEffect);

        UpdateEffectsDisplay();
    } 

    public void RemoveAlteredEffect()
    {
        alteredEffects.Clear();
        aEffectsValue.Clear();

        UpdateEffectsDisplay();
    }

    public void Bleeding()
    {
        if (CheckIfSufferingEffect(CardData.TAlteredEffects.BLEED) < 0) return;

        this.SufferDamage(1);
        this.aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.BLEED)]--;

        if (aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.BLEED)] <= 0) 
            RemoveAlteredEffect(CardData.TAlteredEffects.BLEED);

        UpdateEffectsDisplay();
    }

    public void Poison()
    {
        if (CheckIfSufferingEffect(CardData.TAlteredEffects.POISON) < 0) return;

        this.SufferDamage(1);
        this.aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.POISON)]--;

        if (aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.POISON)] <= 0) 
            RemoveAlteredEffect(CardData.TAlteredEffects.POISON);

        UpdateEffectsDisplay();
    }

    public void Burn()
    {
        if (CheckIfSufferingEffect(CardData.TAlteredEffects.BURN) < 0) return;

        this.SufferDamage(aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.BURN)]);

        RemoveAlteredEffect(CardData.TAlteredEffects.BURN);

        UpdateEffectsDisplay();
    }

    public int Vulnerable(int damage)
    {
        if (this.CheckIfSufferingEffect(CardData.TAlteredEffects.VULNERABLE) <= 0) return damage;

        this.aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.VULNERABLE)]--;

        if (aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.VULNERABLE)] <= 0) 
            RemoveAlteredEffect(CardData.TAlteredEffects.VULNERABLE);

        UpdateEffectsDisplay();

        return damage += (damage / 2);
    }

    public int Guarded(int damage)
    {
        if (this.CheckIfSufferingEffect(CardData.TAlteredEffects.GUARDED) <= 0) return damage;

        this.aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.GUARDED)]--;

        if (aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.GUARDED)] <= 0)
            RemoveAlteredEffect(CardData.TAlteredEffects.GUARDED);

        UpdateEffectsDisplay();

        return damage -= (damage / 2);
    }

    public int Invulnerable(int damage)
    {
        if (this.CheckIfSufferingEffect(CardData.TAlteredEffects.INVULNERABLE) <= 0) return damage;

        this.aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.INVULNERABLE)]--;

        if (aEffectsValue[CheckIfSufferingEffect(CardData.TAlteredEffects.INVULNERABLE)] <= 0)
            RemoveAlteredEffect(CardData.TAlteredEffects.INVULNERABLE);

        UpdateEffectsDisplay();

        return 0;
    }
    #endregion

    #region Display Methods
    public void Update()
    {
        float healthBar = (float)((float)currentHP / (float)HP) * 100;
        GetComponentInChildren<Slider>().value = healthBar;
    }

    public void UpdateEffectsDisplay()
    {
        while (transform.GetChild(1).childCount > 0)
        {
            Destroy(transform.GetChild(1).GetChild(0));
        }

        foreach (CardData.TAlteredEffects effect in alteredEffects)
        {
            GameObject newImage = Instantiate(alteredEffectsDisplayPrefab, transform.GetChild(1));
            //newImage.GetComponentInChildren<Image>().sprite = alteredEffectsImages[(int)effect];
            newImage.GetComponentInChildren<Text>().text = aEffectsValue[CheckIfSufferingEffect(effect)].ToString();
        }
    }
    #endregion
}
