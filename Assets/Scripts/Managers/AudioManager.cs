using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Sources ------")]
    [SerializeField] AudioSource m_MusicSource;
    [SerializeField] AudioSource m_SFXSource;

    [Header("------ Audio Clips ------")]
    [Header("Background Music")]

    public AudioClip m_BackgoundMusic;

    [Header("Power Up Sounds")]

    public AudioClip m_FreezePowerUpSound;
    public AudioClip m_GhostBallPowerUpSound;
    public AudioClip m_ShieldPowerUpSound;
    public AudioClip m_SpikeBallPowerUpSound;
    public AudioClip m_BlooperPowerUpSound;

    [Header("Map Sounds")]

    public AudioClip m_BallWallBounceSound;
    public AudioClip m_BumperSound;
    public AudioClip m_TriggerSound;
    public AudioClip m_SpeedZoneSound;
    public AudioClip m_FanSound;
    public AudioClip m_GoalSound;
    public AudioClip m_RespawnSound;
    public AudioClip m_StunSound;

    [Header("Menu")]
    public AudioClip m_Background2;
    public AudioClip m_Click;

    private void Start()
    {
        m_MusicSource.clip = m_BackgoundMusic;
        m_MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        m_MusicSource.PlayOneShot(clip);
    }

}