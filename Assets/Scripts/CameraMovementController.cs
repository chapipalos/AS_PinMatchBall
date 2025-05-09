using UnityEngine;

public class TopDownFollow : MonoBehaviour
{
    public Transform m_Target;              // El objeto que la c�mara seguir� (por ejemplo, la bola)
    public Vector2 m_FollowAmount = new Vector2(2f, 2f); // Cu�nto se puede mover la c�mara desde su posici�n base
    public float m_SmoothSpeed = 0.1f;      // Qu� tan r�pido se mueve la c�mara hacia la nueva posici�n

    private Vector3 initialPosition;      // Posici�n inicial de la c�mara

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (m_Target == null) return;

        // Calcula el desplazamiento horizontal (x, z) en relaci�n al objetivo
        Vector3 offset = new Vector3(
            Mathf.Clamp(m_Target.position.x - initialPosition.x, -m_FollowAmount.x, m_FollowAmount.x),
            0,
            Mathf.Clamp(m_Target.position.z - initialPosition.z, -m_FollowAmount.y, m_FollowAmount.y)
        );

        // Aplica suavizado al movimiento de la c�mara
        Vector3 desiredPosition = initialPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * Time.deltaTime);
    }
}
