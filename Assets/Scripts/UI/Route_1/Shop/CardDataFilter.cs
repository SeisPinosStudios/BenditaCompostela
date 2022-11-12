using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDataFilter : MonoBehaviour
{
    public List<CardData> WeaponCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Weapons")) obj.Add(card);
        return obj;
    }        
    /*public List<CardData> ArmorCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Weapons")) obj.Add(card);
        return obj;
    }*/
    public List<CardData> ObjectsCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Objects")) obj.Add(card);
        return obj;
    }
    public List<CardData> SpecialCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/SpecialCards")) obj.Add(card);
        return obj;
    }
    public List<CardData> EnemyCardDataList()
    {
        List<CardData> obj = new List<CardData>();
        foreach (CardData card in Resources.LoadAll<CardData>("Assets/Enemies")) obj.Add(card);
        return obj;
    }
}
