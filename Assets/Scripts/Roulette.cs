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
    public float m_Time = 0.1f;

    private Rigidbody rbody;

    private int inRotate;

    public GameObject m_Rulette;

    public GameObject m_Ball;

    public Transform m_RespawnPointRed;
    public Transform m_RespawnPointBlue;

    public Material m_MaterialPlayerRed;
    public Material m_MaterialPlayerBlue;



    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        RotatePower = Random.Range(m_MinRotatePower, m_MaxRotatePower);
        StopPower = Random.Range(m_MinStopPower, m_MaxStopPower);
    }

    float t;
    private void Update()
    {
        if (rbody.angularVelocity.y > 0)
        {
            Vector3 av = rbody.angularVelocity;
            av.y -= StopPower * Time.deltaTime;
            av.y = Mathf.Clamp(av.y, 0, 1440 * Mathf.Deg2Rad);
            rbody.angularVelocity = av;
        }

        if (Mathf.Approximately(rbody.angularVelocity.y, 0f) && inRotate == 1)
        {
            t += Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();
                inRotate = 0;
                t = 0;
            }
        }
    }

    public void Rotate()
    {
        if (inRotate == 0)
        {
            rbody.AddTorque(Vector3.up * RotatePower, ForceMode.Acceleration);
            inRotate = 1;
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
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(m_Time);
        m_Rulette.SetActive(false);
    }

    public void BlueWins()
    {
        m_Ball.GetComponent<MeshRenderer>().material = m_MaterialPlayerBlue;
        m_Ball.transform.position = m_RespawnPointBlue.position;
        m_Ball.SetActive(true);
        GameManager.m_PlayerOwner = false;
    }

    public void RedWins()
    {
        m_Ball.GetComponent<MeshRenderer>().material = m_MaterialPlayerRed;
        m_Ball.transform.position = m_RespawnPointRed.position;
        m_Ball.SetActive(true);
        GameManager.m_PlayerOwner = true;
    }
}
