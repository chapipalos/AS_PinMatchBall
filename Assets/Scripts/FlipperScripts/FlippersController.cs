using UnityEngine;

public class FlippersController : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.J;

    public bool m_Side;

    private Rigidbody m_Rigidbody;

    public bool m_Player;

    private Collider m_Collider;

    public GameObject m_Freeze;

    public GameObject m_Broken;

    public GameObject m_Splash;

    public AudioManager m_AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Player = transform.parent.CompareTag("Player1");
        m_Collider = m_Rigidbody.GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Freeze comprobation
        if (GameManager.m_BlueFrozenPowerUp && !m_Player || GameManager.m_RedFrozenPowerUp && m_Player)
        {
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            m_Freeze.SetActive(true);
            return;
        }
        else
        {
            m_Freeze.SetActive(false);
        }

        if (GameManager.m_RedStunnedPowerUpActive && m_Player && GameManager.m_StunnedSide == m_Side)
        {
            m_Broken.SetActive(true);

        }
        else if (GameManager.m_BlueStunnedPowerUpActive && !m_Player && GameManager.m_StunnedSide == m_Side)
        {
            m_Broken.SetActive(true);
        }
        else if (GameManager.m_RedStunnedPowerUpActive && m_Player && GameManager.m_StunnedSide == m_Side)
        {
            m_Broken.SetActive(true);
        }
        else if (GameManager.m_BlueStunnedPowerUpActive && !m_Player && GameManager.m_StunnedSide == m_Side)
        {
            m_Broken.SetActive(true);
        }
        else
        {
            m_Broken.SetActive(false);
        }

        // Ghost ball comprobation
        if (GameManager.m_GhostBall)
        {
            m_Collider.isTrigger = true;
        }
        else
        {
            m_Collider.isTrigger = false;
        }

        HingeJoint hingeJoint = GetComponent<HingeJoint>();
        JointMotor motor = GetComponent<HingeJoint>().motor;

        if (m_Side)
        {
            // Stunned comprobation for right flipper
            if (GameManager.m_RedStunnedPowerUpActive && m_Player && GameManager.m_StunnedSide)
            {
                return;
            }
            if (GameManager.m_BlueStunnedPowerUpActive && !m_Player && GameManager.m_StunnedSide)
            {
                return;
            }

            if (Input.GetKey(keyCode))
            {
                hingeJoint = GetComponent<HingeJoint>();
                motor.targetVelocity = 1000;

                GetComponent<HingeJoint>().motor = motor;
            }
            else
            {
                motor.targetVelocity = -1000;

                GetComponent<HingeJoint>().motor = motor;
            }
        }
        else
        {
            // Stunned comprobation for left flipper
            if (GameManager.m_RedStunnedPowerUpActive && m_Player && !GameManager.m_StunnedSide)
            {
                return;
            }
            if (GameManager.m_BlueStunnedPowerUpActive && !m_Player && !GameManager.m_StunnedSide)
            {
                return;
            }

            if (Input.GetKey(keyCode))
            {
                hingeJoint = GetComponent<HingeJoint>();
                motor.targetVelocity = -1000;

                GetComponent<HingeJoint>().motor = motor;
            }
            else
            {
                motor.targetVelocity = 1000;

                GetComponent<HingeJoint>().motor = motor;
            }
        }
    }

    private void Update()
    {
        if (GameManager.m_BlueFrozenPowerUp && !m_Player || GameManager.m_RedFrozenPowerUp && m_Player)
        {
            return;
        }
        if (Input.GetKeyDown(keyCode))
        {
            m_Splash.GetComponent<SplashController>().SplashAction();
            m_AudioManager.PlaySFX(m_AudioManager.m_TriggerSound);
        }
    }
}
