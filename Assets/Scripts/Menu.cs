using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  private GameObject HUD;
  private GameObject panel;

  private bool isOpen = false;
  private bool levelSelect = false;

  private void Start() {
    #if UNITY_WEBGL
    // Remove the Quit button in WebGL
    panel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    #endif
  }

  void Awake() {
    panel = transform.GetChild(0).gameObject;
    isOpen = panel.activeSelf;
    var thisCanvas = GetComponent<Canvas>();
    foreach (var c in FindObjectsOfType<Canvas>()) {
      if (!thisCanvas.Equals(c)) {
        HUD = c.gameObject;
        break;
      }
    }

    UpdateUI();

    // The ToggleLevelSelect would make it false.
    // This is so that we setup the start state of all GOs.
    levelSelect = true;
    ToggleLevelSelect();
  }

  void Update() {
    if (Input.GetButtonDown("Cancel")) {
      Play();
    }
  }

  void UpdateUI() {
    panel.SetActive(isOpen);
    HUD.SetActive(!isOpen);
    Time.timeScale = isOpen ? 0 : 1;
  }

  public void Play() {
    isOpen = !isOpen;
    UpdateUI();
  }

  public void Quit() {
    Application.Quit();
  }

  public void ToggleLevelSelect() {
    levelSelect = !levelSelect;
    panel.transform.GetChild(1).gameObject.SetActive(!levelSelect);
    panel.transform.GetChild(2).gameObject.SetActive(levelSelect);
  }

  public void LoadLevel(int level) {
    SceneManager.LoadScene(level);
  }
}
