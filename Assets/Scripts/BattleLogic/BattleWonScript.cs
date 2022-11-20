using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattleWonScript : MonoBehaviour
{
    public Transform pivot;
    public GameObject rewardPrefab;
    public List<Enemy> boss;
    public void Awake()
    {
        var cards = CardDataFilter.ObjectsCardDataList();
        
        for(int i = 0; i < 3; i++)
        {
            rewardPrefab.GetComponent<CardDisplay>().cardData = cards[Random.Range(0, cards.Count)];
            Instantiate(rewardPrefab, pivot);
        }
    }

    public void CleanRewards()
    {
        foreach(GameObject card in GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.GetComponent<CardRewardScript>() != null))
        {
            card.GetComponent<CardRewardScript>().enabled = false;
            card.GetComponent<Button>().enabled = false;
            card.GetComponentInChildren<Image>().color = Color.gray;
        }  
    }

    public void ReturnToRoute()
    {
       // if(!CheckForBoss()) 
    }

    public bool CheckForBoss()
    {
        if (boss.Contains(GameManager.nextEnemy)) return true;
        return false;
    }
}
