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
        audioSource.loop = false;
    }

    public void NextSong()
    {
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
}
