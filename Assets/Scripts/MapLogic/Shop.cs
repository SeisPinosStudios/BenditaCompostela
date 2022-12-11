using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Shop : MonoBehaviour
{
    [SerializeField] private Transform[] weaponPivots;
    [SerializeField] private Transform[] objectPivots;
    [SerializeField] private Transform[] specialObjectPivots;
    [SerializeField] private Transform[] armorPivots;
    [SerializeField] private Transform[] upgradePivots;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject upgradePrefab;

    #region Shop Answers
    public List<string> normalBuy;
    public List<string> specialBuy;
    public List<string> armorBuy;
    public List<string> weaponBuy;
    public List<string> noMoney;
    #endregion

    private CardDatabase cardDataBase;
    private CardDataFilter cardDataFilter;
    public Special.Zone zone;
    void Start()
    {
        cardDataFilter = GetComponent<CardDataFilter>();
;       
        GenerateCards(objectPivots, CardDataFilter.ObjectsCardDataListZone(zone));
        GenerateCards(specialObjectPivots, CardDataFilter.SpecialZoneCardList(zone));
        GenerateCards(weaponPivots, CardDataFilter.ShopWeapons());
        GenerateCards(armorPivots, CardDataFilter.ShopArmor());
        GenerateUpgrade(upgradePivots[0], CardDataFilter.OwnedWeapons());
        GenerateUpgrade(upgradePivots[1], CardDataFilter.OwnedArmors());
    }
    public void GenerateCards(Transform[] pivots, List<CardData> cardDataList)
    {
        var cards = new List<CardData>();
        foreach (CardData card in cardDataList) cards.Add(card);

        for (int i = 0; i < pivots.Length; i++)
        {
            if (pivots[i].transform.childCount > 0) Destroy(pivots[i].transform.GetChild(0));
            var card = cards[Random.Range(0, cards.Count)];
            cards.Remove(card);
            cardPrefab.GetComponent<CardDisplay>().cardData = card;
            Instantiate(cardPrefab, pivots[i]);
        }
    }
    public void GenerateUpgrade(Transform pivot, List<CardData> cards)
    {
        if(cards.Count <= 0) return;
        if (pivot.transform.childCount > 0) Destroy(pivot.transform.GetChild(0));

        CardData card = cards[Random.Range(0, cards.Count)];

        if (card.GetType() == typeof(Armor)) { var armorCard = (Armor)card; upgradePrefab.GetComponent<CardDisplay>().cardData = armorCard; }
        else if(card.GetType() == typeof(Weapon)) { var weaponCard = (Weapon)card; upgradePrefab.GetComponent<CardDisplay>().cardData = weaponCard; } 
        
        Instantiate(upgradePrefab, pivot);
    }
}
