using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public ScoreManager redScoreManager;
    public ScoreManager blueScoreManager;

    public GameObject m_Camera;
    public GameObject m_Ball;

    void Update()
    {
        if (GameManager.m_GameOver)
        {
            return;
        }
        if (redScoreManager.m_Score >= 1)
        {
            GameOver(false);
        }
        else if (blueScoreManager.m_Score >= 1)
        {
            GameOver(true);
        }
    }

    void GameOver(bool winner)
    {
        //Time.timeScale = 0;
        m_Ball.SetActive(false);

        Debug.Log("GameOver");

        m_Camera.GetComponent<CameraMovementController>().m_InitialPosition = m_Camera.transform.position;
        m_Camera.GetComponent<CameraMovementController>().m_InitialRotation = m_Camera.transform.rotation;
        m_Camera.GetComponent<CameraMovementController>().m_Time = 0f;
        GameManager.m_GameOver = true;
        GameManager.m_Winner = !winner;
    }
}
