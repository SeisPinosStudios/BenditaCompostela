using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInterctable
{
    [SerializeField] private PlayerScript player;

    public void UpdateDialogueObject(DialogueObject dialogueObject) 
    {
        this.dialogueObject = dialogueObject;
    }
    void Start()
    {
        player.Interctable = this;
    }

    public void ActivateDialogue()
    {
        player.DialogueActivation();
    }
    [SerializeField] private DialogueObject dialogueObject;
    public void Interact(PlayerScript player) {

        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

}
