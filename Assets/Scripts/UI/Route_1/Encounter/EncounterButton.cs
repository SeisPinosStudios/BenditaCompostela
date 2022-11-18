using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EncounterButton : MonoBehaviour
{

    public Enemy enemy;
    public int encounterId;
    public DialogueObject dialog;
    public Transform pivot;
    public GameObject encounterPrefab;
    public List<ResponseEvent> events;
    private MapPathSelector mapController;

    private void OnEnable()
    {
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => SetCurrentLevelAndTransition());        
    }

    public void SetCurrentLevelAndTransition()
    {
        if (SceneManager.GetActiveScene().name != "test")
        {
            GameManager.currentNode = mapController.GetGoIndex(gameObject);
            GameObject.Find("Slide").GetComponent<Ruta_1>().ToEncounter(encounterId);
        }

        StartCoroutine(Encounter());
    }

    IEnumerator Encounter()
    {
        yield return new WaitForSeconds(1.5f);

        encounterPrefab.GetComponent<DialogueActivator>().dialogueObject = dialog;
        encounterPrefab.GetComponent<DialogueResponseEvents>().dialogueObject = dialog;
        encounterPrefab.GetComponent<BattleButton>().enemy = enemy;
        foreach (ResponseEvent responseEvent in events) encounterPrefab.GetComponent<DialogueResponseEvents>().Events.Add(responseEvent);
        var encounter = Instantiate(encounterPrefab, pivot);
        foreach (ResponseEvent responseEvent in events) encounter.GetComponent<DialogueResponseEvents>().Events.Add(responseEvent);
        encounter.GetComponentInChildren<DialogTriggerScript>().Interctable = encounter.GetComponent<DialogueActivator>();
        encounter.GetComponent<DialogueActivator>().ActivateDialogue();
    }
}
