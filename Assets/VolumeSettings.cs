using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Awake()
    {
        StartCoroutine(InitializeSettings());
    }

    private IEnumerator InitializeSettings()
    {
        // Wait until MainSystem.Instance is initialized
        while (MainSystem.Instance == null)
        {
            yield return null;
        }

        // Load saved volume settings on game start
        LoadVolume();
    }

    public void SetMusicVolume()
    {
        if (myMixer == null || musicSlider == null)
        {
            Debug.LogError("Required components are not set.");
            return;
        }

        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        MainSystem.Instance.BGM_Volume = volume;
    }    

    public void SetSFXVolume()
    {
        if (myMixer == null || SFXSlider == null)
        {
            Debug.LogError("Required components are not set.");
            return;
        }

        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        MainSystem.Instance.SFX_Volume = volume;
    } 

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = 1.0f; // Default value
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            SFXSlider.value = 1.0f; // Default value
        }

        // Set volumes after loading values
        SetMusicVolume();
        SetSFXVolume();
    }
}
