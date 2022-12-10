using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public List<Sound> sounds;
    public List<AudioSource> audioSources;
    public AudioSource battleSource;
    public AudioSource shopSource;

    public void Awake()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == "shop" || sound.name == "battle") continue;
            sound.source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(sound.source);
            sound.source.clip = sound.clip;
            sound.source.volume = GameManager.musicVolume;
            sound.source.pitch = sound.pitch;
            sound.source.playOnAwake = false;
            sound.source.Stop();
        }
    }

    public void NextSong(float delay)
    {
        Debug.Log("NEXT SONG");
        if (battleSource.isPlaying) battleSource.Stop();
        if (shopSource.isPlaying) shopSource.Stop();
        audioSources[Random.Range(0, audioSources.Count)].Play();
    }
    public void SetVolume(float volume)
    {
        foreach(AudioSource source in audioSources) source.volume = volume;
    }
    void MusicQueue()
    {
        foreach (AudioSource source in audioSources) if (source.isPlaying) return;
        if (battleSource.isPlaying || shopSource.isPlaying) return;
        NextSong(1.0f);
    }
    public void BattleMusic()
    {
        foreach(AudioSource source in audioSources) if (source.isPlaying) source.Stop();
        battleSource.Play();
    }
    public void ShopMusic()
    {
        foreach (AudioSource source in audioSources) if (source.isPlaying) source.Stop();
        shopSource.Play();
    }

    public void Update()
    {
        MusicQueue();
    }
}
