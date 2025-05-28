using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Roulette : MonoBehaviour
{
    [Header("Torque Settings")]
    public float m_MinRotatePower = 1000f;
    public float m_MaxRotatePower = 2000f;

    [Header("Stop Settings")]
    public float m_MinStopPower = 5f;
    public float m_MaxStopPower = 15f;


    [Header("Final Settings")]
    [SerializeField] private float RotatePower;
    [SerializeField] private float StopPower;

    [Header("General Settings")]
    private float m_Time = 0f;
    public float m_TimeToScale;

    private Rigidbody m_Rigidbody;

    private int m_InRotate;

    public GameObject m_Rulette;

    public GameObject m_Ball;

    public Transform m_RespawnPointRed;
    public Transform m_RespawnPointBlue;

    public Material m_MaterialPlayerRed;
    public Material m_MaterialPlayerBlue;

    public bool m_ReescaletRoulette;
    public Vector3 m_MinScale;
    public Vector3 m_MaxScale;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        RotatePower = Random.Range(m_MinRotatePower, m_MaxRotatePower);
        StopPower = Random.Range(m_MinStopPower, m_MaxStopPower);
        m_ReescaletRoulette = false;
        transform.parent.localScale = m_MaxScale;
    }

    float t;
    private void Update()
    {
        if (m_Rigidbody.angularVelocity.y > 0)
        {
            Vector3 av = m_Rigidbody.angularVelocity;
            av.y -= StopPower * Time.deltaTime;
            av.y = Mathf.Clamp(av.y, 0, 1440 * Mathf.Deg2Rad);
            m_Rigidbody.angularVelocity = av;
        }

        if (Mathf.Approximately(m_Rigidbody.angularVelocity.y, 0f) && m_InRotate == 1)
        {
            t += Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();
                m_InRotate = 0;
                t = 0;
            }
        }
        float factor = m_Time / m_TimeToScale;
        ReescalteRoulette(factor);
        RouletteDeescalted();
    }

    public void Rotate()
    {
        if (m_InRotate == 0)
        {
            m_Rigidbody.AddTorque(Vector3.up * RotatePower, ForceMode.Acceleration);
            m_InRotate = 1;
        }
    }

    public void GetReward()
    {
        float rotY = transform.eulerAngles.y;
        rotY = rotY % 360; // Asegura que esté entre 0 y 360

        if (rotY >= 0 && rotY < 90)
        {
            SnapRotation(45);
            BlueWins();
        }
        else if (rotY >= 90 && rotY < 180)
        {
            SnapRotation(135);
            RedWins();
        }
        else if (rotY >= 180 && rotY < 270)
        {
            SnapRotation(225);
            BlueWins();
        }
        else // 270 <= rotY < 360
        {
            SnapRotation(315);
            RedWins();
        }
    }

    private void SnapRotation(float y)
    {
        transform.eulerAngles = new Vector3(0, y, 0);
        m_ReescaletRoulette = true;
    }

    private void DeactivateRoulette()
    {
        m_Rulette.SetActive(false);
        m_Ball.SetActive(true);
    }

    public void BlueWins()
    {
        m_Ball.GetComponent<MeshRenderer>().material = m_MaterialPlayerBlue;
        m_Ball.transform.position = m_RespawnPointBlue.position;
        GameManager.m_PlayerOwner = false;
    }

    public void RedWins()
    {
        m_Ball.GetComponent<MeshRenderer>().material = m_MaterialPlayerRed;
        m_Ball.transform.position = m_RespawnPointRed.position;
        GameManager.m_PlayerOwner = true;
    }

    private void ReescalteRoulette(float factor)
    {
        if (!m_ReescaletRoulette)
        {
            return;
        }
        m_Time += Time.deltaTime;
        transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, m_MinScale, factor);
    }

    private void RouletteDeescalted()
    {
        if(transform.parent.localScale == m_MinScale)
        {
            m_ReescaletRoulette = false;
            DeactivateRoulette();
        }
    }
}
