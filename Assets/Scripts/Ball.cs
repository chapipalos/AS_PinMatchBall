using UnityEngine;

public class Ball : MonoBehaviour
{
    public Material m_MaterialPlayer1;
    public Material m_MaterialPlayer2;

    private MeshRenderer m_MeshRenderer;


    public bool m_IsPlayer1;
    public bool m_IsPlayer2;
    
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player1"))
        {
            m_MeshRenderer.material = m_MaterialPlayer1;
            m_IsPlayer1 = true;
            m_IsPlayer2 = false;
        }
        else if(collision.gameObject.CompareTag("Player2"))
        {
            m_MeshRenderer.material = m_MaterialPlayer2;
            m_IsPlayer2 = true;
            m_IsPlayer1 = false;
        }

    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
