using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public GameObject current;
    public GameObject next;

    public GameObject handPannel;
    public GameObject DefaultDeck;

    public List<DialogueObject> tutorialDialogue;
    public List<Transform> pivotGameObject;
    public GameObject dialogueBoxPrefab;
    public GameObject attackDeckShadow;
    public GameObject defDeck;
    public GameObject attDeck;

    public GameObject fade;

    public int dialogueIndex;

    public void Start()
    {
        dialogueIndex = 0;
        fade.GetComponent<Fade>().FadeIn();
        GameObject.Find("====CANVAS====").GetComponent<Image>().sprite = GameManager.activeBackground;
        current = enemy;
        next = player;
        StartCoroutine(TurnCoroutine());
    }
    IEnumerator TurnCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("FIRST TURN BEGIN");

        Tutorial();
        //Turn();
    }

    public void Turn()
    {
        current.GetComponent<Entity>().ReduceAlteredEffect(CardData.TAlteredEffects.DISARMED, 1);
        current.GetComponent<Entity>().Burn();

        var temp = current;
        current = next;
        next = temp;

        Debug.Log("TURN:" + current.name);

        if (current == player) StartCoroutine(player.GetComponent<PlayerScript>().OnTurnBegin());
        else enemy.GetComponent<EnemyScript>().OnTurnBegin(); 
    }
    public void Tutorial()
    {
        if (dialogueIndex <= tutorialDialogue.Count - 1)
        {
            attackDeckShadow.SetActive(false);
            var diagBox = Instantiate(dialogueBoxPrefab, pivotGameObject[dialogueIndex]);
            diagBox.GetComponent<GenericDialogueBox>().dialogue = tutorialDialogue[dialogueIndex];
            diagBox.GetComponent<Button>()?.onClick.AddListener(() => ClosedDialogueBox());

            if (pivotGameObject[dialogueIndex].gameObject.name == "Pivot_AttackDeck")
            {
                attackDeckShadow.SetActive(true);
            }
            if (pivotGameObject[dialogueIndex].gameObject.name == "Pivot_DefDeck")
            {
                attackDeckShadow.SetActive(true);
                attDeck.transform.SetSiblingIndex(0);
                attackDeckShadow.transform.SetSiblingIndex(1);
                defDeck.transform.SetSiblingIndex(2);
            }

        }
        else 
        {
            Turn();
        }

        
    }
    public void ClosedDialogueBox() {
        dialogueIndex++;
        Tutorial();
    }
}
