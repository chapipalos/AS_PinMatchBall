using TMPro;
using UnityEngine;

public class TextAnimationScript : MonoBehaviour
{
    public float bounceSpeed = 6f;
    public float bounceAmount = 8f;
    public float colorSpeed = 1f;
    public float scalePulseSpeed = 2f;
    public float scaleAmount = 0.1f;


    private TMP_Text textMesh;
    private TMP_TextInfo textInfo;
    private Vector3 originalScale;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        
        float pulse = 1 + Mathf.Sin(Time.time * scalePulseSpeed) * scaleAmount;
        transform.localScale = originalScale * pulse;

//Animation for each letter
        textMesh.ForceMeshUpdate();
        textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            //Raimbow Color 
            Color color = Color.HSVToRGB(Mathf.Repeat(Time.time * colorSpeed + i * 0.05f, 1f), 1f, 1f);

            int index = textInfo.characterInfo[i].vertexIndex;
            int matIndex = textInfo.characterInfo[i].materialReferenceIndex;
            var verts = textInfo.meshInfo[matIndex].vertices;
            Color32[] vertexColors = textInfo.meshInfo[matIndex].colors32;

            // Boncing
            Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * bounceSpeed + i) * bounceAmount, 0);
            verts[index + 0] += offset;
            verts[index + 1] += offset;
            verts[index + 2] += offset;
            verts[index + 3] += offset;

            vertexColors[index + 0] = color;
            vertexColors[index + 1] = color;
            vertexColors[index + 2] = color;
            vertexColors[index + 3] = color;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textMesh.UpdateGeometry(meshInfo.mesh, i);
        }

        textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}

