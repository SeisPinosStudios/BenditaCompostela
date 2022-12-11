using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
    public enum Conditions { HAS_HP, HAS_COIN, HAS_ARMOR, HAS_WEAPONS, HAS_OBJECT}
    public List<Conditions> conditions;
    public List<int> conditionsValue;

    public bool CheckConditions()
    {
        for(int i = 0; i < conditions.Count; i++)
        {
            switch (conditions[i])
            {
                case Conditions.HAS_HP:
                    if (GameManager.playerData.currentHP < (conditionsValue[i]+1)) return false;
                    break;
                case Conditions.HAS_COIN:
                    if (GameManager.playerData.coins < conditionsValue[i]) return false;
                    break;
                case Conditions.HAS_ARMOR:
                    if (GameManager.playerData.inventory.Where((card) => card.GetType() == typeof(Armor) && card != GameManager.playerData.chestArmor && card != GameManager.playerData.feetArmor).ToList().Count < conditionsValue[i]) return false;
                    break;
                case Conditions.HAS_WEAPONS:
                    if (GameManager.playerData.inventory.Where((card) => card.GetType() == typeof(Weapon)).ToList().Count < (conditionsValue[i] + 1)) return false;
                    break;
                case Conditions.HAS_OBJECT:
                    if (GameManager.playerData.inventory.Where((card) => card.GetType() == typeof(ObjectCard)).ToList().Count < conditionsValue[i]) return false;
                    break;
            }
        }
        if (conditions.Count == 0) return true;
        return true;
    }
}
