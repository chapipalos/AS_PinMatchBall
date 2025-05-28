using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public ScoreManager m_RedScoreManager;
    public ScoreManager m_BlueScoreManager;

    private bool m_Winner;

    public GameObject m_Camera;
    public GameObject m_Ball;

    void Update()
    {
        if (GameManager.m_GameOver)
        {
            return;
        }
        if (m_RedScoreManager.m_Score >= 9)
        {
            m_Ball.GetComponent<Ball>().enabled = false;
            m_Winner = false;
            Invoke("GameOver", 1f);
        }
        else if (m_BlueScoreManager.m_Score >= 9)
        {
            m_Ball.GetComponent<Ball>().enabled = false;
            m_Winner = true;
            Invoke("GameOver", 1f);
        }
    }

    void GameOver()
    {
        m_Camera.GetComponent<CameraMovementController>().m_InitialPosition = m_Camera.transform.position;
        m_Camera.GetComponent<CameraMovementController>().m_InitialRotation = m_Camera.transform.rotation;
        m_Camera.GetComponent<CameraMovementController>().m_Time = 0f;

        GameManager.m_GameOver = true;
        GameManager.m_Winner = !m_Winner;
    }
}
