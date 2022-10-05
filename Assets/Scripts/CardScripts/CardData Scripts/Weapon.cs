using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon card", menuName = "Card/New weapon")]
public class Weapon : CardData
{
    public int weaponId;
    public List<CardData> attackList;
}
