using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDControlB : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        parts = GetComponentsInChildren<Transform>();

        // Default orientations, child last positions and rotation speeds
        defaultOrientations = new Dictionary<Transform, Quaternion>();
        childlastPositions = new Dictionary<Transform, Vector3>();
        rotationSpeeds = new Dictionary<Transform, Vector3>();
        foreach (Transform part in parts)
        {
            defaultOrientations.Add(part, part.rotation);
            childlastPositions.Add(part, part.childCount > 0 ? part.GetChild(0).position : Vector3.zero);
            rotationSpeeds.Add(part, Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform part in parts)
        {
            // Reaction to movement
            if (isReactionToMovementEnabled && part.childCount > 0)
            {
                rotationSpeeds[part] += 20.0f * Vector3.Cross(part.forward, childlastPositions[part] - part.position);
                part.Rotate(Time.deltaTime * rotationSpeeds[part], Space.World);
                rotationSpeeds[part] -= 0.3f * Time.deltaTime * rotationSpeeds[part];
            }
            // Current direction
            Vector3 currentDirection =
              isPureCCD ?
              (part.childCount == 0 ? part.forward : (parts[parts.Length - 1].position - part.position)) :
              part.forward;
            Vector3 goalDirection = target.transform.position - part.position;
            // Goal orientation
            Quaternion goalOrientation =
              CalculateActivation() ?
              Quaternion.FromToRotation(currentDirection, goalDirection) * part.rotation :
              defaultOrientations[part];
            // New orientation
            Quaternion newOrientation = Quaternion.Slerp(part.rotation, goalOrientation, (CalculateActivation() ? searchSpeed : idleSpeed) * Time.deltaTime);
            // Update
            if (part.parent == null ||
                  transform.GetComponent<AngleLimit>() == null ||
                  Vector3.Angle(part.parent.transform.forward, newOrientation * Vector3.forward) < transform.GetComponent<AngleLimit>().value)
            {
                part.rotation = newOrientation;
            }
            // Child last position storage
            childlastPositions[part] = part.childCount > 0 ? part.GetChild(0).position : Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            GameManager.m_RobotActivate = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.m_RedRobot = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.m_BlueRobot = true;
        }
    }

    private bool CalculateActivation()
    {
        return (transform.parent.CompareTag("RedRobot") && GameManager.m_RedRobot && GameManager.m_RedRobotSearching) || (transform.parent.CompareTag("BlueRobot") && GameManager.m_BlueRobot && GameManager.m_BlueRobotSearching);
    }

    // ATTRIBUTES

    public bool isPureCCD = true;  // Pure CCD switch
    public bool isReactionToMovementEnabled = true;  // Switch controlling reaction to movement
    public float searchSpeed = 1.0f;  // Search speed
    public float idleSpeed = 1.0f;  // Idle speed
    public GameObject target = null;  // Target
    public Transform[] parts;                                // Object collection


    private Dictionary<Transform, Quaternion> defaultOrientations; // Default orientations
    private Dictionary<Transform, Vector3> childlastPositions;  // Child last positions
    private Dictionary<Transform, Vector3> rotationSpeeds;      // RotationSpeeds
}
