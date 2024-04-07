using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSliderV;
    public Slider musicSliderH;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        SetMusicVolume();
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
                SetSliders(musicVolume);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                musicVolume = musicSliderH.value;
                audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
                SetSliders(musicVolume);
                break;

            default:
                break;
        }
    }

    public void SetSliders(float sliderValue)
    {
        musicSliderH.value = sliderValue;
        musicSliderV.value = sliderValue;
    }
}
