using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator fadeAn;
    void Start()
    {
        fadeAn = gameObject.GetComponent<Animator>();
    }

    public void FadeOut() {
        fadeAn.Play("fadeOut");
    }

    public void FadeIn() {
        fadeAn.Play("fadeIn");
    }

    public void lateFadeOut()
    {
        Invoke("FadeOut", 1.5f);
    }

    public void lateFadeIn()
    {
        Invoke("FadeIn", 1.5f);
    }
}
