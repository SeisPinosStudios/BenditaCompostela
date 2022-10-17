using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack card", menuName = "BenditaCompostela/New attack")]
public class Attack : CardData
{
    public int damage;
    public TAlteredEffects[] alteredEffects;
    public int[] aEffectValues;
}
