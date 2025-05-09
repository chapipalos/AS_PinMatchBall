using UnityEngine;

public class PropultionZone : MonoBehaviour
{
    public float forceAmount = 10f; // Fuerza en la dirección X



    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && other.tag == "BALL")
        {
            // Aplica una fuerza constante solo en la dirección X
            rb.AddForce(transform.right * forceAmount, ForceMode.Force);
        }
    }
}
