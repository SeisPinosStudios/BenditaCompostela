using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Animator slideAn;
    public GameObject fadeGo;
    Fade fade;
    void Start()
    {
       slideAn = gameObject.GetComponent<Animator>();
        fade = fadeGo.GetComponent<Fade>();
    }

    // Animaciones de transiciones entre pantallas
    public void menuToHistoria()
    {
        slideAn.SetBool("isHistoria", true);
    }
    public void historiaToMenu()
    {
        slideAn.SetBool("isHistoria", false);
    }
    public void menuToTutorial()
    {
        slideAn.SetBool("isTutorial", true);
    }
    public void tutorialToMenu()
    {
        slideAn.SetBool("isTutorial", false);
    }
    public void historiaToMap()
    {
        fade.lateFadeIn();
        fade.FadeOut();       
        slideAn.SetBool("isMap", true);
    }
    public void mapToHistoria()
    {
        fade.lateFadeIn();
        fade.FadeOut();
        slideAn.SetBool("isMap", false);
    }
    // Rutas
    public void ruta1() 
    {
        SceneManager.LoadScene(3);
    }

}
