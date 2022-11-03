using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    Entity enemy;
    Entity self;
    CardData cardData;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene") {
            self = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
            enemy = FindObjectOfType<TurnSystemScript>().next.GetComponent<Entity>();
        }
    }
    public void UseCard()
    {
        cardData = gameObject.GetComponent<CardDisplay>().cardData;
        Debug.Log("Carta jugada.");

        if (!self.ConsumeEnergy(cardData.cost)) return; /* If the card costs more than the remaining energy, it wont get used */
        Debug.Log(cardData.cost);
        //self.ConsumeEnergy(cardData.cost);

        self.Bleeding();

        switch (cardData.GetType().ToString())
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
            case "ObjectCard":
                Object();
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
    public void EquipWeapon()
    {
        Weapon weapon = (Weapon)cardData;

        foreach(Transform card in GameObject.Find("HandPanel").transform)
        {
            if (card.GetComponent<CardDisplay>().cardData.GetType().ToString() == "Attack") Destroy(card.gameObject);
        }

        GameObject.Find("Player").GetComponent<PlayerScript>().weapon = weapon;
    }
    public void Attack()
    {
        Attack attack = (Attack)cardData;

        enemy.SufferDamage(attack.damage);

        if (attack.alteredEffects.Length != 0)
            for(int i = 0; i < attack.alteredEffects.Length; i++)
                enemy.ApplyAlteredEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
    }
    public void Special()
    {
        Special special = (Special)cardData;
        if (special.damage > 0) enemy.SufferDamage(special.damage);

        if (special.alteredEffects.Length != 0)
            for (int i = 0; i < special.alteredEffects.Length; i++)
            {
                if (special.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || special.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyAlteredEffect(special.alteredEffects[i], special.aEffectValues[i]);
                else
                    enemy.ApplyAlteredEffect(special.alteredEffects[i], special.aEffectValues[i]);
            }
 
        //Pending modification
        if (special.effects.Length != 0)
            for (int i = 0; i < special.effects.Length; i++)
            {
                ComputeEffect(special.effects[i], special.eValues[i]);
            }
    }
    public void Object()
    {
        ObjectCard objectCard = (ObjectCard)cardData;

        if(objectCard.alteredEffects.Length != 0)
            for(int i = 0; i < objectCard.alteredEffects.Length; i++) 
            {
                if (objectCard.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || objectCard.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyAlteredEffect(objectCard.alteredEffects[i], objectCard.aEffectValues[i]);
                else
                    enemy.ApplyAlteredEffect(objectCard.alteredEffects[i], objectCard.aEffectValues[i]);
            }

        if(objectCard.effects.Length != 0)
            for(int i = 0; i < objectCard.effects.Length; i++)
            {
                ComputeEffect(objectCard.effects[i], objectCard.eValues[i]);
            }
    }
    public void ComputeEffect(CardData.TEffects effect, int value)
    {
        switch (effect)
        {
            case CardData.TEffects.rHEALTH:
                self.RestoreHealth(value);
                break;
            case CardData.TEffects.rENERGY:
                self.RestoreEnergy(value);
                break;
            case CardData.TEffects.DRAW:
                StartCoroutine(GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().DrawCardCorroutine(value));
                break;
            case CardData.TEffects.CLEANSE:
                self.RemoveAlteredEffect();
                break;
            case CardData.TEffects.DRAWATTACK:
                StartCoroutine(GameObject.Find("AttackDeck").GetComponent<AttackDeck>().DrawCardCorroutine(value));
                break;
            default:
                Debug.Log("Default.");
                break;
        }
    }
}
