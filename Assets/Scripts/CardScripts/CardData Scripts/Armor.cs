using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New armor card", menuName = "BenditaCompostela/New armor")]
public class Armor : CardData
{
    public int defenseValue;
    public int damageBonus;
    public int extraDefence;
    public List<Weapon> synergyWeapon;
    public TArmor armorType;
    public TSynergy synergy;
    public int upgradeLevel;
    public Armor improvedArmor;
    public enum TArmor 
        { CHEST, FEET}

    public enum TSynergy
        { DAMAGE, DEFENCE, HEALING, ENERGY, xVULNERABLE, xGUARDED}

    public int armorId;
}
