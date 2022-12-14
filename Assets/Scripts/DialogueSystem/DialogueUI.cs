using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public Sprite character;
    public Image backgroundImage;
    public Image characterImage;
    public GameObject parent;

    private CinematicCharacterDisplay cinematicCharacterDisplay;

    public bool IsOpen { get; private set; }

    public ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;
    private void Awake()
    {
        if (characterImage.sprite != null) characterImage.sprite = character;
        else characterImage.gameObject.SetActive(false);
        characterImage.SetNativeSize();
        if (SceneManager.GetActiveScene().name != "Cinematic_1") backgroundImage.sprite = GameManager.activeBackground;
        else 
        {
            characterImage.gameObject.SetActive(false);
            cinematicCharacterDisplay = GameObject.Find("CinematicCharacterDisplay").GetComponent<CinematicCharacterDisplay>();
            cinematicCharacterDisplay.EneableBackground();
        }

        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(RunThroughDialogue(dialogueObject));
    }

    public void ShowResponseDialog(DialogueObject dialogueObject, ResponseEvent response) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(RunThroughDialogueResponse(dialogueObject, response));
    }

    public void AddResponseEvents(List<ResponseEvent> responseEvents) {
        responseHandler.AddResponseEvent(responseEvents);
    }

    private IEnumerator RunThroughDialogue(DialogueObject dialogueObject) {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            if (SceneManager.GetActiveScene().name == "Cinematic_1") cinematicCharacterDisplay.CharacterSelector(dialogue);
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else {
            CloseDialogBox();
        }
    }

    private IEnumerator RunThroughDialogueResponse(DialogueObject dialogueObject, ResponseEvent response)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {            
            string dialogue = dialogueObject.Dialogue[i];
            
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogBox();
            Debug.Log(response.name);
            if (response != null) response.onPickedResponse.Invoke();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue) {
        typeWriterEffect.Run(dialogue, textLabel);
        while (typeWriterEffect.IsRunning)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                typeWriterEffect.Stop();
            }
        }
    }
    public void CloseDialogBox() {
        IsOpen = false;
        dialogueBox.SetActive(false);
        //parent.SetActive(false);
        textLabel.text = string.Empty;
    }
}
