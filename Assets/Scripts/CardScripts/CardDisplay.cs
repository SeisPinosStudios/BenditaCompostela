using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Text;

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
    #endregion

    void Start()
    {
        nameText.text = cardData.name;
        descText.text = cardData.description;
        Description();
        sprite.sprite = cardData.artwork;
        cost.text = cardData.cost.ToString();
        if(cardData.GetType() == typeof(Armor) || cardData.GetType() == typeof(Weapon)) energyIcon.SetActive(false);
    }
    public void Description()
    {
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
                break;
            case "Weapon":
                break;
        }
    }
    public void DamageText()
    {
        if (cardData.GetType() == typeof(Attack))
        {
            var card = (Attack)cardData;
            if(card.damage > 0) description.Append("Daña " + card.damage.ToString() + "<br>");
        }
        else if (cardData.GetType() == typeof(Special)){
            var card = (Special)cardData;
            if (card.damage > 0) description.Append("Daña " + card.damage.ToString() + "<br>");
        }
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
                        if (card.aEffectValues[i] == -1) description.Append("Recarga toda tu energía.<br>");
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
                        if (card.eValues[i] == -1) description.Append("Recarga toda tu energía.<br>");
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
        description.Append("Sinergia con " + armor.synergyWeapon[0].name + ":");

        switch (armor.synergy)
        {
            case Armor.TSynergy.xVULNERABLE:
                description.Append("Haces " + (10 + armor.upgradeLevel * 5) + "% más de daño a <sprite index=5>");
                break;
            case Armor.TSynergy.DEFENCE:

                break;
            case Armor.TSynergy.HEALING:

                break;
            case Armor.TSynergy.ENERGY:

                break;
            case Armor.TSynergy.DAMAGE:

                break;
            case Armor.TSynergy.xGUARDED:

                break;
        }
    }
}
