using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class BarrierController : MonoBehaviour
{
    public bool m_Activate = false;

    [Header("Spring values")]
    public float m_Force = 100f;
    public float m_Cushioning = 10f;
    public float m_TargetPosition;

    private HingeJoint m_HingeJoint;

    void Start()
    {
        m_HingeJoint = GetComponent<HingeJoint>();
        m_HingeJoint.useSpring = true;
    }

    void Update()
    {
        JointSpring spring = m_HingeJoint.spring;
        if (m_Activate)
        {
            spring.spring = m_Force;
            spring.damper = m_Cushioning;
            spring.targetPosition = m_TargetPosition;
        }
        m_HingeJoint.spring = spring;
    }
}