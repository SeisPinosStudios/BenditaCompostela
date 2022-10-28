using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObject;
    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueObject == null) {
            Debug.Log("DialogueObject es NULL");
            return;
        }

        if (dialogueObject.Responses == null)
        {
            Debug.Log("DialogueObject.Responses es NULL");
            return;
        }
        /*if (events != null && events.Length == dialogueObject.Responses.Length)
        {
            Debug.Log(events.Length + " |||| " + dialogueObject.Responses.Length);
            Debug.Log("Hay una cantidad de eventos igual a las respuestas del dialogo");
            return;
        }*/

        if (events == null)
        {
            Debug.Log("events es NULL");
            events = new ResponseEvent[dialogueObject.Responses.Length];
        } else
        {
            Debug.Log("events no es NULL");
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }

        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];
            if (events[i]!=null)
            {
                events[i].name = response.ResponseText;
                continue;
            }
            events[i] = new ResponseEvent() { name = response.ResponseText };
        }
    }
}
