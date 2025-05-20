using System.Collections;
using UnityEngine;

public class ResetBallController : MonoBehaviour
{
    public Transform m_ResetBall;

    public GameObject m_Parent;

    public GameObject m_ResetBallnPrefab;
    private GameObject m_ResetBallEffect;
    private ParticleSystem[] m_ResetBallParticleSystem;

    private void Awake()
    {
        m_ResetBallEffect = GameObject.Instantiate(m_ResetBallnPrefab);
        m_ResetBallParticleSystem = m_ResetBallEffect.GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BALL"))
        {
            foreach (ParticleSystem ps in m_ResetBallParticleSystem)
            {
                ps.transform.position = other.transform.position;
                ps.Play();
            }
            other.attachedRigidbody.linearVelocity = Vector3.zero;
            other.gameObject.SetActive(false);
            StartCoroutine(ResetBall(other.transform));

            if (m_Parent.tag == "BlueRobot")
            {
                GameManager.m_BlueRobot = false;
            }
            else if (m_Parent.tag == "RedRobot")
            {
                GameManager.m_RedRobot = false;
            }
        }
    }

    private IEnumerator ResetBall(Transform ball)
    {
        if (transform.parent.parent.parent.parent.tag == "BlueRobot")
        {
            GameManager.m_BlueRobotSearching = false;
        }
        else
        {
            GameManager.m_RedRobotSearching = false;
        }
        yield return new WaitForSeconds(2f);
        ball.GetComponent<Rigidbody>().mass = 0.3f;
        ball.position = m_ResetBall.position;
        ball.gameObject.SetActive(true);
        foreach (ParticleSystem ps in m_ResetBallParticleSystem)
        {
            ps.transform.position = ball.transform.position;
            ps.Play();
        }
    }
}
