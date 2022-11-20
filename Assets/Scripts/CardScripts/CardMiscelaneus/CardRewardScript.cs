using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardRewardScript : MonoBehaviour, IPointerClickHandler
{
    GameObject cardDatabaseObject;
    public void Awake()
    {
        
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.playerData.inventory.Add(GetComponent<CardDisplay>().cardData);
        GameObject.Find("BattleWonUI(Clone)").GetComponent<BattleWonScript>().CleanRewards();
    }
}
