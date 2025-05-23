using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public float m_TimeFrozen;
    public float m_RedRemainingTimeFrozen;
    public float m_BlueRemainingTimeFrozen;

    public float m_TimeStunned;
    public float m_RedRemainingTimeStunned;
    public float m_BlueRemainingTimeStunned;

    public float m_TimeGhostBall;
    public float m_RemainingTimeGhostBall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_RedRemainingTimeFrozen = m_TimeFrozen;
        m_BlueRemainingTimeFrozen = m_TimeFrozen;

        m_RedRemainingTimeStunned = m_TimeStunned;
        m_BlueRemainingTimeStunned = m_TimeStunned;

        m_RemainingTimeGhostBall = m_TimeGhostBall;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        FreezePowerUp(dt);
        StunnedPowerUp(dt);
        GhostBallPowerUp(dt);
    }

    private void FreezePowerUp(float dt)
    {
        if (GameManager.m_RedFrozenPowerUp)
        {
            if (m_RedRemainingTimeFrozen <= 0.0f)
            {
                m_RedRemainingTimeFrozen = m_TimeFrozen;
                GameManager.m_RedFrozenPowerUp = false;
            }
            else
            {
                m_RedRemainingTimeFrozen -= dt;
            }
        }
        if (GameManager.m_BlueFrozenPowerUp)
        {
            if (m_BlueRemainingTimeFrozen <= 0.0f)
            {
                m_BlueRemainingTimeFrozen = m_TimeFrozen;
                GameManager.m_BlueFrozenPowerUp = false;
            }
            else
            {
                m_BlueRemainingTimeFrozen -= dt;
            }
        }
    }

    private void StunnedPowerUp(float dt)
    {
        if (GameManager.m_RedStunnedPowerUpActive)
        {
            GameManager.m_RedStunnedPowerUp = false;
            if (m_RedRemainingTimeStunned <= 0.0f)
            {
                m_RedRemainingTimeStunned = m_TimeStunned;
                GameManager.m_RedStunnedPowerUpActive = false;
            }
            else
            {
                m_RedRemainingTimeStunned -= dt;
            }
        }
        if (GameManager.m_BlueStunnedPowerUpActive)
        {
            GameManager.m_BlueStunnedPowerUp = false;
            if (m_BlueRemainingTimeStunned <= 0.0f)
            {
                m_BlueRemainingTimeStunned = m_TimeStunned;
                GameManager.m_BlueStunnedPowerUpActive = false;
            }
            else
            {
                m_BlueRemainingTimeStunned -= dt;
            }
        }
    }

    private void GhostBallPowerUp(float dt)
    {
        if (GameManager.m_GhostBall)
        {
            if (m_RemainingTimeGhostBall <= 0.0f)
            {
                m_RemainingTimeGhostBall = m_TimeGhostBall;
                GameManager.m_GhostBall = false;
            }
            else
            {
                m_RemainingTimeGhostBall -= dt;
            }
        }
    }
}