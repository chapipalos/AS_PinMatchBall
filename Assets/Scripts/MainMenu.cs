using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;
  

    private void Start()
    {
       
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void UpdateVolume(float value)
    {
       
    }

    public void UpdateBrightness(float value)
    {
      
    }
}

