using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Entity enemy;
    Entity self;

    public void Awake()
    {
        self = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
        enemy = FindObjectOfType<TurnSystemScript>().next.GetComponent<Entity>();
    }
    public void UseCard()
    {
        Debug.Log("Se ha usado la carta: " + gameObject.GetComponent<CardDisplay>().name);

        self.ConsumeEnergy(gameObject.GetComponent<CardDisplay>().cardData.cost);

        switch (gameObject.GetComponent<CardDisplay>().cardData.GetType().ToString())
        {
            case "Weapon":
                EquipWeapon();
                break;
            case "Attack":
                Attack();
                break;
            case "Special":
                Special();
                break;
            case "Object":
                Object();
                break;
            default:
                break;
        }
    }

    public void EquipWeapon()
    {
        Weapon weapon = (Weapon)gameObject.GetComponent<CardDisplay>().cardData;
        GameObject.FindObjectOfType<PlayerScript>().weapon = weapon;
    }

    public void Attack()
    {
        Attack attack = (Attack)gameObject.GetComponent<CardDisplay>().cardData;

        enemy.SufferDamage(attack.damage);

        if (attack.alteredEffects.Length != 0)
            for(int i = 0; i < attack.alteredEffects.Length; i++)
                enemy.ApplyEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
    }

    public void Special()
    {
        Special special = (Special)gameObject.GetComponent<CardDisplay>().cardData;

        if (special.alteredEffects.Length != 0)
            for (int i = 0; i < special.alteredEffects.Length; i++)
            {
                if (special.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || special.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyEffect(special.alteredEffects[i], special.aEffectValues[i]);
                else
                    enemy.ApplyEffect(special.alteredEffects[i], special.aEffectValues[i]);
            }

        if (special.damage > 0) enemy.SufferDamage(special.damage);

        //Pending modification
        if (special.effects.Length != 0)
            for (int i = 0; i < special.effects.Length; i++)
            {
                switch (special.effects[i])
                {
                    case CardData.TEffects.rHEALTH:
                        self.RestoreHealth(special.eValues[i]);
                        break;
                    case CardData.TEffects.rENERGY:
                        self.RestoreEnergy(special.eValues[i]);
                        break;
                    case CardData.TEffects.DRAW:
                        StartCoroutine(DrawCardCorroutine(i, special.eValues[i]));
                        break;
                    case CardData.TEffects.CLEANSE:
                        self.RemoveEffect();
                        break;
                }
            }
    }

    public void Object()
    {
        ObjectCard objectCard = (ObjectCard)gameObject.GetComponent<CardDisplay>().cardData;

        if(objectCard.alteredEffects.Length != 0)
            for(int i = 0; i < objectCard.alteredEffects.Length; i++) 
            {
                if (objectCard.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || objectCard.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyEffect(objectCard.alteredEffects[i], objectCard.aEffectValues[i]);
                else
                    enemy.ApplyEffect(objectCard.alteredEffects[i], objectCard.aEffectValues[i]);
            }
                

        if(objectCard.effects.Length != 0)
            for(int i = 0; i < objectCard.effects.Length; i++)
            {
                switch (objectCard.effects[i])
                {
                    case CardData.TEffects.rHEALTH:
                        enemy = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
                        enemy.RestoreHealth(objectCard.eValues[i]);
                        break;
                    case CardData.TEffects.rENERGY:
                        enemy = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
                        enemy.RestoreEnergy(objectCard.eValues[i]);
                        break;
                    case CardData.TEffects.DRAW:
                        StartCoroutine(DrawCardCorroutine(i, objectCard.eValues[i]));
                        break;
                    case CardData.TEffects.CLEANSE:
                        enemy = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
                        enemy.RemoveEffect();
                        break;
                }
            }
    }

    IEnumerator DrawCardCorroutine(int i, int drawnCards)
    {
        for (int j = 0; i < drawnCards; j++) 
        { 
            FindObjectOfType<DefaultDeck>().DrawCard();
            yield return new WaitForSeconds(1);
        }     
    }
}
