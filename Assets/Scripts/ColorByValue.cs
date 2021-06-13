using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorByValue : MonoBehaviour
{
    public Color lowerColor;
    public Color midColor;
    public Color highColor;
    public Color dangerColor;

    public Image handle;
    public Image fill;

    private float lastHealthValue;
    private Color currentColor;

    private void Awake()
    {
        lastHealthValue = 0.5f;
    }

    private void Update()
    {
        if(lastHealthValue <= 0.2f)
        {
            float timeWave = (Mathf.Sin(10*Time.time)+1)/2;
            Color blendColor = Color.Lerp(currentColor, dangerColor, Mathf.Round(timeWave));
            handle.color = blendColor;
            fill.color = blendColor;
        }
    }

    public void UpdateColors(float value)
    {
        lastHealthValue = value;
        Color blendColor;

        if (value < .5)
        {
            blendColor = Color.Lerp(lowerColor, midColor, value*2);
        }
        else
        {
            blendColor = Color.Lerp(midColor, highColor, (value - .5f)*2f);
        }


        currentColor = blendColor;
        handle.color = blendColor;
        fill.color = blendColor;
    }
}
