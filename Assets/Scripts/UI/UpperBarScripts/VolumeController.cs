using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;
    public void Awake()
    {
        MusicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
        MusicSlider.onValueChanged.AddListener(MusicVolume);
        SFXSlider.onValueChanged.AddListener(SFXVolumen);
    }

    public void MusicVolume(float volume)
    {
        GameManager.SetMusicVolume(volume);
    }

    public void SFXVolumen(float volume)
    {
        GameManager.SetSFXVolume(volume);
    }
}
