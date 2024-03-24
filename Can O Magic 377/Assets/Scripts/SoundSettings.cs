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

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        SetSliders(sliderValue);
    }

    public void SetSliders(float sliderValue)
    {
        musicSliderH.value = sliderValue;
        musicSliderV.value = sliderValue;
    }
}
