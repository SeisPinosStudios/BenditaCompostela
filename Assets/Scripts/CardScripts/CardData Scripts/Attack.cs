using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack card", menuName = "Card/New attack")]
public class Attack : CardData
{
    public int[] effect;
    public int[] effectValue;
    public int damage;
}
