using UnityEngine;

public class MoveSticksManager : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveSpeed = 0.5f;
    public float rotationSpeed = 30f;
    public float targetAngle = 45f;

    private Vector3 targetPosition;
    private float currentRotation = 0f;
    private int rotationDirection = 1;
    private bool isMoving = false;
    public bool MoveIf = false;

    public void Start()
    {
        if (CompareTag("StickLeft"))
        {
            targetPosition = transform.position + Vector3.left * moveDistance;
            rotationDirection = 1;
        }
        else if (CompareTag("StickRight"))
        {
            targetPosition = transform.position + Vector3.right * moveDistance;
            rotationDirection = -1;
        }
    }

    void Update()
    {
        if (MoveIf && !isMoving)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float stepRotation = rotationSpeed * Time.deltaTime;
            if (currentRotation < targetAngle)
            {
                transform.Rotate(Vector3.forward * stepRotation * rotationDirection);
                currentRotation += stepRotation;
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f && currentRotation >= targetAngle)
            {
                isMoving = false;
            }
        }
    }
}


