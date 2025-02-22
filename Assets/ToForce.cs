using Unity.VisualScripting;
using UnityEngine;

public class ToForce : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;
    public Collider cl;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, 0,ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);
           
        }
   
    }
}
