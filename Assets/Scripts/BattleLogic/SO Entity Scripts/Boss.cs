using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : ScriptableObject
{
    public new string name;
    public Sprite sprite;

    public int HP;
    public int energy;
    public List<CardData> enemyAttacks;
}
