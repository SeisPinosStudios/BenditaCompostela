using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon card", menuName = "Card/New card")]
public class CardData : ScriptableObject
{
    #region Information variables
    /* Information variables, do not affect
     * functionality of the card, only used for 
     * visualpurposes */
    public new string name;
    public string description;
    public Sprite artwork;
    #endregion

    #region Gameplay variables
    /* Variables that affect  the card's functionalities
     * following the guide inside the Resources/Cards folder */

    public int type;

    public int effect;
    public int effectValue;

    public int cost;
    
    public int weaponId;
    public int attackDamage;
    #endregion
}
