using Unity.VisualScripting;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [Header("Swing Settings")]
    public float m_maxAngle = 5f;             // Maximum tilt angle in X
    public float m_swingSpeed = 2f;           // Speed of swinging motion

    [Header("Parent Tracking")]
    public Transform m_parent;                // Reference to parent transform
    public float m_rotationSpeed = 100f;      // Rotation speed when facing movement direction

    private Quaternion m_initialRotation;     // Original rotation at start
    private Vector3 m_previousParentPosition; // Parent's position from previous frame

    void Start()
    {
        m_initialRotation = transform.rotation;

        if (m_parent == null)
            m_parent = transform.parent;

        m_previousParentPosition = m_parent.position;
    }

    void Update()
    {
        // Balance on X axis (swinging motion)
        float angle = Mathf.Sin(Time.time * m_swingSpeed) * m_maxAngle;
        transform.rotation = m_initialRotation * Quaternion.Euler(angle, 0f, 0f);

        // Calculate parent's movement direction
        Vector3 movementDirection = m_parent.position - m_previousParentPosition;
        movementDirection.y = 0f; // Ignore vertical movement

        if (movementDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            Quaternion currentRotation = transform.rotation;

            Quaternion yOnlyRotation = Quaternion.Euler(
                currentRotation.eulerAngles.x,
                targetRotation.eulerAngles.y,
                currentRotation.eulerAngles.z
            );

            transform.rotation = Quaternion.Lerp(currentRotation, yOnlyRotation, Time.deltaTime * m_rotationSpeed);
        }

        m_previousParentPosition = m_parent.position;
    }


}
