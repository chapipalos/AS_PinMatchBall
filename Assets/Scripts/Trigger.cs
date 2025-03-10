using UnityEngine;

public class Trigger : MonoBehaviour
{
    
    public float rotationAngle = 45f; // Maximum rotation angle when holding Space
    public float rotationSpeed = 100f; // Speed of rotation
    private Quaternion originalRotation;
    private bool rotating = false;

    public Transform rightFlipper;
    public Transform leftFlipper;
    private Quaternion orgRightRotation;
    private Quaternion orgLeftRotation;

    private bool leftRotating;
    private bool rightRotating;

    private bool player;

    void Start()
    {
        //originalRotation = transform.rotation; // Store the original rotation
        orgLeftRotation = leftFlipper.rotation;
        orgRightRotation = rightFlipper.rotation;

        if(CompareTag("Player1"))
        {
            player = true;
        }
        else if(CompareTag("Player2"))
        {
            player = false;
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    rotating = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    rotating = false;
        //}

        Inputs();
        
        RotateFlipper(leftRotating, leftFlipper, orgLeftRotation, false);
        RotateFlipper(rightRotating, rightFlipper, orgRightRotation, true);
    }

    private void FixedUpdate()
    {
        
    }

    private void Inputs()
    {
        if (player)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rightRotating = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                rightRotating = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                leftRotating = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                leftRotating = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rightRotating = true;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rightRotating = false;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                leftRotating = true;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                leftRotating = false;
            }
        }
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

    void RotateFlipper(bool rotate, Transform flipper, Quaternion orRot, bool flip)
    {
        if (rotate)
        {
            Quaternion targetRotation;
            // Rotate towards the target rotation angle
            if (flip)
            {
                targetRotation = Quaternion.Euler(0, rotationAngle, 0) * orRot;
            }
            else
            {
                targetRotation = Quaternion.Euler(0, -rotationAngle, 0) * orRot;
            }
            flipper.rotation = Quaternion.RotateTowards(flipper.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Return to the original rotation smoothly
            flipper.rotation = Quaternion.RotateTowards(flipper.rotation, orRot, rotationSpeed * Time.deltaTime);
        }
    }
}
