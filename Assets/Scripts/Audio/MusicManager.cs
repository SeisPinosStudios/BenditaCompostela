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
    public List<AudioClip> battleEndMusic;
    public AudioSource audioSource;
    string scene;
    float delay = 1.0f;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = GameManager.musicVolume;
        audioSource.loop = false;
        audioSource.Stop();
        StartCoroutine(PlayClip(songs[0]));
    }

    public void NextSong()
    {
        if (SceneManager.GetActiveScene().name == "Cinematic_1" || SceneManager.GetActiveScene().name == "FinalCinematic" || SceneManager.GetActiveScene().name == "FinalScene" || SceneManager.GetActiveScene().name == "Credits") return;
        StartCoroutine(PlayClip(songs[Random.Range(0, songs.Count)]));
    }
    public void ShopMusic()
    {
        audioSource.Stop();
        StartCoroutine(PlayClip(shopMusic));
    }
    public void BattleMusic()
    {
        audioSource.Stop();
        StartCoroutine(PlayClip(GameManager.nextEnemy.name == "Santiago" ? battleMusic[1] : battleMusic[0]));
    }
    public void PlayBattleEnd(bool won)
    {
        audioSource.Stop();
        StartCoroutine(PlayClip(won ? battleEndMusic[0] : battleEndMusic[1]));
    }
    public void StopSong()
    {
        audioSource.Stop();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    IEnumerator PlayClip(AudioClip song)
    {
        audioSource.Stop();
        Debug.Log("PLAYING CLIP" + song.name);
        audioSource.clip = song;
        audioSource.PlayDelayed(1.0f);
        yield return new WaitForSeconds(song.length);
        NextSong();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene" && !audioSource.isPlaying) BattleMusic();
        else if (!audioSource.isPlaying) NextSong();
    }
}
