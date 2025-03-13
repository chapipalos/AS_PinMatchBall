using UnityEngine;

public class BallDetector : MonoBehaviour
{

    public Beat m_Beat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Beat = GetComponentInChildren<Beat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float fuerzaExpulsion = 10f; // Ajusta la fuerza de expulsi�n

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null && other.tag == "BALL") // Verifica si el objeto tiene un Rigidbody2D
        {
            m_Beat.activarExpansion = true;


            Vector3 direccionExpulsion = (other.transform.position - transform.position).normalized;
            rb.linearVelocity = Vector3.zero; // Opcional: Reinicia la velocidad antes de expulsar
            rb.AddForce(direccionExpulsion * fuerzaExpulsion, ForceMode.Impulse);
        }
    }


}
