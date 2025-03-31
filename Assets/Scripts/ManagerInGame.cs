using UnityEngine;
using UnityEngine.UI;

public class ManagerInGame : MonoBehaviour
{
    public Image BrighPanelInGame;
    public float savedBrightness;
    private void Start()
    {
         savedBrightness = PlayerPrefs.GetFloat("brightness", 0.5f);
        BrighPanelInGame.color = new Color(BrighPanelInGame.color.r, BrighPanelInGame.color.g, BrighPanelInGame.color.b, savedBrightness);
    }
}