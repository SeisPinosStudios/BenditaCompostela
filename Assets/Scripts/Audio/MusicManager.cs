using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> songs;
    public List<AudioClip> battleMusic;
    public AudioClip shopMusic;
    public AudioSource audioSource;
    public List<AudioClip> battleEndMusic;
    string scene;
    float delay = 1.0f;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = songs[0];
        audioSource.Play();
        audioSource.volume = GameManager.musicVolume;
    }

    public void NextSong()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene") { audioSource.Stop(); return; }
        audioSource.clip = songs[Random.Range(0, songs.Count)];
        audioSource.PlayDelayed(delay);
    }
    public void ShopMusic()
    {
        audioSource.Stop();
        audioSource.clip = shopMusic;
        audioSource.PlayDelayed(delay);
    }
    public void BattleMusic()
    {
        audioSource.Stop();
        audioSource.clip = GameManager.nextEnemy.name == "Santiago" ? battleMusic[1] : battleMusic[0];
        audioSource.PlayDelayed(delay);
    }
    public void PlayBattleEnd(bool won)
    {
        audioSource.Stop();
        audioSource.clip = won ? battleEndMusic[0] : battleEndMusic[1];
        audioSource.PlayDelayed(delay);
    }
    public void StopSong()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void Update()
    {
        if (audioSource.isPlaying) return;

        if (SceneManager.GetActiveScene().name == "BattleScene") BattleMusic();
        else NextSong();
    }

    /*
    public void NextSong(float delay)
    {
        Debug.Log("NEXT SONG");
        if (battleSource.isPlaying) battleSource.Stop();
        if (shopSource.isPlaying) shopSource.Stop();
        audioSources[Random.Range(0, audioSources.Count)].Play();
    }
    
    void MusicQueue()
    {
        if (scene == "Cinematic_1" || scene == "Credits" || scene == "DeathScene") { StopAllMusic(); return; }
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
    public void StopAllMusic()
    {
        foreach (AudioSource source in audioSources) if (source.isPlaying) source.Stop();
    }

    public void Update()
    {
        MusicQueue();
        scene = SceneManager.GetActiveScene().name;
    }*/
}
