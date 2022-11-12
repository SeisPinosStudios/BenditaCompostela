using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject encounter_1;
        
    public GameObject fadeGo;
    Fade fade;
    SlideAnimCoroutines coroutines;

    static bool encounterHappened;
    void Start()
    {
        fade = fadeGo.GetComponent<Fade>();
        coroutines = gameObject.GetComponent<SlideAnimCoroutines>();
        encounterHappened = false;
    }

    public void ToShop() {
        fade.FadeOut();
        fade.lateFadeIn();
        encounter_1.SetActive(false);
        shop.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));
    }

    public void ToRoute() {
        fade.FadeOut();
        fade.lateFadeIn();

        Invoke("CloseWindows",1f);
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, 0, 0), 100f));

    }

    private void CloseWindows() {
        shop.SetActive(false);
        encounter_1.SetActive(false);
    }

    public void ToEncounter() {        
        encounter_1.SetActive(true);
        shop.SetActive(false);
        if (!encounterHappened)
        {
            encounter_1.GetComponent<DialogueActivator>().ActivateDialogue();
        }
        encounterHappened = true;

        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));
    }    
    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
