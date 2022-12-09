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

    public GameObject bg;
    public GameObject characters;

    void Start()
    {
        VideoHasEnded = false;
        firstCinematic.loopPointReached += startDialogue;
        secondCinematic.loopPointReached += ToRoute;
    }
    void startDialogue(VideoPlayer vp) {
        startDialogueEvent.Invoke();
    }

    public void startSecondCinematic()
    {
        bg.SetActive(false);
        characters.SetActive(false);
        GameObject.Find("Cinematica1").SetActive(false);
        Destroy(GameObject.Find("DialogPivot"));
        secondCinematic.Play();
    }

    public void ToRoute(VideoPlayer vp)
    {
        SceneManager.LoadScene("Sevilla");
    }
}
