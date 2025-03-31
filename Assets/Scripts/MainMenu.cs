using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    float SliderVolumeValue;
    float SliderBrighValue;
    public Slider brightnessSlider;
    public Image BrighPanel;



    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 0.5f);
        BrighPanel.color=new Color(BrighPanel.color.r,BrighPanel.color.g,BrighPanel.color.b, brightnessSlider.value);
    

    }
    public void changeSlider(float value)
    {
        SliderVolumeValue = value;
        PlayerPrefs.SetFloat("volumeAudio", SliderVolumeValue);
        AudioListener.volume=volumeSlider.value;
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }



    public void UpdateBrightness(float value)
    {
        SliderBrighValue = value;
        PlayerPrefs.SetFloat("brightness", SliderBrighValue);
        PlayerPrefs.Save();
        BrighPanel.color = new Color(BrighPanel.color.r, BrighPanel.color.g, BrighPanel.color.b, brightnessSlider.value);
     
    }
}

