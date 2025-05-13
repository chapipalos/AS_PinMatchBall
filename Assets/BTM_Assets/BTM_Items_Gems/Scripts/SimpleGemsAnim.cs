using UnityEngine;
using System.Collections;

namespace Benjathemaker
{
    public class SimpleGemsAnim : MonoBehaviour
    {
        public bool isRotating = false;
        public float rotationSpeed = 90f; // Degrees per second

        public Transform m_Base;

        void Update()
        {
            if (isRotating)
            {
                transform.RotateAround(m_Base.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
    }
}

