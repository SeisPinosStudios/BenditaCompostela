using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New object card", menuName = "BenditaCompostela/New object")]
public class ObjectCard : CardData
{
    public TAlteredEffects[] alteredEffects;
    public int[] aEffectValues;
    public TEffects[] effects;
    public List<int> eValues;
    public Special.Zone zone;
}
