using System.Collections;
using UnityEngine;

public class ResetBallController : MonoBehaviour
{
    public Transform m_ResetBall;

    private IEnumerator m_ResetBallCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            other.attachedRigidbody.linearVelocity = Vector3.zero;
            m_ResetBallCoroutine = ResetBall(other.transform);
        }
    }

    private IEnumerator ResetBall(Transform ball)
    {
        yield return new WaitForSeconds(2f);
        ball.position = m_ResetBall.position;
    }
}
