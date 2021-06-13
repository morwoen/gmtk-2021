using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  private GameObject HUD;
  private GameObject panel;
  private Image fadeToBlack;

  public bool isOpen {
    get;
    private set;
  } = false;
  private bool levelSelect = false;

  private void Start() {
#if UNITY_WEBGL
    // Remove the Quit button in WebGL
    transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
#endif
  }

  void Awake() {
    fadeToBlack = transform.GetChild(0).GetComponent<Image>();
    panel = transform.GetChild(1).gameObject;
    // Hide the win panel
    panel.transform.GetChild(3).gameObject.SetActive(false);
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
    fadeToBlack.color = new Color(0, 0, 0, isOpen ? fadeToBlack.color.a : 0);
    Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
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

  public void WinTheGame() {
    isOpen = true;
    UpdateUI();
    panel.transform.GetChild(1).gameObject.SetActive(false);
    panel.transform.GetChild(2).gameObject.SetActive(false);
    panel.transform.GetChild(3).gameObject.SetActive(true);
  }

  public void Restart() {
    // Intended to be 0, restart the whole game
    SceneManager.LoadScene(0);
  }
}
