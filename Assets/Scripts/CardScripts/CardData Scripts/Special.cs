using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New spacial card", menuName = "BenditaCompostela/New special card")]
public class Special : CardData
{
    public int damage;
    public TAlteredEffects[] alteredEffects;
    public int[] aEffectValues;
    public TEffects[] effects;
    public int[] eValues;
    public Zone zone;

    public enum Zone { NULL, ANDALUCIA, EXTREMADURA, LEON, GALICIA }
}
