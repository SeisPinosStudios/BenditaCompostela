using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.Linq;
using TMPro;

public class BattleWonScript : MonoBehaviour
{
    public Transform pivot;
    public GameObject rewardPrefab;
    public List<Enemy> boss;
    public GameObject rewardText;
    public GameObject rewardMainText;
    Special.Zone zone;
    StringBuilder desc = new StringBuilder();
    public void Awake()
    {
        GetZone();
        Debug.Log("ROUTE: " + GameManager.ActualRoute);

        CardReward(IsBoss());
        if (IsBoss()) BossReward();
        else DefaultEnemy();

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();

        GameManager.SaveGame();
        GameManager.PlayBattleEnd(true);
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
        SceneManager.LoadScene(GameManager.ActualRoute);    
    }
    public bool IsBoss()
    {
        if (boss.Contains(GameManager.nextEnemy)) return true;
        return false;
    }
    
    void GetZone()
    {
        Debug.Log(GameManager.ActualRoute);
        switch (GameManager.ActualRoute)
        {
            case "Sevilla":
                zone = Special.Zone.ANDALUCIA;
                break;
            case "Extremadura":
                zone= Special.Zone.EXTREMADURA;
                break;
            case "Leon":
                zone = Special.Zone.LEON;
                break;
            case "Galicia":
                zone = Special.Zone.GALICIA;
                break;
        }
    }
    void CardReward(bool special)
    {
        var cards = new List<CardData>();

        if(special) foreach (CardData card in CardDataFilter.SpecialZoneCardList(zone)) cards.Add(card);
        else foreach (CardData card in CardDataFilter.ObjectsCardDataListZone(zone)) cards.Add(card);

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(cards.Count);
            var card = cards[Random.Range(0, cards.Count)];
            cards.Remove(card);
            rewardPrefab.GetComponent<CardDisplay>().cardData = card;
            Instantiate(rewardPrefab, pivot);
        }
    }
    void AddReward(int HP, int energy, int coin)
    {
        GameManager.playerData.maxHP += HP;
        GameManager.playerData.currentHP += HP;
        GameManager.playerData.energy += energy;
        GameManager.playerData.CoinIncrease(coin);
    }
    void DefaultEnemy()
    {
        desc.Append("Has conseguido derrotar a tu rival y obtenido las siguientes recompensas: " + GameManager.nextEnemy.reward + " monedas.");
        GameManager.playerData.CoinIncrease(GameManager.nextEnemy.reward);
    }

    #region Boss Rewards
    void BossReward()
    {
        var rewards = new int[] { 0, 0, 0 };
        switch (boss.IndexOf(GameManager.nextEnemy))
        {
            case 0:
                rewards[0] = 10;
                rewards[1] = 3;
                rewards[2] = 20;
                desc.Append("Has conseguido derrotar a la temible sierpe y obtenido las siguientes recompensas:<br>");
                GameManager.ActualRoute = "Extremadura";
                GameManager.NewRoute();
                break;
            case 1:
                rewards[0] = 10;
                rewards[1] = 3;
                rewards[2] = 25;
                desc.Append("¿Realmente deberías estar orgulloso de apalizar a un niño? Sea como fuere, has conseguido las siguientes recompensas:<br>");
                GameManager.ActualRoute = "Leon";
                GameManager.NewRoute();
                break;
            case 2:
                rewards[0] = 10;
                rewards[1] = 3;
                rewards[2] = 35;
                desc.Append("La agilidad del trasgu no ha sido rival para la agilidad de tu muñeca y has acabado haciéndole unos cuantos agujeros más, ¡enhorabuena! Has conseguido las siguientes recompensas:<br>");
                GameManager.ActualRoute = "Galicia";
                GameManager.NewRoute();
                break;
            case 3:
                SceneManager.LoadScene("FinalScene");
                break;
        }

        AddReward(rewards[0], rewards[1], rewards[2]);
        desc.Append("+" + rewards[0] + " vida - +" + rewards[1] + " energía - +" + rewards[2] + " monedas.");
    }
    #endregion
}
