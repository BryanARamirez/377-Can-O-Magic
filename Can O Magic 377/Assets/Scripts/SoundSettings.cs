using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSliderV;
    public Slider musicSliderH;
    public Slider sfxSliderV;
    public Slider sfxSliderH;
    public Slider masterSliderV;
    public Slider masterSliderH;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
    public void SetMusicVolume()
    {
        float musicVolume;
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                musicVolume = musicSliderV.value;
                audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
                SetMusicSliders(musicVolume);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                musicVolume = musicSliderH.value;
                audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
                SetMusicSliders(musicVolume);
                break;

            default:
                break;
        }
    }
    public void SetSFXVolume()
    {
        float sfxVolume;
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                sfxVolume = sfxSliderV.value;
                audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
                SetSFXSliders(sfxVolume);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                sfxVolume = sfxSliderH.value;
                audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
                SetSFXSliders(sfxVolume);
                break;

            default:
                break;
        }
    }
    public void SetMasterVolume()
    {
        float masterVolume;
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                masterVolume = masterSliderV.value;
                audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
                SetMasterSliders(masterVolume);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                masterVolume = masterSliderH.value;
                audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
                SetMasterSliders(masterVolume);
                break;

            default:
                break;
        }
    }

    public void SetMusicSliders(float sliderValue)
    {
        musicSliderH.value = sliderValue;
        musicSliderV.value = sliderValue;
    }
    public void SetSFXSliders(float sliderSFXValue)
    {
        sfxSliderH.value = sliderSFXValue;
        sfxSliderV.value = sliderSFXValue;
    }
    public void SetMasterSliders(float sliderMasterValue)
    {
        masterSliderH.value = sliderMasterValue;
        masterSliderV.value = sliderMasterValue;
    }
}
