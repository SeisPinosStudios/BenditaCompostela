using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class cinematic_ruta1 : MonoBehaviour
{
    [SerializeField] VideoPlayer myVideoPlayer;
    [SerializeField] DialogueUI dialogueUI;

    public bool VideoHasEnded;


    void Start()
    {
        VideoHasEnded = false;
        myVideoPlayer.loopPointReached += startDialogue;
    }

    void startDialogue(VideoPlayer vp) {
        gameObject.GetComponent<DialogueActivator>().ActivateDialogue();
        VideoHasEnded = true;
    }
    private void Update()
    {
        if (!dialogueUI.IsOpen && VideoHasEnded)
        {
            SceneManager.LoadScene(2);
        }
    }
}
