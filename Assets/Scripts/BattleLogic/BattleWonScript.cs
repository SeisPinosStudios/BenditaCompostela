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
    public void Awake()
    {   
        var cards = CardDataFilter.ObjectsCardDataList();
        
        for(int i = 0; i < 3; i++)
        {
            rewardPrefab.GetComponent<CardDisplay>().cardData = cards[Random.Range(0, cards.Count)];
            Instantiate(rewardPrefab, pivot);
        }

        if (GameManager.nextEnemy == boss[0]) Sierpe();

        if (GameManager.nextEnemy == boss[1]) Hernan();

        if (GameManager.nextEnemy == boss[2]) Trasgu();

        if (GameManager.nextEnemy == boss[3]) Santiago();

        if (GameManager.nextEnemy.name == "Bandido A") BandidoTutorial();

        if (!boss.Contains(GameManager.nextEnemy) && GameManager.nextEnemy.name != "Bandido A") DefaultEnemy();

        GameManager.SaveGame();
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
    public bool CheckForBoss()
    {
        if (boss.Contains(GameManager.nextEnemy)) return true;
        return false;
    }
    void DefaultEnemy()
    {
        StringBuilder desc = new StringBuilder();
        desc.Append("Has conseguido derrotar a tu rival y obtenido las siguientes recompensas: " + GameManager.nextEnemy.reward + " monedas.");
        GameManager.playerData.CoinIncrease(GameManager.nextEnemy.reward);

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();
    }
    void Sierpe()
    {
        GameManager.ActualRoute = "Extremadura";
        GameManager.NewRoute();

        StringBuilder desc = new StringBuilder();
        desc.Append("Has conseguido derrotar a la temible sierpe y obtenido las siguientes recompensas:<br>+5 vida - +3 energía - +35 monedas -");
        GameManager.playerData.maxHP += 5;
        GameManager.playerData.currentHP += 5;
        GameManager.playerData.energy += 2;
        GameManager.playerData.CoinIncrease(35);

        var card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA).Count)];
        desc.Append(card.name+" - ");
        GameManager.playerData.inventory.Add(card);

        card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();
    }
    void Hernan()
    {
        GameManager.ActualRoute = "Leon";
        GameManager.NewRoute();

        StringBuilder desc = new StringBuilder();
        desc.Append("¿Realmente deberías estar orgulloso de apalizar a un niño? Sea como fuere, has conseguido las siguientes recompensas:<br>+5 vida - +3 energía - +50 monedas - ");
        GameManager.playerData.maxHP += 5;
        GameManager.playerData.currentHP += 5;
        GameManager.playerData.energy += 2;
        GameManager.playerData.CoinIncrease(50);

        var card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.EXTREMADURA).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.EXTREMADURA).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();
    }
    void Trasgu()
    {
        GameManager.ActualRoute = "Galicia";
        GameManager.NewRoute();

        StringBuilder desc = new StringBuilder();
        desc.Append("La agilidad del trasgu no ha sido rival para la agilidad de tu muñeca y has acabado haciéndole unos cuantos agujeros más, ¡enhorabuena! Has conseguido las siguientes recompensas:<br>+5 vida - +3 energía - +65 monedas - ");
        GameManager.playerData.maxHP += 5;
        GameManager.playerData.currentHP += 5;
        GameManager.playerData.energy += 2;
        GameManager.playerData.CoinIncrease(65);

        var card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.LEON).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.LEON).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        card = CardDataFilter.SpecialZoneCardList(Special.Zone.ANDALUCIA)[Random.Range(0, CardDataFilter.SpecialZoneCardList(Special.Zone.LEON).Count)];
        desc.Append(card.name + " - ");
        GameManager.playerData.inventory.Add(card);

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();
    }
    void Santiago()
    {
        SceneManager.LoadScene("FinalScene");
    }
    void BandidoTutorial()
    {
        StringBuilder desc = new StringBuilder();
        desc.Append("Has conseguido derrotar a tu rival y obtenido las siguientes recompensas: " + GameManager.nextEnemy.reward + " monedas.");
        var card = CardDataFilter.WeaponCardDataList().Find(weapon => weapon.name == "Daga");
        desc.Append(card.name);
        GameManager.playerData.inventory.Add(card);

        rewardText.GetComponent<TextMeshProUGUI>().text = desc.ToString();
    }
}
