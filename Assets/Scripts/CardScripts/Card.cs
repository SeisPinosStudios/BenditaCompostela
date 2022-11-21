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

        cardData = gameObject.GetComponent<CardDisplay>().cardData;
    }
    public void UseCard()
    {
        var energyCost = cardData.cost;

        #region Trasgu's Passive
        if (enemy.GetComponent<Entity>().IsBoss(Enemy.Boss.TRASGU) && Random.Range(0,9) >= 8)
        {
            Destroy(gameObject);
            return; 
        }
        #endregion

        if (self.Suffering(CardData.TAlteredEffects.CONFUSED)  && cardData.GetType() != typeof(Weapon))
        {
            energyCost++;
            self.ReduceAlteredEffect(CardData.TAlteredEffects.CONFUSED, 1);
        }

        if (!self.ConsumeEnergy(energyCost)) return; /* If the card costs more than the remaining energy, it wont get used */

        self.Bleeding();

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                if (self.Suffering(CardData.TAlteredEffects.DISARMED)) return;
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
        self.GetComponent<PlayerScript>().OnWeaponEquiped(weapon);   
    }
    public void Attack()
    {
        Attack attack = (Attack)cardData;

        enemy.SufferDamage(attack.damage - enemy.defence + self.damageBoost);

        if (attack.alteredEffects.Length != 0)
            for(int i = 0; i < attack.alteredEffects.Length; i++)
                if (attack.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || attack.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyAlteredEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
                else
                    enemy.ApplyAlteredEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
    }
    public void Special()
    {
        Special special = (Special)cardData;
        if (special.damage > 0) enemy.SufferDamage(special.damage - enemy.defence + self.damageBoost);

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
                self.RestoreHealth(value + self.extraHealing);
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
                if (self.GetComponent<PlayerScript>().weapon == null) return;
                StartCoroutine(GameObject.Find("AttackDeck").GetComponent<AttackDeck>().DrawCardCoroutine(value));
                break;
            /*
            case CardData.TEffects.STEAL:
                StartCoroutine(self.GetComponent<EnemyScript>().Steal
                break;*/
            default:
                Debug.Log("Default.");
                break;
        }
    }
}
