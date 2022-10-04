using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon card", menuName = "Card/New card")]
public class CardData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public int effect = 0;
    public int cost;
    public int value;
    public int weapon_id;
}
