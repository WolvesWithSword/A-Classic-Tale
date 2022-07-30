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
    }
    #endregion

    AudioSource audioSource;
    public AudioClip ambiantSong;
    public AudioClip grassStep;
    public AudioClip zombieGrowl;
    public AudioClip hauntedTreeGrowl;
    public AudioClip gameOverSong;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
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

    public void PlayGameOverSong()
    {
        audioSource.Stop();
        audioSource.clip = gameOverSong;
        audioSource.Play();
    }

    public void PlayAmbiantSong()
    {
        audioSource.Stop();
        audioSource.clip = ambiantSong;
        audioSource.Play();
    }
}
