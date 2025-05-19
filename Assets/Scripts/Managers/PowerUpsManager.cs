using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public float m_TimeFrozen;
    public float m_RemainingTimeFrozen;

    public float m_TimeStunned;
    public float m_RemainingTimeStunned;

    public float m_TimeGhostBall;
    public float m_RemainingTimeGhostBall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_RemainingTimeFrozen = m_TimeFrozen;
        m_RemainingTimeStunned = m_TimeStunned;
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
        if (GameManager.m_FrozenPowerUp)
        {
            if (m_RemainingTimeFrozen <= 0.0f)
            {
                m_RemainingTimeFrozen = m_TimeFrozen;
                GameManager.m_FrozenPowerUp = false;
            }
            else
            {
                m_RemainingTimeFrozen -= dt;
            }
        }
    }

    private void StunnedPowerUp(float dt)
    {
        if (GameManager.m_StunnedPowerUp && GameManager.m_StunnedPowerUpActive)
        {
            if (m_RemainingTimeStunned <= 0.0f)
            {
                m_RemainingTimeStunned = m_TimeStunned;
                GameManager.m_StunnedPowerUp = false;
                GameManager.m_StunnedPowerUpActive = false;
            }
            else
            {
                m_RemainingTimeStunned -= dt;
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
