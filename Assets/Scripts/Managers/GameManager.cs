using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    public static bool m_PlayerOwner; // true for blue palyer / false for red player

    [Header("Freeze variables")]
    public static bool m_RedFrozenPowerUp = false;
    public static bool m_BlueFrozenPowerUp = false;

    [Header("Stunned variables")]
    public static bool m_RedStunnedPowerUp = false;
    public static bool m_BlueStunnedPowerUp = false;
    public static bool m_RedStunnedPowerUpActive = false;
    public static bool m_BlueStunnedPowerUpActive = false;
    public static bool m_StunnedSide = false; // true for right flippers / false for left ones

    [Header("Ghost ball variables")]
    public static bool m_GhostBall = false;

    [Header("Splash variables")]
    public static bool m_RedSplash = false;
    public static bool m_BlueSplash = false;

    public static bool m_UpperFanActive = false;
    public static bool m_BottomFanActive = false;

    [Header("IK Robot variables")]
    public static bool m_RedRobotSearching = false;
    public static bool m_BlueRobotSearching = false;
    public static bool m_RedRobot = false;
    public static bool m_BlueRobot = false;
}
