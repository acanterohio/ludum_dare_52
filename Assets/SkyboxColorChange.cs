using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChange : MonoBehaviour
{
    [SerializeField] private Color originalColor;
    [SerializeField] private Color newColor;

    public void ChangeColor()
    {
        RenderSettings.skybox.SetColor("_SkyColor", newColor);
        // RenderSettings.
    }
}
