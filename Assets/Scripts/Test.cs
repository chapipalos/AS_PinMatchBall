using UnityEngine;

public class Test : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.J;

    public bool m_Side;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HingeJoint hingeJoint = GetComponent<HingeJoint>();
        JointMotor motor = hingeJoint.motor;

        if (m_Side)
        {
            if (Input.GetKey(keyCode))
            {
                hingeJoint = GetComponent<HingeJoint>();
                motor.targetVelocity = 1000;

                hingeJoint.motor = motor;
            }
            else
            {
                motor.targetVelocity = -1000;

                hingeJoint.motor = motor;

            }
        }
        else
        {
            if (Input.GetKey(keyCode))
            {
                hingeJoint = GetComponent<HingeJoint>();
                motor.targetVelocity = -1000;

                hingeJoint.motor = motor;
            }
            else
            {
                motor.targetVelocity = 1000;

                hingeJoint.motor = motor;

            }
        }
        
    }
}
