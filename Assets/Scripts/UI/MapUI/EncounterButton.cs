using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class EncounterButton : MonoBehaviour
{
    //public int encounterId;
    public DialogueObject dialog;
    public Transform pivot;
    public GameObject encounterPrefab;
    public List<ResponseEvent> events;
    private MapPathSelector mapController;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Cinematic_1") mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());
    }
    public void SetCurrentLevelAndTransition()
    {
        if (SceneManager.GetActiveScene().name != "test")
        {
            GameManager.currentNode = mapController.GetGoIndex(gameObject);
            Debug.Log(mapController.GetGoIndex(gameObject));
            GameObject.Find("Slide").GetComponent<RouteNavigator>().ToEncounter();
        }

        StartCoroutine(Encounter());
    }
    IEnumerator Encounter()
    {
        Debug.Log("ENCOUNTER");
        yield return new WaitForSeconds(1.5f);

        encounterPrefab.GetComponent<DialogueActivator>().dialogueObject = dialog;
        encounterPrefab.GetComponent<DialogueResponseEvents>().dialogueObject = dialog;
        foreach (ResponseEvent responseEvent in events) encounterPrefab.GetComponent<DialogueResponseEvents>().Events.Add(responseEvent);
        encounterPrefab.GetComponentInChildren<DialogueUI>().character = dialog.character;
        var encounter = Instantiate(encounterPrefab, pivot);
        encounter.GetComponentInChildren<DialogTriggerScript>().Interctable = encounter.GetComponent<DialogueActivator>();
        encounter.GetComponent<DialogueActivator>().ActivateDialogue();
    }
    public void Cinematic()
    {
        encounterPrefab.GetComponent<DialogueActivator>().dialogueObject = dialog;
        encounterPrefab.GetComponent<DialogueResponseEvents>().dialogueObject = dialog;
        foreach (ResponseEvent responseEvent in events) encounterPrefab.GetComponent<DialogueResponseEvents>().Events.Add(responseEvent);
        encounterPrefab.GetComponentInChildren<DialogueUI>().character = dialog.character;
        var encounter = Instantiate(encounterPrefab, pivot);
        encounter.transform.localScale = encounter.transform.localScale * 1.2f;
        encounter.GetComponentInChildren<DialogTriggerScript>().Interctable = encounter.GetComponent<DialogueActivator>();
        encounter.GetComponent<DialogueActivator>().ActivateDialogue();
    }
    public void ToBattleScene(Enemy enemy)
    {
        //if (GameManager.playerData.playerDeck.Count < 5) return;
        GameManager.nextEnemy = enemy;
        SceneManager.LoadScene("BattleScene");
    }

    #region Event Units
    public void ToSlideRoute() {
        GameObject.Find("Slide").GetComponent<RouteNavigator>().ToRoute();
    }
    public bool TakeMoney(int money)
    {
        return GameManager.playerData.CoinDecrease(money);
    }
    public void GiveCard(CardData card)
    {
        GameManager.playerData.inventory.Add(card);
    }
    public void GiveRandomCard()
    {
        GameManager.playerData.inventory.Add(CardDataFilter.ObjectsCardDataList()[Random.Range(0, CardDataFilter.ObjectsCardDataList().Count)]);
    }
    public void GiveMoney(int money)
    {
        GameManager.playerData.CoinIncrease(money);
    }
    public bool loseHP(int HP)
    {
        return GameManager.playerData.loseHP(HP);
    }
    public void Heal(int HP)
    {
        GameManager.playerData.Heal(HP);
    }
    public bool TakeArmor()
    {
        var armors = GameManager.playerData.inventory.Where(armor => armor.GetType() == typeof(Armor)).ToList();
        if(armors.Count <= 0) return false;
        GameManager.playerData.inventory.Remove(armors[Random.Range(0,armors.Count)]);
        return true;
    }
    public void TakeRandomCard()
    {
        var playerDeck = GameManager.playerData.playerDeck.Where(card => card.GetType() == typeof(ObjectCard)).ToList();
        GameManager.playerData.inventory.Remove(playerDeck[Random.Range(0, playerDeck.Count)]);
    }
    #endregion

    #region Events
    public void JuglarAceptar(CardData card)
    {
        if (TakeMoney(3)) GiveCard(card);
    }
    public void JuglarRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void AgricultorAceptar()
    {
        if (loseHP(7))
        {
            GiveMoney(10);
        }
    }
    public void AgricultorRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void TerratenienteAceptar()
    {
        GiveMoney(1);
    }
    public void TerratenienteRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void BandidoAceptar()
    {
        GiveMoney(20);
    }
    public void BandidoRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void CambiapielesAceptar()
    {
        GiveMoney(5);
        Heal(5);
    }
    public void CambiapielesRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void HerreroAceptar()
    {
        for(int i = 0; i < 4; i++)
        {
            GiveRandomCard();
        }
    }
    public void HerreroRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void ViejaAceptar(CardData card, CardData card2)
    {
        GiveCard(card);
        GiveCard(card);
        GiveCard(card2);
        GiveCard(card2);
    }
    public void ViejaRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void HerreroGaliciaAceptar(CardData card)
    {
        if(!TakeArmor()) return ;
        Heal(5);
        for(int i = 0; i < 4; i++)
        {
            GiveRandomCard();
        }
        GiveCard(card);
        GiveCard(card);
    }
    public void HerreroGaliciaRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }
    
    public void PastorAceptar(CardData card)
    {
        TakeRandomCard();
        TakeRandomCard();
        GiveMoney(5);
        Heal(5);
        GiveRandomCard();
        GiveCard(card);
    }
    public void PastorRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }

    public void CambiapielesGaliciaAceptar(CardData card)
    {
        TakeRandomCard();
        GiveCard(card);
        GiveCard(card);
        GiveRandomCard();
        GiveRandomCard();
    }
    public void CambiapielesGaliciaRechazar(Enemy enemy)
    {
        ToBattleScene(enemy);
    }
    #endregion
}
