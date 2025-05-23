using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Sources ------")]
    [SerializeField] AudioSource m_MusicSource;
    [SerializeField] AudioSource m_SFXSource;

    [Header("------ Audio Clips ------")]

    public AudioClip m_BackgoundMusic;

    public AudioClip m_FreezePowerUp;
    public AudioClip m_GhostBallPowerUp;
    public AudioClip m_ShieldPowerUp;
    public AudioClip m_SpikeBallPowerUp;
    public AudioClip m_BlooperPowerUp;

    private void Start()
    {
        m_MusicSource.clip = m_BackgoundMusic;
        m_MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }


}