using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    float SliderVolumeValue;
    Resolution[] resol;
    public TMP_Dropdown resdrop;
    public Toggle FullScr;

    public static int nextSceneIndex; // Para almacenar la escena que se quiere cargar después de la transición

    private void Start()
    {
        if (Screen.fullScreen)
        {
            FullScr.isOn = true;
        }
        else
        {
            FullScr.isOn = false;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = volumeSlider.value;
        ReviseResolution();
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    private void ReviseResolution()
    {
        int ActualRes = 0;
        string option;
        resol = Screen.resolutions;
        resdrop.ClearOptions();
        List<string> optionsRes = new List<string>();

        for (int i = 0; i < resol.Length; i++)
        {
            option = resol[i].width + " x " + resol[i].height;
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

    public void changeSlider(float value)
    {
        SliderVolumeValue = value;
        PlayerPrefs.SetFloat("volumeAudio", SliderVolumeValue);
        AudioListener.volume = volumeSlider.value;
    }

    public void play()
    {
        StartCoroutine(LoadSceneWithTransition(1)); // Cambia 1 por el índice de tu escena de juego
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
