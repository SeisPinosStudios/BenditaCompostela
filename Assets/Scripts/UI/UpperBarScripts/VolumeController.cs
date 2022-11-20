using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public void Awake()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        Debug.Log(volume);
        AudioListener.volume = volume;
    }
}
