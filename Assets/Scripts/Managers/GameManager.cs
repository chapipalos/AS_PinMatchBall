using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    public static bool m_PlayerOwner; // true for blue palyer / false for red player

    public static bool m_FrozenPowerUp;
    public static bool m_StunnedPowerUp;
    public static bool m_StunnedPowerUpActive;
    public static bool m_StunnedSide; // true for right flippers / false for left ones
    public static bool m_GhostBall;

    public static bool m_FanRotating;
}
