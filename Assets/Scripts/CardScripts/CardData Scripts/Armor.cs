using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New armor card", menuName = "BenditaCompostela/New armor")]
public class Armor : CardData
{
    public int defenseValue;
    public int damageBonus;
    public int extraDefence;
    public Weapon synergyWeapon;
    public TArmor armorType;
    public TSynergy synergy;
    public enum TArmor 
        { CHEST, FEET}

    public enum TSynergy
        { DAMAGE, DEFENCE, HEALING, rCOST, xVULNERABLE, xGUARDED}
}
