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

    #region Transitions
    Vector3 originalPosition;
    Vector3 originalScale;
    Vector3 actualPosition;
    Vector3 actualScale;
    Vector3 desiredPosition;
    Vector3 desiredScale;
    bool inPlace = true;
    public float progress = 1;
    float speed = 1f;
    #endregion

    private void Awake()
    {
        //transform.localPosition = new Vector3(960, -540, 0);
        transform.localPosition = new Vector3(0, -1080, 0);
        transform.localScale = new Vector3(1, 1, 1);

        originalPosition = transform.localPosition;
        originalScale = transform.localScale;

        desiredPosition = transform.localPosition;
        desiredScale = transform.localScale;

        actualPosition = transform.localPosition;
        actualScale = transform.localScale;
    }
    void Start()
    {
        //transform.localPosition = new Vector3(0, -1080, 0);
        //transform.localScale = new Vector3(1, 1, 1);
        fade = fadeGo.GetComponent<Fade>();
        fade.FadeIn();
        coroutines = gameObject.GetComponent<SlideAnimCoroutines>();
        Invoke("EneableTitleAnim", 1f);

        gameManager.GetComponent<GameManager>().ResetGameManager();
    }

    #region Title Animation
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

    #region Button Methods
    public void StartToMainMenu() {
        if (!inPlace) return;
        //StartCoroutine(coroutines.animPos(new Vector3(0, 0, 0), 15.5f));
        MoveTo(new Vector3(0, 0, 0), transform.localScale);
    }
    public void ToCredits()
    {
        if (!inPlace) return;
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        StartCoroutine(coroutines.animPos(new Vector3(3000, 0, 0), 100f));
        SceneManager.LoadScene("Credits");
    }
    public void ToMainMenu()
    {
        if (!inPlace) return;
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        //StartCoroutine(coroutines.animPos(new Vector3(0, 0, 0), 100f));
        MoveTo(new Vector3(0, 0, 0), transform.localScale);
    }
    public void ToPlay()
    {
        if (!inPlace) return;
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        
        if (transform.localPosition == new Vector3(-3000, 2000, 0))
        {
            Invoke("StopAllCoroutines", 1.7f);
            //StartCoroutine(coroutines.animScaleAndPos(new Vector3(0.5f, 0.5f, 1), new Vector3(-1548, 1049), 50f));
            MoveTo(new Vector3(-1548, 1049, 0), new Vector3(0.5f, 0.5f, 1));
            //StartCoroutine(coroutines.MoveSlideDelay(new Vector3 (1,1,1), new Vector3(-3000, 0, 0), 1f, 1f));
            StartCoroutine(DelayedMoveTo(new Vector3(-3000, 0, 0), new Vector3(1, 1, 1), 1f));
        } else
        {
            //StartCoroutine(coroutines.animPos(new Vector3(-3000, 0, 0), 100f));
            MoveTo(new Vector3(-3000, 0, 0), transform.localScale);
        }
    }
    public void ToMap()
    {
        if(!inPlace) return;
        fade.FadeOut();
        fade.lateFadeIn();
        StopAllCoroutines();
        Invoke("StopAllCoroutines", 1.7f);
        //StartCoroutine(coroutines.animScaleAndPos(new Vector3(4,4,1),new Vector3(-11754,-170), 50f));
        MoveTo(new Vector3(-11754, -170, 0), new Vector3(4, 4, 1));
        //StartCoroutine(coroutines.MoveSlideDelay(new Vector3(1, 1, 1),new Vector3(-3000, 2000, 0), 1f,1f));
        StartCoroutine(DelayedMoveTo(new Vector3(-3000, 2000, 0), new Vector3(1, 1, 1), 1f));
    }
    #endregion

    #region Movement
    public void MoveTo(Vector3 position, Vector3 scale)
    {
        actualPosition = transform.localPosition;
        actualScale = transform.localScale;

        desiredPosition = position;
        desiredScale = scale;

        progress = 0;
    }
    public IEnumerator DelayedMoveTo(Vector3 position, Vector3 scale, float delay)
    {
        yield return new WaitForSeconds(delay);
        desiredPosition = position;
        desiredScale = scale;

        transform.position = position;
        transform.localScale = scale;
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

    public void Update()
    {
        
        if (transform.localPosition == desiredPosition && transform.localScale == desiredScale) inPlace = true;
        else inPlace = false;

        if (!inPlace)
        {
            Debug.Log("MOVIENDO");
            var position = Vector3.Lerp(actualPosition, desiredPosition, progress);
            var scale = Vector3.Lerp(actualScale, desiredScale, progress);
            progress += speed * Time.deltaTime;
            transform.localPosition = position;
            transform.localScale = scale;
        }
    }
}
