using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruta_1 : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject encounter_1;
    
    Animator slideAn;
    public GameObject fadeGo;
    Fade fade;

    static bool encounterHappened;
    void Start()
    {
        slideAn = gameObject.GetComponent<Animator>();
        fade = fadeGo.GetComponent<Fade>();
        encounterHappened = false;
    }

    public void ruta1ToShop() {
        slideAn.SetBool("isShop", true);
        encounter_1.SetActive(false);
        shop.SetActive(true);
    }

    public void shopToRuta1() {
        slideAn.SetBool("isShop", false);
        Invoke("CloseWindows",1f);
    }

    private void CloseWindows() {
        shop.SetActive(false);
        encounter_1.SetActive(false);
    }

    public void RouteToEncounter() {        
        slideAn.SetBool("isShop", true);
        encounter_1.SetActive(true);
        shop.SetActive(false);
        
        if (!encounterHappened)
        {
            encounter_1.GetComponent<DialogueActivator>().ActivateDialogue();
        }
        encounterHappened = true;
    }    
    void changeLevel(int lvl) {
        SceneManager.LoadScene(lvl);
    }
}
