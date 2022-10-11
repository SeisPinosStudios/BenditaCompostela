using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New spacial card", menuName = "Card/New special card")]
public class Special : CardData
{
    public TEffect[] effect;
    public int[] effectValue;
}
