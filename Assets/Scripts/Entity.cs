using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int HP;
    public int currentHP;
    public int energy;
    public int currentEnergy;

    public CardData.TEffect[] effects;
    public int[] effectValue;

    public void RestoreEnergy(int energyRestored)
    {
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
        this.currentHP = Mathf.Clamp(this.currentHP + healthRestored, 0, HP);
    }

    public bool IsDead()
    {
        if (this.currentHP <= 0) return true;
        else return false;
    }

    /* Effect methods. This methods are used to check if the Entity suffers from various effects */
    public int CheckIfSufferingEffect(CardData.TEffect checkingEffect)
    {
        for(int i = 0; i < this.effects.Length; i++)
        {
            if (this.effects[i] == checkingEffect) return this.effectValue[i];
        }
        return -1;
    }
}
