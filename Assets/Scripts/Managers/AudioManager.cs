using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);// To delete previous instance if exist
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = this.GetComponent<AudioSource>();
    }
    #endregion

    AudioSource audioSource;
    public AudioClip ambiantSong;
    public AudioClip grassStep;
    public AudioClip zombieGrowl;
    public AudioClip hauntedTreeGrowl;
    public AudioClip gameOverSong;
    public AudioClip bossSong;
    public AudioClip slashSound;

    private AudioClip lastPlaySong;

    private void Start()
    {
        audioSource.clip = ambiantSong;
        audioSource.Play();
    }

    public void GrassStep()
    {
        audioSource.PlayOneShot(grassStep, 0.2f);
    }

    public void ZombieGrowl()
    {
        audioSource.PlayOneShot(zombieGrowl);
    }

    public void HauntedTreeGrowl()
    {
        audioSource.PlayOneShot(hauntedTreeGrowl);
    }

    public void SlashSound()
    {
        audioSource.PlayOneShot(slashSound);
    }

    public void PlayGameOverSong()
    {
        if (IsSameSongPlay(gameOverSong)) return;

        audioSource.Stop();
        audioSource.clip = gameOverSong;
        lastPlaySong = gameOverSong;
        audioSource.Play();
    }

    public void PlayAmbiantSong()
    {
        if (IsSameSongPlay(ambiantSong)) return;

        audioSource.Stop();
        audioSource.clip = ambiantSong;
        lastPlaySong = ambiantSong;
        audioSource.Play();
    }

    public void PlayBossSong()
    {
        if (IsSameSongPlay(bossSong)) return;

        audioSource.Stop();
        audioSource.clip = bossSong;
        lastPlaySong = bossSong;
        audioSource.Play();
    }

    private bool IsSameSongPlay(AudioClip song)
    {
        return song == lastPlaySong;
    }

    public void StopPlayingSong()
    {
        audioSource.Stop();
        lastPlaySong = null;
    }
}
