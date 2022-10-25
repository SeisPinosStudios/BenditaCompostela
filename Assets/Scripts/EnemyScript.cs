using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Entity
{
    public Enemy enemyData;
    GameObject attack;

    public void Awake()
    {
        HP = enemyData.HP;
        energy = enemyData.energy;
    }
    public void OnTurnBegin()
    {
        Debug.Log("Turno del enemigo.");
        
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        attack.GetComponent<CardDisplay>().cardData = enemyData.enemyAttacks[Random.Range(0, enemyData.enemyAttacks.Count - 1)];
        GameObject attackCard = Instantiate(attack);
        attackCard.GetComponent<Card>().UseCard();
        yield return new WaitForSeconds(2.0f);
    }
}
