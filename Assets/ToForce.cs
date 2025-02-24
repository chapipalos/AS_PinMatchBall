using Unity.VisualScripting;
using UnityEngine;

public class ToForce : MonoBehaviour
{
    public Rigidbody rb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            rb.AddForce(4 * (transform.position - collision.transform.position), ForceMode.Impulse);
        }
    }
}
