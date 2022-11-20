using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    public GameObject shop;
    public GameObject[] encounterList;
    public GameObject[] loreList;    
    [SerializeField] private MapPathSelector mapController;
        
    public GameObject fadeGo;
    Fade fade;
    public SlideAnimCoroutines coroutines;

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
        GameManager.UpdateNodeProgress();
        mapController.UpdateMap();
    }

    private void CloseWindows() {
        shop.SetActive(false);
        CloseArray(encounterList);
        CloseArray(loreList);
    }

    private void CloseArray(GameObject[] list) {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].SetActive(false);
        }
    }

    public void ToEncounter() {
        fade.FadeOut();
        fade.lateFadeIn();

        //encounterList[encounterId].SetActive(true);

        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));
    }
    public void ToLore(int loreId)
    {
        fade.FadeOut();
        fade.lateFadeIn();

        loreList[loreId].SetActive(true);

        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, -3000, 0), 100f));

        StartCoroutine(DelayDialogueActivation(loreList,loreId));
    }
    public IEnumerator DelayDialogueActivation(GameObject[] list, int id) {
        yield return new WaitForSeconds(2f);        
        list[id].GetComponent<DialogueActivator>().ActivateDialogue();
    }    
    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
