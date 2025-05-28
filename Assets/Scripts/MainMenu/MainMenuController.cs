using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private List<GameObject> m_Lighs = new List<GameObject>();
    public Transform m_LightsParent;
    public int m_FirstLight;
    public int m_LastLight;
    public float m_TimeToChangeLights;
    private float m_RemainingTimeToChangeLights;

    private List<GameObject> m_PlayLighs = new List<GameObject>();
    public Transform m_PlayLightsParent;

    private List<GameObject> m_OptionsLighs = new List<GameObject>();
    public Transform m_OptionsLightsParent;

    private List<GameObject> m_ExitLighs = new List<GameObject>();
    public Transform m_ExitLightsParent;

    public Button m_PlayButton;

    public Button[] m_RemtachButtons = new Button[2];
    public Button m_OptionsButton;
    public Button[] m_MainMenuButtons = new Button[3];
    public Button[] m_ExitButtons = new Button[3];

    public GameObject m_MainMenu;
    public GameObject m_BlueWinsPanel;
    public GameObject m_RedWinsPanel;
    public GameObject m_OptionsPanel;
    public TMP_Dropdown m_ResolutionDropdown;
    public Toggle m_FullscreenToggle;

    private Resolution[] m_Resolutions;

    private void Awake()
    {
        for (int i = 0; i < m_LightsParent.childCount; i++)
        {
            GameObject lightGo = m_LightsParent.GetChild(i).gameObject;
            GameObject lightPlayGo = m_PlayLightsParent.GetChild(i).gameObject;
            GameObject lightOptGo = m_OptionsLightsParent.GetChild(i).gameObject;
            GameObject lightExitGo = m_ExitLightsParent.GetChild(i).gameObject;
            if (i < m_LastLight)
            {
                lightGo.SetActive(true);
                lightPlayGo.SetActive(true);
                lightOptGo.SetActive(true);
                lightExitGo.SetActive(true);
            }
            else
            {
                lightGo.SetActive(false);
                lightPlayGo.SetActive(false);
                lightOptGo.SetActive(false);
                lightExitGo.SetActive(false);
            }
            m_Lighs.Add(lightGo);
            m_PlayLighs.Add(lightPlayGo);
            m_OptionsLighs.Add(lightOptGo);
            m_ExitLighs.Add(lightExitGo);
        }
        m_RemainingTimeToChangeLights = m_TimeToChangeLights;

        m_PlayButton.onClick.AddListener(Play);
        m_OptionsButton.onClick.AddListener(OptionsPanel);
        foreach (Button button in m_MainMenuButtons)
        {
            button.onClick.AddListener(MainMenuPanel);
        }
        foreach (Button button in m_RemtachButtons)
        {
            button.onClick.AddListener(Play);
        }
        foreach (Button button in m_ExitButtons)
        {
            button.onClick.AddListener(Exit);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitResolutionSettings();
        if (GameManager.m_GameOver && GameManager.m_Winner)
        {
            m_OptionsPanel.gameObject.SetActive(false);
            m_BlueWinsPanel.gameObject.SetActive(true);
            m_RedWinsPanel.gameObject.SetActive(false);
            m_MainMenu.gameObject.SetActive(false);
            m_PlayLightsParent.gameObject.SetActive(true);
            m_ExitLightsParent.gameObject.SetActive(true);
            m_OptionsLightsParent.localPosition = new Vector3(-0.5f, -2.637f, -12.69f);
        }
        else if (GameManager.m_GameOver && !GameManager.m_Winner)
        {
            m_OptionsPanel.gameObject.SetActive(false);
            m_BlueWinsPanel.gameObject.SetActive(false);
            m_RedWinsPanel.gameObject.SetActive(true);
            m_MainMenu.gameObject.SetActive(false);
            m_PlayLightsParent.gameObject.SetActive(true);
            m_ExitLightsParent.gameObject.SetActive(true);
            m_OptionsLightsParent.localPosition = new Vector3(-0.5f, -2.637f, -12.69f);
        }
        else
        {
            m_OptionsPanel.gameObject.SetActive(false);
            m_BlueWinsPanel.gameObject.SetActive(false);
            m_RedWinsPanel.gameObject.SetActive(false);
            m_MainMenu.gameObject.SetActive(true);
            m_PlayLightsParent.gameObject.SetActive(true);
            m_ExitLightsParent.gameObject.SetActive(true);
            m_OptionsLightsParent.localPosition = new Vector3(-0.5f, -2.637f, -12.69f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (m_RemainingTimeToChangeLights <= 0)
        {
            ChangeLights();
            m_RemainingTimeToChangeLights = m_TimeToChangeLights;
        }
        else
        {
            m_RemainingTimeToChangeLights -= dt;
        }
    }

    private void ChangeLights()
    {
        for (int i = 0; i < m_Lighs.Count; i++)
        {
            if (CheckActivationOfLight(i))
            {
                m_Lighs[i].gameObject.SetActive(true);
                m_PlayLighs[i].gameObject.SetActive(true);
                m_OptionsLighs[i].gameObject.SetActive(true);
                m_ExitLighs[i].gameObject.SetActive(true);
            }
            else
            {
                m_Lighs[i].gameObject.SetActive(false);
                m_PlayLighs[i].gameObject.SetActive(false);
                m_OptionsLighs[i].gameObject.SetActive(false);
                m_ExitLighs[i].gameObject.SetActive(false);
            }
        }
        m_FirstLight++;
        if (m_FirstLight >= m_Lighs.Count)
        {
            m_FirstLight = 0;
        }
        m_LastLight++;
        if (m_LastLight >= m_Lighs.Count)
        {
            m_LastLight = 0;
        }
    }

    private bool CheckActivationOfLight(int index)
    {
        bool res = false;
        if (index >= m_FirstLight && index <= m_LastLight)
        {
            res = true;
        }
        else if (m_FirstLight > m_LastLight && (index >= m_FirstLight || index <= m_LastLight))
        {
            res = true;
        }
        return res;
    }


    private void Play()
    {
        GameManager.m_GameOver = false;
        SceneManager.LoadScene(1);
    }

    private void OptionsPanel()
    {
        m_OptionsPanel.gameObject.SetActive(true);
        m_BlueWinsPanel.gameObject.SetActive(false);
        m_RedWinsPanel.gameObject.SetActive(false);
        m_MainMenu.gameObject.SetActive(false);
        m_OptionsLightsParent.localPosition = new Vector3(-0.5f, -10f, 17f);
        m_PlayLightsParent.gameObject.SetActive(false);
        m_ExitLightsParent.gameObject.SetActive(false);
    }

    private void MainMenuPanel()
    {
        m_OptionsPanel.gameObject.SetActive(false);
        m_BlueWinsPanel.gameObject.SetActive(false);
        m_RedWinsPanel.gameObject.SetActive(false);
        m_MainMenu.gameObject.SetActive(true);
        m_OptionsLightsParent.localPosition = new Vector3(-0.5f, -2.637f, -12.69f);
        m_PlayLightsParent.gameObject.SetActive(true);
        m_ExitLightsParent.gameObject.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void InitResolutionSettings()
    {
        // Only resolution 1920x1080
        Resolution res = new Resolution
        {
            width = 1920,
            height = 1080
        };

        m_Resolutions = new Resolution[] { res };
        m_ResolutionDropdown.ClearOptions();

        List<string> options = new List<string> { "1920 x 1080" };
        m_ResolutionDropdown.AddOptions(options);
        m_ResolutionDropdown.value = 0;
        m_ResolutionDropdown.RefreshShownValue();

        m_FullscreenToggle.isOn = Screen.fullScreen;

        m_ResolutionDropdown.onValueChanged.AddListener(SetResolution);
        m_FullscreenToggle.onValueChanged.AddListener(SetFullscreen);


    }




    public void SetResolution(int resolutionIndex)
    {
        if (m_Resolutions == null || resolutionIndex < 0 || resolutionIndex >= m_Resolutions.Length)
            return;

        Resolution resolution = m_Resolutions[resolutionIndex];
        bool isFullscreen = m_FullscreenToggle.isOn;
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
