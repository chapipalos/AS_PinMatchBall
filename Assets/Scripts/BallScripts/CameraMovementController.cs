using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform m_Target;              // El objeto que la cámara seguirá (por ejemplo, la bola)
    public Vector2 m_FollowAmount = new Vector2(2f, 2f); // Cuánto se puede mover la cámara desde su posición base
    public float m_SmoothSpeed = 0.1f;      // Qué tan rápido se mueve la cámara hacia la nueva posición

    private Vector3 initialPosition;      // Posición inicial de la cámara

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (m_Target == null) return;

        // Calcula el desplazamiento horizontal (x, z) en relación al objetivo
        Vector3 offset = new Vector3(
            Mathf.Clamp(m_Target.position.x - initialPosition.x, -m_FollowAmount.x, m_FollowAmount.x),
            0,
            Mathf.Clamp(m_Target.position.z - initialPosition.z, -m_FollowAmount.y, m_FollowAmount.y)
        );

        // Aplica suavizado al movimiento de la cámara
        Vector3 desiredPosition = initialPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * Time.deltaTime);
    }
}
