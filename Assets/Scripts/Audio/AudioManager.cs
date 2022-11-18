using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;
    public void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void PlaySound(string name)
    {
        var sound = sounds.Find(s => s.name == name);
        if (sound == null) return;
        sound.source.Play();
    }
}
