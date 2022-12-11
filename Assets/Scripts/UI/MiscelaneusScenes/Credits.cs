using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float fadeSpeed;
    public GameObject fade;
    [TextArea(3,10)]
    public string[] credits;
    int progress;
    Color color;

    public void Awake()
    {
        color = fade.GetComponent<Image>().color;
        StartCoroutine(FadeController());
    }

    public void Fade()
    {
        if(color.a < 1)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        var alpha = color.a;

        while(fade.GetComponent<Image>().color.a < 1)
        {
            Debug.Log(alpha);
            alpha = color.a + (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, alpha);
            fade.GetComponent<Image>().color = color;
            yield return null;
        }

        progress++;
    }

    IEnumerator FadeIn()
    {
        var alpha = color.a;

        while (fade.GetComponent<Image>().color.a > 0)
        {
            Debug.Log(alpha);
            alpha = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, alpha);
            fade.GetComponent<Image>().color = color;
            yield return null;
        }
    }

    IEnumerator FadeController()
    {
        while(progress < credits.Length)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(FadeOut());
        }
    }

    public void Leave(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        if(progress < credits.Length) GetComponentInChildren<TextMeshProUGUI>().text = credits[progress];

        if (progress == credits.Length) SceneManager.LoadScene("MainMenu");
    }
}
