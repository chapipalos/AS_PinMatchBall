using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public ScoreManager redScoreManager;
    public ScoreManager blueScoreManager;

    public GameObject redWinsText;
    public GameObject blueWinsText;
    public GameObject MainMenub;
    public bool init=false;

    void Update()
    {
        if (redScoreManager.m_Score >= 9)
        {
            GameOver("Red");
        }
        else if (blueScoreManager.m_Score >= 9)
        {
            GameOver("Blue");
        }
    }

    void GameOver(string winner)
    {
        Time.timeScale = 0;

        if (winner == "Red" && redWinsText != null)
        {
            blueWinsText.SetActive(true);
            MainMenub.SetActive(true);
        }
        else if (winner == "Blue" && blueWinsText != null)
        {
            redWinsText.SetActive(true);
            MainMenub.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        init = true;
    }
    public void PlayAgain()
    {
        blueScoreManager.m_Score = 0;
        redScoreManager.m_Score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

     
    }
}
