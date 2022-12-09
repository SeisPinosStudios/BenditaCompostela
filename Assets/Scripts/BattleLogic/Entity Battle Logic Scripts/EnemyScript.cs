using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyScript : Entity
{
    public Enemy enemyData;
    public GameObject attack;
    public GameObject trasguPrefab;

    public void Awake()
    {
        SetupEntity();
        if(GameManager.nextEnemy != null) enemyData = GameManager.nextEnemy;

        HP = enemyData.HP;
        energy = enemyData.energy;
        currentHP = HP;
        passives = enemyData.passives;

        PassivesDisplay();

        if (HasPassive(Enemy.Passive.SIERPE)) defense = 2;

        gameObject.GetComponentInChildren<Image>().sprite = enemyData.sprite;
        gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = enemyData.enemyAnimatorController;

        attack.GetComponent<CardDragSystem>().enabled = true;
        attack.GetComponent<CardInspection>().enabled = true;
    }
    public void Start()
    {
        gameObject.GetComponentInChildren<Image>().SetNativeSize();
    }
    public void OnTurnBegin()
    {
        Debug.Log("Turno del enemigo.");
        currentEnergy = energy;
        Poison();
        if(this.currentHP > 0) StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn()
    {
        Card card = ShowCard(enemyData.enemyAttacks[Random.Range(0, enemyData.enemyAttacks.Count - 1)]);
        yield return new WaitForSeconds(1.0f);
        card.UseCard();
        yield return new WaitForSeconds(1.0f);
        Debug.Log("ENEMY TURN");
        FindObjectOfType<TurnSystemScript>().GetComponent<TurnSystemScript>().Turn();
    }
    public void UseCard(Card card)
    {
        StartCoroutine(UseCardCoroutine(card));
    }
    public IEnumerator UseCardCoroutine(Card card)
    {
        yield return new WaitForSeconds(1.0f);
        card.UseCard();
    }
    public Card ShowCard(CardData card)
    {
        attack.GetComponent<CardDisplay>().cardData = card;
        GameObject attackCard = Instantiate(attack, FindObjectOfType<Canvas>().GetComponent<Canvas>().transform);

        attackCard.GetComponent<CardDragSystem>().enabled = false;
        attackCard.GetComponent<CardInspection>().enabled = false;

        attackCard.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

        return attackCard.GetComponent<Card>();
    }
    public void TrasguPassive()
    {
        Debug.Log("TRASGU");
        Instantiate(trasguPrefab, FindObjectOfType<Canvas>().GetComponent<Canvas>().transform);
    }
}
