using UnityEngine;
using System.Collections.Generic;

public class FanController : MonoBehaviour
{
    public float rotationSpeed = 90f;
    private float m_CurrentSpeed;

    public float m_FanForce;

    public Transform m_Blades;

    public GameObject m_WindPrefab;
    private GameObject m_WindEffect;
    private ParticleSystem m_WindParticleSystem;

    public GameObject m_WindHitEffectPrefab;
    private GameObject m_WindHitEffect;
    private ParticleSystem[] m_WindHitParticles;

    private bool m_Effect;

    private HashSet<Collider> m_AlreadyTriggered = new HashSet<Collider>();

    private void Awake()
    {
 
        m_WindEffect = Instantiate(m_WindPrefab);
        m_WindEffect.transform.position = m_Blades.transform.position;
        m_WindParticleSystem = m_WindEffect.GetComponent<ParticleSystem>();

  
        m_WindHitEffect = Instantiate(m_WindHitEffectPrefab);
        m_WindHitEffect.SetActive(false);
        m_WindHitParticles = m_WindHitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        m_CurrentSpeed = 0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (GameManager.m_FanRotating)
        {
            m_CurrentSpeed += rotationSpeed * dt;
            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, rotationSpeed);
            m_Blades.Rotate(Vector3.forward, m_CurrentSpeed);

            if (!m_Effect)
            {
                m_WindParticleSystem.Play();
                m_Effect = true;
            }
        }
        else
        {
            m_CurrentSpeed -= rotationSpeed * dt;
            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, rotationSpeed);
            m_Blades.Rotate(Vector3.forward, m_CurrentSpeed);

            if (m_Effect)
            {
                m_WindParticleSystem.Stop();
                m_Effect = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.J)) GameManager.m_FanRotating = true;
        if (Input.GetKeyDown(KeyCode.K)) GameManager.m_FanRotating = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.m_FanRotating && other.CompareTag("BALL"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            other.attachedRigidbody?.AddForce(-transform.right * (1 / distance) * m_FanForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.m_FanRotating && other.CompareTag("BALL") && !m_AlreadyTriggered.Contains(other))
        {
            m_AlreadyTriggered.Add(other);

            if (m_WindHitEffect != null)
            {
               
                m_WindHitEffect.SetActive(true);

                foreach (var ps in m_WindHitParticles)
                {
                    ps.transform.position = other.transform.position;
                    ps.Play();
                }

                
                Invoke(nameof(DeactivateWindHitEffect), 2f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_AlreadyTriggered.Remove(other);
    }

    private void DeactivateWindHitEffect()
    {
        foreach (var ps in m_WindHitParticles)
        {
            ps.Stop();
        }
        m_WindHitEffect.SetActive(false);
    }
}

