using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Shop : MonoBehaviour
{
    [SerializeField] private Transform[] weaponPivots;
    [SerializeField] private Transform[] objectPivots;
    [SerializeField] private Transform[] specialObjectPivots;
    [SerializeField] private GameObject cardPrefab;


    private CardDatabase cardDataBase;
    private CardDataFilter cardDataFilter;
    [SerializeField] private float scaleFactor;
    void Start()
    {
        cardDataFilter = GetComponent<CardDataFilter>();

        scaleFactor = .75f;       
        GenerateCards(objectPivots, cardDataFilter.ObjectsCardDataList(), scaleFactor);
        GenerateCards(specialObjectPivots, cardDataFilter.SpecialCardDataList(), scaleFactor);
        GenerateCards(weaponPivots, cardDataFilter.WeaponCardDataList(), scaleFactor);
    }

    public void GenerateCards(Transform[] pivots, List<CardData> cardDataList, float scaleFactor)
    {        
        for (int i = 0; i < pivots.Length; i++)
        {
            GameObject go = Instantiate(cardPrefab, pivots[i]);
            CardDisplay cardDisplay = go.GetComponent<CardDisplay>();
            go.GetComponent<CardInspection>().scaleMultiplier = 1.5f;
            go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            cardDisplay.cardData = cardDataList[Random.Range(1,cardDataList.Count)];

        }
    }

    
}
