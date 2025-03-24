using UnityEngine;

public class Wall : MonoBehaviour
{
    public float pushForce = 10f; // Force applied to the ball

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody; // Get the ball's Rigidbody

        if (rb != null) // Ensure the object has a Rigidbody
        {
            // Calculate the push direction based on the wall's normal
            Vector3 pushDirection = collision.contacts[0].normal;

            // Apply force to the ball in the direction of the normal
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
