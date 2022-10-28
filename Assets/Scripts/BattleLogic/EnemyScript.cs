using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Entity
{
    public Enemy enemyData;
    public GameObject attack;

    public void Awake()
    {
        //enemyData = GameManager.nextEnemy;

        HP = enemyData.HP;
        energy = enemyData.energy;
        currentHP = HP;

        attack.GetComponent<CardDragSystem>().enabled = true;
        attack.GetComponent<CardInspection>().enabled = true;
    }

    public void OnTurnBegin()
    {
        Debug.Log("Turno del enemigo.");
        currentEnergy = energy;
        this.Poison();
        if(this.currentHP > 0) StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        attack.GetComponent<CardDisplay>().cardData = enemyData.enemyAttacks[Random.Range(0, enemyData.enemyAttacks.Count - 1)];
        GameObject attackCard = Instantiate(attack, FindObjectOfType<Canvas>().GetComponent<Canvas>().transform);

        /*  Disables the interaction with the card used by the enemy creature   */
        attackCard.GetComponent<CardDragSystem>().enabled = false;
        attackCard.GetComponent<CardInspection>().enabled = false;
        /*                                                                      */


        attackCard.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        yield return new WaitForSeconds(1.0f);
        attackCard.GetComponent<Card>().UseCard();
        yield return new WaitForSeconds(2.0f);
        this.Burn();
        FindObjectOfType<TurnSystemScript>().GetComponent<TurnSystemScript>().Turn();
    }
}
