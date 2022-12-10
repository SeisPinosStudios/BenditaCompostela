using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    Entity enemy;
    AudioManager audioManager;
    public Entity self;
    public CardData cardData;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene") {
            self = FindObjectOfType<TurnSystemScript>().current.GetComponent<Entity>();
            enemy = FindObjectOfType<TurnSystemScript>().next.GetComponent<Entity>();
            
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cardData = gameObject.GetComponent<CardDisplay>().cardData;
    }
    public void UseCard()
    {
        var energyCost = cardData.cost;

        #region Trasgu's Passive
        if (enemy.HasPassive(Enemy.Passive.TRASGU) && Random.Range(0,9) >= 8)
        {
            enemy.GetComponent<EnemyScript>().TrasguPassive();
            Destroy(gameObject);
            return; 
        }
        #endregion

        Debug.Log("USED CARD: " + cardData.name);

        if (self.Suffering(CardData.TAlteredEffects.CONFUSED)  && cardData.GetType() != typeof(Weapon))
        {
            energyCost++;
            self.ReduceAlteredEffect(CardData.TAlteredEffects.CONFUSED, 1);
        }

        if (!self.ConsumeEnergy(energyCost)) return; /* If the card costs more than the remaining energy, it wont get used */

        self.Bleeding();
        if(self.HasPassive(Enemy.Passive.HERNAN)) enemy.ApplyAlteredEffect(CardData.TAlteredEffects.BLEED, 1);

        switch (cardData.GetType().ToString())
        {
            case "Weapon":
                if (self.Suffering(CardData.TAlteredEffects.DISARMED)) return;
                EquipWeapon();
                break;
            case "Attack":
                self.animationTrigger.Attack();
                Attack();
                break;
            case "Special":
                Debug.Log("SELF: " + self.name);
                self.animationTrigger.Attack();
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
        audioManager.PlaySound("Sword");
    }
    public void Attack()
    {
        Attack attack = (Attack)cardData;

        if (attack.damage > 0) { enemy.SufferDamage(attack.damage - enemy.defense + self.damageBoost); audioManager.PlaySound("SwordHit"); }

        if (attack.alteredEffects.Length > 0)
            for(int i = 0; i < attack.alteredEffects.Length; i++)
                if (attack.alteredEffects[i] == CardData.TAlteredEffects.INVULNERABLE || attack.alteredEffects[i] == CardData.TAlteredEffects.GUARDED)
                    self.ApplyAlteredEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
                else
                    enemy.ApplyAlteredEffect(attack.alteredEffects[i], attack.aEffectValues[i]);
    }
    public void Special()
    {
        Special special = (Special)cardData;
        if (special.damage > 0) enemy.SufferDamage(special.damage - enemy.defense + self.damageBoost);

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

        GameManager.playerData.playerDeck.Remove(special);
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
                GameObject.Find("DefaultDeck").GetComponent<DefaultDeck>().StartDrawCoroutine(value);
                break;
            case CardData.TEffects.CLEANSE:
                self.RemoveAlteredEffect();
                break;
            case CardData.TEffects.DRAWATTACK:
                if (self.GetComponent<PlayerScript>().weapon == null) return;
                GameObject.Find("AttackDeck").GetComponent<AttackDeck>().StartDrawCoroutine(value);
                break;
            case CardData.TEffects.STEAL:
                var card = enemy.gameObject.GetComponent<PlayerScript>().GetStolableCard();
                self.gameObject.GetComponent<EnemyScript>().UseCard(self.gameObject.GetComponent<EnemyScript>().ShowCard(card));
                break;
            default:
                Debug.Log("Default.");
                break;
        }
    }
    void Sound()
    {
        
    }
}
