using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class BattleButton : MonoBehaviour
{
    public Enemy enemy;
    private MapPathSelector mapController;
    public GameObject dialogueBoxPrefab;
    public Transform pivot;
    public DialogueObject warning;
    public AudioManager audioManager;
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "DebugScene") pivot = GameObject.Find("WarningTrigger").transform;
        if(SceneManager.GetActiveScene().name != "DebugScene") mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapPathSelector>();
        gameObject.GetComponent<Button>()?.onClick.AddListener(() => ToBattleScene(enemy));
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void ToBattleScene(Enemy enemy)
    {
        audioManager.PlaySound("Evento");
        if (GameManager.playerData.playerDeck.Count < 5 || GameManager.playerData.playerDeck.Find(card => card.GetType() == typeof(Weapon)) == null) {
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
