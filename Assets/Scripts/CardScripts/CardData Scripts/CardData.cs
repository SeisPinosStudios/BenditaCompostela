using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    #region General variables
    /* Variables shared between cards */
    public new string name;
    [TextArea(5, 20)]
    public string description;
    public Sprite artwork;
    public int cost;
    public int money;
    public enum TAlteredEffects 
        {BLEED, POISON, BURN, VULNERABLE, GUARDED, INVULNERABLE, CONFUSED, DISARMED}

    public enum TEffects
        {rHEALTH, rENERGY, DRAW, DRAWATTACK, DISCARD, CLEANSE, STEAL}
    #endregion
}
