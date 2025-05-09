using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.1f;

    private Image img;
    private int index;
    private float timer;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            index = (index + 1) % frames.Length;
            img.sprite = frames[index];
            timer = 0f;
        }
    }
}
