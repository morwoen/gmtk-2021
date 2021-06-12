using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogo : MonoBehaviour
{
  public RectTransform[] points;
  public RectTransform logo;
  public float speed = 1;

  private float currentTime = 0;
  private Vector3 originalPosition;
  private Vector3 originalScale;
  private Quaternion originalRotation;
  private RectTransform target;
  private int nextPointsIndex = 0;

  void Awake() {
    Setup();
  }

  void Update() {
    currentTime = Mathf.Clamp01(currentTime + speed * Time.unscaledDeltaTime);
    logo.localScale = Vector3.Lerp(originalScale, target.localScale, currentTime);
    logo.rotation = Quaternion.Lerp(originalRotation, target.rotation, currentTime);
    logo.localPosition = Vector3.Lerp(originalPosition, target.localPosition, currentTime);

    if (currentTime == 1) {
      currentTime = 0;
      Setup();
    }
  }

  void Setup() {
    originalPosition = logo.localPosition;
    originalScale = logo.localScale;
    originalRotation = logo.rotation;
    target = points[nextPointsIndex];
    nextPointsIndex = (nextPointsIndex + 1) % points.Length;
  }
}
