using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInterctable
{
    [SerializeField] private DialogTriggerScript dialogTriggerScript;

    public void UpdateDialogueObject(DialogueObject dialogueObject) 
    {
        this.dialogueObject = dialogueObject;
    }
    void Start()
    {
        dialogTriggerScript.Interctable = this;
    }

    public void ActivateDialogue()
    {
        dialogTriggerScript.DialogueActivation();
    }
    [SerializeField] public DialogueObject dialogueObject;
    public void Interact(DialogTriggerScript dialogTriggerScript) {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.dialogueObject == dialogueObject)
            {
                dialogTriggerScript.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        dialogTriggerScript.DialogueUI.ShowDialogue(dialogueObject);
    }

}
