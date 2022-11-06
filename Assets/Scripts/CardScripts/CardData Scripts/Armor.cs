using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New armor card", menuName = "BenditaCompostela/New armor")]
public class Armor : CardData
{
    public int defenseValue;
    public List<Weapon> synergies;
    public TArmor armorType;
    public enum TArmor 
        { CHEST, FEET}
}
