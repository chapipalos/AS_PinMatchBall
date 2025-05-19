using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    public static bool m_PlayerOwner; // true for blue palyer / false for red player

    public static bool m_FrozenPowerUp = false;
    public static bool m_StunnedPowerUp = false;
    public static bool m_StunnedPowerUpActive = false;
    public static bool m_StunnedSide = false; // true for right flippers / false for left ones
    public static bool m_GhostBall = false;

    public static bool m_FanRotating = false;

    public static bool m_RedRobotSearching = false;
    public static bool m_BlueRobotSearching = false;
    public static bool m_RedRobot = false;
    public static bool m_BlueRobot = false;
}
