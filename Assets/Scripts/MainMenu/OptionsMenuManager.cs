using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    public Slider m_GeneralVolumeSlider;
    public Slider m_MusicVolumeSlider;
    public Slider m_SfxVolumeSlider;

    public AudioMixer m_AudioMixer;

    private void Update()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        float generalVol = m_GeneralVolumeSlider.value;
        m_AudioMixer.SetFloat("General", Mathf.Log10(generalVol) * 20);
        PlayerPrefs.SetFloat("GeneralVolume", generalVol);

        float musicVol = m_MusicVolumeSlider.value;
        m_AudioMixer.SetFloat("Music", Mathf.Log10(musicVol) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
        float fxVol = m_SfxVolumeSlider.value;
        m_AudioMixer.SetFloat("Sfx", Mathf.Log10(fxVol) * 20);
        PlayerPrefs.SetFloat("SfxVolume", fxVol);
    }
    public void LoadVolume()
    {
        m_GeneralVolumeSlider.value = PlayerPrefs.GetFloat("GeneralVolume");
        m_MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        m_SfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        SetVolume();
    }
}
