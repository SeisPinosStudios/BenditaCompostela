using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;
    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject) {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(RunThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[]responseEvents) {
        responseHandler.AddResponseEvent(responseEvents);
    }

    private IEnumerator RunThroughDialogue(DialogueObject dialogueObject) {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            textLabel.text = dialogue;
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
        textLabel.text = string.Empty;
    }
}
