using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon card", menuName = "Card/New card")]
public class CardData : ScriptableObject
{
    #region General variables
    /* Variables shared between cards */
    public new string name;
    public string description;
    public Sprite artwork;
    public int cost;
    #endregion
}
