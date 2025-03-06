using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    public float rotationAngle = 45f; // Maximum rotation angle when holding Space
    public float rotationSpeed = 100f; // Speed of rotation
    private Quaternion originalRotation;
    private bool rotating = false;

    void Start()
    {
        originalRotation = transform.rotation; // Store the original rotation
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rotating = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rotating = false;
        }

        RotateObject();
    }

    void RotateObject()
    {
        if (rotating)
        {
            // Rotate towards the target rotation angle
            Quaternion targetRotation = Quaternion.Euler(0, rotationAngle, 0) * originalRotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Return to the original rotation smoothly
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
