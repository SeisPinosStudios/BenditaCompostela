using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon card", menuName = "BenditaCompostela/New weapon")]
public class Weapon : CardData
{
    public int weaponId;
    public List<CardData> attackList;
    public Weapon improvedWeapon;
    public Weapon previousWeapon;
    public int upgradeLevel;
}
