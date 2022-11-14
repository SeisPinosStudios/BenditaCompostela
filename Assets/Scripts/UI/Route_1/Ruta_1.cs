using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject encounter_0;
    [SerializeField] private GameObject encounter_1;
    [SerializeField] private GameObject lore_0;
    [SerializeField] private MapPathSelector mapController;
        
    public GameObject fadeGo;
    Fade fade;
    SlideAnimCoroutines coroutines;

    void Start()
    {
        CloseWindows();
        fade = fadeGo.GetComponent<Fade>();
        coroutines = gameObject.GetComponent<SlideAnimCoroutines>();        
    }

    public void ToShop() {
        fade.FadeOut();
        fade.lateFadeIn();
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
        GameManager.gameProgressContext++;
        mapController.UpdateMap();

    }
    private void CloseWindows() {
        shop.SetActive(false);
        encounter_1.SetActive(false);
        encounter_0.SetActive(false);
        lore_0.SetActive(false);
    }

    public void ToEncounter(int encounterId) {        
        switch (encounterId)
        {
            case 0:
                Debug.Log("ENCONTRONAZO " + encounterId);
                encounter_0.SetActive(true);
                encounter_0.GetComponent<DialogueActivator>().ActivateDialogue();
                break;
            case 1:
                Debug.Log("ENCONTRONAZO " + encounterId);
                encounter_1.SetActive(true);
                encounter_1.GetComponent<DialogueActivator>().ActivateDialogue();
                break;
        }

        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));

    }
    public void ToLore(int loreId)
    {        
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));
        switch (loreId)
        {
            case 0:
                lore_0.SetActive(true);
                //lore_0.GetComponent<DialogueActivator>().ActivateDialogue();
                break;
        }

    }
    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
