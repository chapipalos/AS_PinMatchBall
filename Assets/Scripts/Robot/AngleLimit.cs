using UnityEngine;

public class AngleLimit : MonoBehaviour
{
    public Material m_NormalMaterial;
    public Material m_DisableMaterial;

    private MeshRenderer m_MeshRenderer;

    public GameObject m_Parent;

    public float value = 180.0f;

    private void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (m_Parent.tag == "RedRobot" && GameManager.m_RedRobot)
        {
            m_MeshRenderer.material = m_NormalMaterial;
        }
        else if (m_Parent.tag == "BlueRobot" && GameManager.m_BlueRobot)
        {
            m_MeshRenderer.material = m_NormalMaterial;
        }
        else
        {
            m_MeshRenderer.material = m_DisableMaterial;
        }
    }
}
