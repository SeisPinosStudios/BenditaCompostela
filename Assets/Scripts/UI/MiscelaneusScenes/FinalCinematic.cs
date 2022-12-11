using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FinalCinematic : MonoBehaviour
{
    public VideoPlayer firstCinematic;

    private void Awake()
    {
        firstCinematic.loopPointReached += ToFinalScene;
    }

    private void ToFinalScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("FinalScene");
    }
}
