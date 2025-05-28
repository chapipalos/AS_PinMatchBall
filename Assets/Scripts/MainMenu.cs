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


if (PlayerPrefs.HasKey("GeneralVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }


    }
    public void SetVolume()
    {

        float generalVol = GeneralvolumeSlider.value;
        audiomixer.SetFloat("General", Mathf.Log10(generalVol) * 20);
        PlayerPrefs.SetFloat("GeneralVolume", generalVol);

        float musicVol = musicSlider.value;
        audiomixer.SetFloat("Music", Mathf.Log10(musicVol) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
        float fxVol = fxSlider.value;
        audiomixer.SetFloat("Sfx", Mathf.Log10(fxVol) * 20);
        PlayerPrefs.SetFloat("SfxVolume", fxVol);
    }
   public void LoadVolume()
    {
        GeneralvolumeSlider.value = PlayerPrefs.GetFloat("GeneralVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        fxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        SetVolume();
    }
  


    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
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
