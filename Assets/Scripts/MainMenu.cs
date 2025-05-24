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

if (PlayerPrefs.HasKey("GeneralVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }

        ReviseResolution();
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
