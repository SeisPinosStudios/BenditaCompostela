using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class CardDisplay : MonoBehaviour
{
    #region Card information variables
    /* Printable information of the card. */
    public CardData cardData;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public Image sprite;
    public TextMeshProUGUI cost;
    public StringBuilder description = new StringBuilder();
    public GameObject energyIcon;
    Entity user;
    Entity enemy;
    #endregion

    void Start()
    {
        if (InBattle())
        {
            user = GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().current.GetComponent<Entity>();
            enemy = GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().current.GetComponent<Entity>();
        }
        nameText.text = cardData.name;
        descText.text = cardData.description;
        Description();
        sprite.sprite = cardData.artwork;
        cost.text = cardData.cost.ToString();
        if(cardData.GetType() == typeof(Armor) || cardData.GetType() == typeof(Weapon)) energyIcon.SetActive(false);
    }
    public void Description()
    {
        description.Clear();
        switch (cardData.GetType().ToString())
        {
            case "Attack":
                DamageText();
                AlteredEffect();
                descText.text = description.ToString();
                break;
            case "Special":
                DamageText();
                AlteredEffect();
                Effect();
                descText.text = description.ToString();
                break;
            case "ObjectCard":
                AlteredEffect();
                Effect();
                descText.text = description.ToString();
                break;
            case "Armor":
                ArmorDescription();
                descText.text = description.ToString();
                break;
            case "Weapon":
                break;
        }
    }
    public void DamageText()
    {
        var vulnerable = 0;
        var originalDamage = 0;
        var finalDamage = 0;
        if(SceneManager.GetActiveScene().name == "BattleScene")
        {
            var enemy = GameObject.Find("TurnSystem").GetComponent<TurnSystemScript>().next.GetComponent<Entity>();
            if (enemy.Suffering(CardData.TAlteredEffects.VULNERABLE)) vulnerable = enemy.alteredEffects[CardData.TAlteredEffects.VULNERABLE];
        }

        if (cardData.GetType() == typeof(Attack))
        {
            var card = (Attack)cardData;
            finalDamage += GetVulnerable(card.damage, vulnerable);
            originalDamage = card.damage;
        }
        else if (cardData.GetType() == typeof(Special)){
            var card = (Special)cardData;
            finalDamage += GetVulnerable(card.damage, vulnerable);
            originalDamage = card.damage;
        }

        if (InBattle()) finalDamage += user.damageBoost - enemy.defense;

        if (originalDamage <= 0) return;

        if (finalDamage == originalDamage) description.Append("Da�a " + finalDamage.ToString() + "<br>");
        else if (finalDamage > originalDamage) description.Append("Da�a <color=green>" + finalDamage.ToString() + "</color><br>");
        else description.Append("Da�a <color=green>" + finalDamage.ToString() + "</color><br>");
    }
    public void AlteredEffect()
    {

        if (cardData.GetType() == typeof(Attack))
        {
            var card = (Attack)cardData;

            if (card.alteredEffects.Length <= 0) return;

            description.Append("Aplica ");
            for (int i = 0; i < card.alteredEffects.Length; i++)
                description.Append("<sprite index=" + (int)card.alteredEffects[i] + ">x" + card.aEffectValues[i] + " ");
        }
        else if(cardData.GetType() == typeof(Special))
        {
            var card = (Special)cardData;

            if (card.alteredEffects.Length <= 0) return;

            description.Append("Aplica ");
            for (int i = 0; i < card.alteredEffects.Length; i++)
                description.Append("<sprite index=" + (int)card.alteredEffects[i] + ">x" + card.aEffectValues[i] + " ");
        }
        else if(cardData.GetType() == typeof(ObjectCard))
        {
            var card = (ObjectCard)cardData;

            if (card.alteredEffects.Length <= 0) return;

            description.Append("Aplica ");
            for (int i = 0; i < card.alteredEffects.Length; i++)
                description.Append("<sprite index=" + (int)card.alteredEffects[i] + ">x" + card.aEffectValues[i] + " ");
        }
    }
    public void Effect()
    {
        if(cardData.GetType() == typeof(Special))
        {
            var card = (Special)cardData;

            for (int i = 0; i < card.effects.Length; i++)
            {
                switch (card.effects[i])
                {
                    case CardData.TEffects.rHEALTH:
                        description.Append("Cura " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.rENERGY:
                        if (card.aEffectValues[i] == -1) description.Append("Recarga toda tu energ�a.<br>");
                        else description.Append("Recarga " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.DRAW:
                        description.Append("Robas " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.DRAWATTACK:
                        description.Append("Robas " + card.eValues[i] + " ataques<br>");
                        break;
                    case CardData.TEffects.DISCARD:
                        description.Append("Descarta " + card.eValues[i] + " cartas.<br>");
                        break;
                    case CardData.TEffects.CLEANSE:
                        description.Append("Limpia todos los estados<br>");
                        break;
                    case CardData.TEffects.STEAL:
                        description.Append("Robas una carta a tu rival y la usas este turno.<br>");
                        break;
                }
            }
        }
        else if (cardData.GetType() == typeof(ObjectCard))
        {
            var card = (ObjectCard)cardData;

            for (int i = 0; i < card.effects.Length; i++)
            {
                switch (card.effects[i])
                {
                    case CardData.TEffects.rHEALTH:
                        description.Append("Cura " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.rENERGY:
                        if (card.eValues[i] == -1) description.Append("Recarga toda tu energ�a.<br>");
                        else description.Append("Recarga " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.DRAW:
                        description.Append("Robas " + card.eValues[i] + "<br>");
                        break;
                    case CardData.TEffects.DRAWATTACK:
                        description.Append("Robas " + card.eValues[i] + " ataques<br>");
                        break;
                    case CardData.TEffects.DISCARD:
                        description.Append("Descarta " + card.eValues[i] + " cartas.");
                        break;
                    case CardData.TEffects.CLEANSE:
                        description.Append("Limpia todos los estados<br>");
                        break;
                    case CardData.TEffects.STEAL:
                        description.Append("Robas una carta a tu rival y la usas este turno");
                        break;
                }
            }
        }



    }
    public void ArmorDescription()
    {
        var armor = (Armor)cardData;
        description.Append("Protege " + armor.defenseValue + "<br>");
        if(armor.synergyWeapon[0] != null) description.Append("Sinergia con " + armor.synergyWeapon[0].name + ":");

        switch (armor.synergy)
        {
            case Armor.TSynergy.xVULNERABLE:
                description.Append("Haces " + (10 + armor.upgradeLevel * 5) + "% m�s de da�o a <sprite index=5>");
                break;
            case Armor.TSynergy.DEFENCE:
                description.Append("Protege " + armor.extraDefence + " extra.");
                break;
            case Armor.TSynergy.HEALING:
                description.Append("Curas extra");
                break;
            case Armor.TSynergy.ENERGY:
                description.Append("Tienes m�s energ�a");
                break;
            case Armor.TSynergy.DAMAGE:
                description.Append("Da�as " + armor.damageBonus + " extra con ataques.");
                break;
            case Armor.TSynergy.xGUARDED:
                description.Append("<sprite index=5> bloquea " + (10 + armor.upgradeLevel * 5) + " extra.");
                break;
        }
    }
    public int GetVulnerable(int damage, int charges)
    {
        if (charges == 0) return damage;
        return damage += Mathf.Clamp((int)Mathf.Round((float)damage * (0.1f + (0.2f * (charges - 1)))), 1, 99);
    }
    public bool InBattle()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene") return true;
        return false;

    }
    public void Update()
    {
        Description();
    }
}
