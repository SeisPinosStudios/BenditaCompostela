using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    public Enemy enemy;
    private MapPathSelector mapController;
    public GameObject dialogueBoxPrefab;
    public Transform pivot;
    public DialogueObject warning;
    void Start()
    {
        pivot = GameObject.Find("WarningTrigger").transform;
        if(SceneManager.GetActiveScene().name != "DebugScene") mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>()?.onClick.AddListener(() => ToBattleScene(enemy));
    }

    public void ToBattleScene(Enemy enemy)
    {
        if (GameManager.playerData.playerDeck.Count < 5) {
            NotEnoughCards(warning);
            return;
        }
        if (SceneManager.GetActiveScene().name != "DebugScene") GameManager.currentNode = mapController.GetGoIndex(gameObject);
        GameManager.nextEnemy = enemy;
        SceneManager.LoadScene("BattleScene");
    }

    public void NotEnoughCards(DialogueObject warningText) {
        var obj = Instantiate(dialogueBoxPrefab,pivot);
        obj.transform.localScale = obj.transform.localScale * 2;
        obj.GetComponent<GenericDialogueBox>().dialogue = warningText;
    }
}
