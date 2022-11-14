using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerScript : MonoBehaviour
{
    static public bool OnCombat;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInterctable Interctable { get; set; }

    public void DialogueActivation()
    {
        Debug.Log("DIALOGUEACTIVATION");
        Interctable?.Interact(this);
    }
}
