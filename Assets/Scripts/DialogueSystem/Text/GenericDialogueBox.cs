////using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenericDialogueBox : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    public DialogueObject dialogue;
    void Start()
    {
        textLabel.text = dialogue.Dialogue[0];
    }

    public void CloseDialogueBox() {       
        Destroy(gameObject);
    }
}
