using Unity.VisualScripting;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [Header("Swing Settings")]
    public float m_maxAngle = 5f;
    public float m_swingSpeed = 2f;

    [Header("Parent Tracking")]
    public Transform m_parent;
    public float m_rotationSpeed = 5f; // Reducido para mejor control

    private Quaternion m_initialRotation;
    private Vector3 m_previousParentPosition;
    private float m_currentSwingAngle;

    void Start()
    {
        m_initialRotation = transform.rotation;

        if (m_parent == null)
            m_parent = transform.parent;

        m_previousParentPosition = m_parent.position;
        m_currentSwingAngle = 0f;
    }

    void Update()
    {
        // Calcular dirección de movimiento primero
        Vector3 movementDirection = m_parent.position - m_previousParentPosition;
        movementDirection.y = 0f;

        // Solo rotar si hay movimiento significativo
        if (movementDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * m_rotationSpeed
            );
        }

        // Aplicar swing después de la rotación
        m_currentSwingAngle = Mathf.Sin(Time.time * m_swingSpeed) * m_maxAngle;
        transform.rotation *= Quaternion.Euler(m_currentSwingAngle, 0f, 0f);

        m_previousParentPosition = m_parent.position;
    }
}
