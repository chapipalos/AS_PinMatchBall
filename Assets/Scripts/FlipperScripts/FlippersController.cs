using UnityEngine;

public class FlippersController : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.J;

    public bool m_Side;

    private Rigidbody m_Rigidbody;

    private bool m_Player;

    private Collider m_Collider;

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
        if (GameManager.m_FrozenPowerUp && m_Player != GameManager.m_PlayerOwner)
        {
            //m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            //GetComponentInChildren<GameObject>().SetActive(true);
            return;
        }
        //else
        //{
        //    m_Rigidbody.constraints = RigidbodyConstraints.None;
        //    GetComponentInChildren<GameObject>().SetActive(false);
        //}

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
            if (GameManager.m_StunnedPowerUpActive && m_Player != GameManager.m_PlayerOwner && GameManager.m_StunnedSide)
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
            if (GameManager.m_StunnedPowerUpActive && m_Player != GameManager.m_PlayerOwner && !GameManager.m_StunnedSide)
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
}
