using UnityEngine;

public class ResetBallController : MonoBehaviour
{
    public Transform m_ResetBall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            other.attachedRigidbody.linearVelocity = Vector3.zero;
            Invoke("ResetBall", 2f);
        }
    }

    private void ResetBall(Transform ball)
    {
        ball.position = m_ResetBall.position;
    }
}
