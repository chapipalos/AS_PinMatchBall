using UnityEngine;

public class WinManager : MonoBehaviour
{
    public ScoreManager redScoreManager;
    public ScoreManager blueScoreManager;

    public GameObject redWinsText;
    public GameObject blueWinsText;

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
            blueWinsText.SetActive(true);
        else if (winner == "Blue" && blueWinsText != null)
        redWinsText.SetActive(true);
    }
}
