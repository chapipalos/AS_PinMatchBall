using UnityEngine;

public class RobotController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_RedRobot && this.CompareTag("RedRobot"))
        {
            if (other.CompareTag("BALL") && GameManager.m_RobotActivate)
            {
                GameManager.m_RedRobotSearching = true;
            }
        }
        else if(GameManager.m_BlueRobot && this.CompareTag("BlueRobot"))
        {
            if (other.CompareTag("BALL") && GameManager.m_RobotActivate)
            {
                GameManager.m_BlueRobotSearching = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (GameManager.m_RedRobot && this.CompareTag("RedRobot"))
        {
            if (other.CompareTag("BALL") && GameManager.m_RobotActivate)
            {
                GameManager.m_RedRobotSearching = false;
            }
        }
        else if (GameManager.m_BlueRobot && this.CompareTag("BlueRobot"))
        {
            if (other.CompareTag("BALL") && GameManager.m_RobotActivate)
            {
                GameManager.m_BlueRobotSearching = false;
            }
        }
    }
}
