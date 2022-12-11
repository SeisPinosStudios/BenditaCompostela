using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New enemy asset", menuName = "BenditaCompostela/New enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public RuntimeAnimatorController enemyAnimatorController;
    public int HP;
    public int energy;
    public List<CardData> enemyAttacks;
    public enum Passive { SIERPE, HERNAN, TRASGU, SANTIAGO, SANTIAGO_2, rVULNERABLE, rGUARDED, rPOISON, rBURN, rBLEED }
    public List<Passive> passives;

    public enum Boss
    {
        NOT_BOSS, SIERPE, HERNAN, TRASGU, SANTIAGO
    }

    public int reward;
}
