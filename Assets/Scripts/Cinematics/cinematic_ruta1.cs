using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class cinematic_ruta1 : MonoBehaviour
{
    [SerializeField]
    VideoPlayer myVideoPlayer;
    void Start()
    {
        myVideoPlayer.loopPointReached += changeScene;
    }

    void changeScene(VideoPlayer vp) {        
        SceneManager.LoadScene(2);
    }
}
