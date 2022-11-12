using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables
    public GameObject fadeGo;
    Fade fade;
    SlideAnimCoroutines coroutines;
    #endregion

    #region Initialization
    void Start()
    {
        transform.localPosition = new Vector3(0,-1080,0);
        transform.localScale= new Vector3(1, 1, 1);        
        fade = fadeGo.GetComponent<Fade>();      
        fade.FadeIn();
        coroutines = gameObject.GetComponent<SlideAnimCoroutines>();
    }
    #endregion

    #region Transitions between Screens
    public void StartToMainMenu() {        
        StartCoroutine(coroutines.animPos(new Vector3(0, 0, 0), 15.5f));
    }

    public void ToCredits()
    {
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(3000, 0, 0), 100f));
    }
    public void ToMainMenu()
    {
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, 0, 0), 100f));
    }
    public void ToPlay()
    {
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        
        if (transform.localPosition == new Vector3(-3000, 2000, 0))
        {
            Invoke("StopAllCoroutines", 1.7f);
            StartCoroutine(coroutines.animScaleAndPos(new Vector3(0.5f, 0.5f, 1), new Vector3(-1548, 1049), 50f));
            StartCoroutine(coroutines.MoveSlideDelay(new Vector3 (1,1,1), new Vector3(-3000, 0, 0), 1f, 1f));
        } else
        {
            StartCoroutine(coroutines.animPos(new Vector3(-3000, 0, 0), 100f));
        }
    }
    
    public void ToMap()
    {
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        Invoke("StopAllCoroutines", 1.7f);
        StartCoroutine(coroutines.animScaleAndPos(new Vector3(4,4,1),new Vector3(-11754,-170), 50f));        
        StartCoroutine(coroutines.MoveSlideDelay(new Vector3(1, 1, 1),new Vector3(-3000, 2000, 0), 1f,1f));
    }

    #endregion

    #region Route Selection
    public void ruta1() 
    {
        fade.FadeOut();
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, 2000, 0), 100f));

        Invoke("ruta1LoadScene",1.5f);        
    }
    void ruta1LoadScene() {
        SceneManager.LoadScene(1);
    }
    #endregion


}
