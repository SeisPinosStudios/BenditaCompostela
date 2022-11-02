using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New enemy asset", menuName = "BenditaCompostela/New enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public Sprite sprite;

    public int HP;
    public int energy;
    public List<CardData> enemyAttacks;
}
