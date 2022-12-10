using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    public List<AudioSource> audioSource;
    public void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            audioSource.Add(sound.source);
            sound.source.clip = sound.clip;
            sound.source.volume = GameManager.SFXVolume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void PlaySound(string name)
    {
        var sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.Play();
    }

    public void SetVolume(float volume)
    {
        foreach(AudioSource source in audioSource) source.volume = volume;
    }
}
