using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public void Start()
    {
        
    }

    
    public void UseCard()
    {
        if (gameObject.GetComponent<CardDisplay>().cardData.GetType() == typeof(Weapon)) EquipWeapon();
    }

    public void EquipWeapon()
    {
        Weapon weapon = (Weapon)gameObject.GetComponent<CardDisplay>().cardData;
        GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerCombatScript>().weapon = weapon.weaponId;
    }
}
