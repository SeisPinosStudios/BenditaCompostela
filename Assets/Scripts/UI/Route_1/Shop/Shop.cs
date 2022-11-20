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
        GenerateCards(objectPivots, CardDataFilter.ObjectsCardDataList());
        GenerateCards(specialObjectPivots, CardDataFilter.SpecialZoneCardList(zone));
        GenerateCards(weaponPivots, CardDataFilter.ShopWeapons());
        GenerateCards(armorPivots, CardDataFilter.ShopArmor());
        GenerateUpgrade(upgradePivots[0], CardDataFilter.OwnedWeapons());
        GenerateUpgrade(upgradePivots[1], CardDataFilter.OwnedArmors());
    }
    public void GenerateCards(Transform[] pivots, List<CardData> cardDataList)
    {        
        for (int i = 0; i < pivots.Length; i++)
        {
            cardPrefab.GetComponent<CardDisplay>().cardData = cardDataList[Random.Range(0, cardDataList.Count)];
            Instantiate(cardPrefab, pivots[i]);
        }
    }
    public void GenerateUpgrade(Transform pivot, List<CardData> card)
    {
        if(card.Count <= 0) return;
        upgradePrefab.GetComponent<CardDisplay>().cardData = card[Random.Range(0, card.Count)];
        Instantiate(upgradePrefab, pivot);
    }
}
