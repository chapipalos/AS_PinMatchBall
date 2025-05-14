using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] public List<Sprite> m_Numbers;

    public int m_Score = 0;

    public GameObject m_Number;

    private SpriteRenderer m_Renderer;
    void Start()
    {
        //m_Numbers[m_Score].SetActive(true);
        m_Renderer = m_Number.GetComponent<SpriteRenderer>();
        m_Renderer.sprite = m_Numbers[m_Score];
    }

    public void IncreaseScore()
    {
        m_Score++;
        m_Score = Mathf.Clamp(m_Score, 0, 9);
        m_Renderer.sprite = m_Numbers[m_Score];
    }


}
