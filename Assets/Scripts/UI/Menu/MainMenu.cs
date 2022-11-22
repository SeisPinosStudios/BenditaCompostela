using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables
    public GameObject fadeGo;
    public GameObject title;
    public GameObject touchText;
    public GameObject savedDataScreen;
    public GameObject gameManager;
    Fade fade;
    SlideAnimCoroutines coroutines;
    #endregion

    #region Initialization
    void Start()
    {
        transform.localPosition = new Vector3(0, -1080, 0);
        transform.localScale = new Vector3(1, 1, 1);
        fade = fadeGo.GetComponent<Fade>();
        fade.FadeIn();
        coroutines = gameObject.GetComponent<SlideAnimCoroutines>();
        Invoke("EneableTitleAnim", 1f);
        gameManager.GetComponent<GameManager>().ResetGameManager();
    }
    public void EneableTitleAnim() {
        title.SetActive(true);
        Invoke("AnimSound", 1f);
        Invoke("EneableTouchText", 2f);
    }
    public void EneableTouchText()
    {
        touchText.SetActive(true);
    }
    public void AnimSound()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound("MainAnimation");
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
        SceneManager.LoadScene("Credits");
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

    #region Data Detected
    public void CheckForData()
    {
        if(PlayerPrefs.GetInt("SavedData") == 1)
        {
            Debug.Log("Entra en hay datos");
            savedDataScreen.SetActive(true);
        }
        else
        {
            GameManager.ActualRoute = "Sevilla";
            ToCinematic();
        }
    }
    #endregion

    #region Route Selection
    public void ToRoute() 
    {
        fade.FadeOut();
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(0, 2000, 0), 100f));

        Invoke("LoadRouteScene", 1.5f);        
    }
    void LoadRouteScene() {
        SceneManager.LoadScene(GameManager.ActualRoute);
    }

    public void ToCinematic()
    {
        SceneManager.LoadScene("Cinematic_1");
    }
    #endregion

}
