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

    public Boss boss;
    public List<CardData.TAlteredEffects> resistances;

    public enum Boss
    {
        NOT_BOSS, SIERPE, HERNAN, TRASGU, SANTIAGO
    }

    public bool IsBoss(Boss isBoss)
    {
        if (boss == isBoss) return true;

        return false;
    }

    public int reward;
}
