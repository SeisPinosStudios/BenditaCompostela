using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class cinematic_ruta1 : MonoBehaviour
{
    [SerializeField] VideoPlayer firstCinematic;
    [SerializeField] VideoPlayer secondCinematic;

    public bool VideoHasEnded;
    public UnityEvent startDialogueEvent;


    void Start()
    {
        VideoHasEnded = false;
        firstCinematic.loopPointReached += startDialogue;
        secondCinematic.loopPointReached += ToRoute;
    }
    void startDialogue(VideoPlayer vp) {
        /*
        gameObject.GetComponent<DialogueActivator>().ActivateDialogue();
        VideoHasEnded = true;*/
        startDialogueEvent.Invoke();
    }

    public void startSecondCinematic()
    {
        GameObject.Find("Cinematica1").SetActive(false);
        Destroy(GameObject.Find("DialogPivot"));
        secondCinematic.Play();
    }

    public void ToRoute(VideoPlayer vp)
    {
        SceneManager.LoadScene("Sevilla");
    }
    private void Update()
    {

    }
}
