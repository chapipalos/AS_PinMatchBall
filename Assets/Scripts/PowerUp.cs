using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public List<Mesh> m_Meshes;
    public List<Material> m_Materials;

    private MeshFilter m_MeshFilter;
    private MeshRenderer m_MeshRendererPU;

    public GameObject m_Mesh;
    public Transform m_FreezePosition;
    private Quaternion m_MeshTransform;

    public int m_Option;

    private void Awake()
    {
        m_MeshFilter = m_Mesh.GetComponent<MeshFilter>();
        m_MeshRendererPU = m_Mesh.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        m_MeshTransform = new Quaternion(0, 0, 0, 0);
        int op = Random.Range(0, m_Meshes.Count);
        m_Option = op;
        switch (op)
        {
            case 0:
                m_Mesh.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
                m_Mesh.transform.rotation = m_FreezePosition.rotation;
                m_Mesh.transform.position = transform.position + new Vector3(0f, 1.65f, 0f);
                tag = "FreezePU";
                break;
            case 1:
                m_Mesh.transform.localScale = new Vector3(1f, 1f, 1f);
                m_Mesh.transform.rotation = m_MeshTransform;
                m_Mesh.transform.position = transform.position + new Vector3(0f, 1.5f, 0f);
                tag = "SpikePU";
                break;
            case 2:
                m_Mesh.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
                m_Mesh.transform.rotation = m_MeshTransform;
                m_Mesh.transform.position = transform.position + new Vector3(0f, 0.65f, 0f);
                tag = "GhostPU";
                break;
            case 3:
                m_Mesh.transform.localScale = new Vector3(0.78f, 0.78f, 0.78f);
                m_Mesh.transform.rotation = m_MeshTransform;
                m_Mesh.transform.position = transform.position + new Vector3(0f, 1.65f, 0f);
                tag = "ShieldPU";
                break;
        }
        m_MeshRendererPU.material = m_Materials[op];
        m_MeshFilter.mesh = m_Meshes[op];
    }
}
