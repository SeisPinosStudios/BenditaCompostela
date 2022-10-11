using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    #region General variables
    /* Variables shared between cards */
    public new string name;
    public string description;
    public Sprite artwork;
    public int cost;
    public enum TEffect 
        {BLEED, POISON, BURN, VULNERABLE, GUARDED, INVULNERABLE, CONFUSED, DISARMED}
    #endregion
}
