﻿using UnityEngine;
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

    public Light fanLight;
    public Color newColor = Color.magenta;

    private Color originalLightColor;

    private bool m_Effect;

    public bool m_FanType;
  public   AudioManager m_AudioManager;


    private void Awake()
    {
        m_WindEffect = Instantiate(m_WindPrefab);
        m_WindEffect.transform.position = m_Blades.transform.position;
        m_WindParticleSystem = m_WindEffect.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        m_CurrentSpeed = 0f;
        if (fanLight != null)
        {
            originalLightColor = fanLight.color;
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if ((!m_FanType && GameManager.m_UpperFanActive) || (m_FanType && GameManager.m_BottomFanActive))
        {
            m_CurrentSpeed += rotationSpeed * dt;
            m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, rotationSpeed);
            m_Blades.Rotate(Vector3.forward, m_CurrentSpeed);

            if (!m_Effect)
            {
                m_AudioManager.PlaySFX(m_AudioManager.m_FanSound);
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
    }

    private void OnTriggerStay(Collider other)
    {
        if ((!m_FanType && GameManager.m_UpperFanActive) || (m_FanType && GameManager.m_BottomFanActive) && other.CompareTag("BALL"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            other.attachedRigidbody?.AddForce(-transform.right * (1 / distance) * m_FanForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((!m_FanType && GameManager.m_UpperFanActive) || (m_FanType && GameManager.m_BottomFanActive) && other.CompareTag("BALL"))
        {
            ChangeLightColorTemporarily();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((!m_FanType && GameManager.m_UpperFanActive) || (m_FanType && GameManager.m_BottomFanActive) && other.CompareTag("BALL"))
        {
            RevertLightColor();
        }
    }

    private void ChangeLightColorTemporarily()
    {
        CancelInvoke(nameof(RevertLightColor));

        if (fanLight != null)
        {
            fanLight.color = newColor;
            fanLight.intensity = 1000;
        }
    }

    private void RevertLightColor()
    {
        if (fanLight != null)
        {
            fanLight.color = originalLightColor;
            fanLight.intensity = 10;
        }
    }
}
