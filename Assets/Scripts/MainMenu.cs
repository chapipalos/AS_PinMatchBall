using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider GeneralvolumeSlider;
    public Slider musicSlider;
    public Slider fxSlider;

    public AudioMixer audiomixer;

    Resolution[] resol;
    public TMP_Dropdown resdrop;
    public Toggle FullScr;
    public AudioSource backgroundMusic;
    public static int nextSceneIndex;

    private void Start()
    {
        FullScr.isOn = Screen.fullScreen;

        // Carga valores guardados
        float generalVol = PlayerPrefs.GetFloat("GeneralVolume", 0.5f);
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float fxVol = PlayerPrefs.GetFloat("FxVolume", 0.5f);

        GeneralvolumeSlider.value = generalVol;
        musicSlider.value = musicVol;
        fxSlider.value = fxVol;

        // Aplica volumen convertido a decibelios
        audiomixer.SetFloat("GeneralVolume", VolumeToDb(generalVol));
        audiomixer.SetFloat("MusicVolume", VolumeToDb(musicVol));
        audiomixer.SetFloat("FxVolume", VolumeToDb(fxVol));

        backgroundMusic.Play();
        ReviseResolution();
    }

    public void ChangeGeneralVolume(float value)
    {
        audiomixer.SetFloat("GeneralVolume", VolumeToDb(value));
        PlayerPrefs.SetFloat("GeneralVolume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        audiomixer.SetFloat("MusicVolume", VolumeToDb(value));
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeFXVolume(float value)
    {
        audiomixer.SetFloat("FxVolume", VolumeToDb(value));
        PlayerPrefs.SetFloat("FxVolume", value);
    }

    // Conversión de volumen lineal (0-1) a decibelios (dB)
    private float VolumeToDb(float volume)
    {
        // Clamp para evitar log(0) que da infinito negativo
        return Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    private void ReviseResolution()
    {
        int ActualRes = 0;
        resol = Screen.resolutions;
        resdrop.ClearOptions();
        List<string> optionsRes = new List<string>();

        for (int i = 0; i < resol.Length; i++)
        {
            string option = resol[i].width + " x " + resol[i].height;
            optionsRes.Add(option);

            if (Screen.fullScreen && resol[i].width == Screen.currentResolution.width && resol[i].height == Screen.currentResolution.height)
            {
                ActualRes = i;
            }
        }

        resdrop.AddOptions(optionsRes);
        resdrop.value = ActualRes;
        resdrop.RefreshShownValue();
    }

    public void changeResolution(int index)
    {
        Resolution resolut = resol[index];
        Screen.SetResolution(resolut.width, resolut.height, Screen.fullScreen);
    }

    public void play()
    {
        StartCoroutine(LoadSceneWithTransition(1));
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        nextSceneIndex = sceneIndex;
        SceneManager.LoadScene("SceneTransition");
        yield return null;
    }
}
