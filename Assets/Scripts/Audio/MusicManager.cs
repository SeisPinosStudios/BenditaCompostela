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

    public List<string> scenes;
    public List<Enemy> boss;
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
        if (scenes.Contains(SceneManager.GetActiveScene().name)) return;
        Debug.Log("NEXT SONG");
        //if (SceneManager.GetActiveScene().name == "Cinematic_1" || SceneManager.GetActiveScene().name == "FinalCinematic" || SceneManager.GetActiveScene().name == "FinalScene" || SceneManager.GetActiveScene().name == "Credits") return;
        StartCoroutine(PlayClip(songs[Random.Range(0, songs.Count)]));
    }
    public void ShopMusic()
    {
        StopSong();
        StartCoroutine(PlayClip(shopMusic));
    }
    public void BattleMusic()
    {
        StopSong();
        StartCoroutine(PlayClip(boss.Contains(GameManager.nextEnemy) ? battleMusic[1] : battleMusic[0]));
    }
    public void PlayBattleEnd(bool won)
    {
        StopSong();
        StartCoroutine(PlayClip(won ? battleEndMusic[0] : battleEndMusic[1]));
    }
    public void StopSong()
    {
        audioSource.Stop();
        StopAllCoroutines();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    IEnumerator PlayClip(AudioClip song)
    {
        Debug.Log("PLAYING CLIP" + song.name);
        audioSource.clip = song;
        audioSource.PlayDelayed(1.0f);
        yield return new WaitForSeconds(song.length);
        Debug.Log("EN COROUTINE");
        NextSong();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene" && !audioSource.isPlaying) BattleMusic();
        else if (!audioSource.isPlaying) NextSong();
    }
}
