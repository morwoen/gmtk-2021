using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorByValue : MonoBehaviour
{
  public Color lowerColor;
  public Color highColor;

  public Image handle;
  public Image fill;

  public void UpdateColors(float value) {
    var newColor = Color.Lerp(lowerColor, highColor, value);
    handle.color = newColor;
    fill.color = newColor;
  }
}
