using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDataFilter : MonoBehaviour
{
    public static List<CardData> allCards()
    {
        return Resources.LoadAll<CardData>("Assets").ToList();
    }
    public static List<CardData> WeaponCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Weapons")) obj.Add(card);
        return obj.OrderBy(card => card.name).ToList();
    }        
    public static List<CardData> ArmorCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Armors")) if(card.name != "Mantita de mamá" && card.name != "Botas gastadas") obj.Add(card);
        return obj;
    }
    public static List<CardData> ObjectsCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Objects")) obj.Add(card);
        return obj;
    }
    public static List<CardData> ObjectsCardDataListZone(Special.Zone zone)
    {
        List<CardData> cards = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Objects"))
        {
            Debug.Log(card.name);
            var objectCard = (ObjectCard)card;
            if (objectCard.zone == zone) cards.Add(card);
        }

        return cards;
    }
    public static List<CardData> SpecialCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/SpecialCards")) obj.Add(card);
        return obj;
    }
    public static List<CardData> EnemyCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Enemies")) obj.Add(card);
        return obj;
    }
    public static List<CardData> SpecialZoneCardList(Special.Zone zone)
    {
        List<CardData> cards = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/SpecialCards"))
        {
            var special = (Special)card;
            if(special.zone == zone) cards.Add(card);
        }

        return cards;
    }
    public static List<CardData> ShopWeapons()
    {
        List<CardData> obj = new List<CardData>();

        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Weapons"))
        {
            var weapon = (Weapon)card;
            if (weapon.upgradeLevel == 0) obj.Add(card);
        }
            
        return obj;
    }
    public static List<CardData> ShopArmor()
    {
        List<CardData> obj = new List<CardData>();

        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Armors"))
        {
            var armor = (Armor)card;
            if (armor.upgradeLevel == 0) obj.Add(card);
        }

        return obj;
    }
    public static List<CardData> OwnedWeapons()
    {
        List<CardData> weapons = new List<CardData>();
        foreach (CardData weapon in GameManager.playerData.inventory.Where(card => card.GetType() == typeof(Weapon)))
        {
            var weaponCard = (Weapon)weapon;
            if(weaponCard.upgradeLevel < 2) weapons.Add(weaponCard);
        }
        foreach (CardData weapon in GameManager.playerData.playerDeck.Where(card => card.GetType() == typeof(Weapon)))
        {
            var weaponCard = (Weapon)weapon;
            if (weaponCard.upgradeLevel < 2) weapons.Add(weaponCard);
        }
        return weapons;
    }

    public static List<CardData> OwnedArmors()
    {
        List<CardData> armors = new List<CardData>();
        foreach (CardData armor in GameManager.playerData.inventory.Where(card => card.GetType() == typeof(Armor)))
        {
            var armorCard = (Armor)armor;
            if(armorCard.upgradeLevel < 2)armors.Add(armor);
        }
        return armors;
    }
}
