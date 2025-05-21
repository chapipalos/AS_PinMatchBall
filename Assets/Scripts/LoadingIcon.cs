using UnityEngine;

public class LoadingIcon : MonoBehaviour
{
    public float rotationSpeed = 180f; // Grados por segundo

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
