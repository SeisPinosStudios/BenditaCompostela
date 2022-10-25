using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int HP;
    public int currentHP;
    public int energy;
    public int currentEnergy;

    public List<CardData.TAlteredEffects> effects;
    public List<int> effectValue;

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
        if(effects.Contains(checkingEffect)) return effects.IndexOf(checkingEffect);
        return -1;
    }

    public void ApplyEffect(CardData.TAlteredEffects effect, int value)
    {
        if(CheckIfSufferingEffect(effect) >= 0)
        {
            effectValue[CheckIfSufferingEffect(effect)] += value;
            return;
        }

        effects.Add(effect);
        effectValue.Add(value);
    }

    public void RemoveEffect(CardData.TAlteredEffects effect)
    {
        if (CheckIfSufferingEffect(effect) < 0) return;
        effectValue.Remove(CheckIfSufferingEffect(effect));
        effects.Remove(effect);
    } 

    public void RemoveEffect()
    {
        effects.Clear();
        effectValue.Clear();
    }
}
