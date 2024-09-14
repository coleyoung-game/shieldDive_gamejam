using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clip ------")]
    public AudioClip maintheme;
    public AudioClip trampoline;
    public AudioClip princessyay;
    public AudioClip bombblow2;
    public AudioClip dragonroar;
    public AudioClip attacksound;
    public AudioClip spike;
    public AudioClip dashavoid;

    private void Start()
    {
        musicSource.clip = maintheme;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
