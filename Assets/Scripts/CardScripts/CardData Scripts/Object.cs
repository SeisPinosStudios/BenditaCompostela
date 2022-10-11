using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New object card", menuName = "Card/New object")]
public class Object : CardData
{
    public TEffect[] effect;
    public int[] effectValue;
}
