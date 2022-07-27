using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } } // Accessor

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);// To delete previous instance if exist

        instance = this;
    }

    AudioSource audioSource;
    public AudioClip ambiantSong;
    public AudioClip grassStep;
    public AudioClip zombieSound;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = ambiantSong;
        audioSource.Play();
    }

    public void GrassStep()
    {
        audioSource.PlayOneShot(grassStep, 0.5f);
    }

    public void ZombieGrowl()
    {
        audioSource.PlayOneShot(zombieSound);
    }
}
