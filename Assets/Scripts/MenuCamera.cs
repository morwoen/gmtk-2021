using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour
{
  public float stayOnCharacter = 5f;
  public float fadeToBlackSpeed = 1f;
  
  private Image fadeToBlack;
  private CinemachineFreeLook cinemachine;
  private Transform player1;
  private Transform player2;
  private Menu menu;

  private Transform focusedPlayer;
  private Transform previousPlayer;

  private float waitOnCharacter = 0f;
  private float fadeToBlackTimer = 0f;
  private float waitOnBlackTimer = 0f;
  private float fadeToWhiteTimer = 0f;

  void Awake() {
    fadeToBlack = transform.GetChild(0).GetComponent<Image>();
    cinemachine = FindObjectOfType<CinemachineFreeLook>();
    menu = GetComponent<Menu>();
    var pc = FindObjectOfType<PlayerController>().transform;
    player1 = pc.GetChild(0);
    player2 = pc.GetChild(1);

    focusedPlayer = player2;
  }

  void Update() {
    if (menu.isOpen) {
      if (!previousPlayer) {
        previousPlayer = cinemachine.Follow;
        focusedPlayer = cinemachine.Follow;
      }
      cinemachine.Follow = focusedPlayer;
      cinemachine.LookAt = focusedPlayer;

      cinemachine.m_XAxis.m_InputAxisName = "";
      cinemachine.m_YAxis.m_InputAxisName = "";

      cinemachine.m_XAxis.m_InputAxisValue = 0.03f;
      cinemachine.m_YAxis.Value = 0.8f;

      if (waitOnCharacter < 1) {
        waitOnCharacter = Mathf.Clamp01(waitOnCharacter + stayOnCharacter * Time.unscaledDeltaTime);
      } else if (fadeToBlackTimer < 1) {
        fadeToBlackTimer = Mathf.Clamp01(fadeToBlackTimer + fadeToBlackSpeed * Time.unscaledDeltaTime);
        fadeToBlack.color = new Color(0, 0, 0, fadeToBlackTimer);
        if (fadeToBlackTimer == 1) {
          if (focusedPlayer == player1) {
            focusedPlayer = player2;
          } else {
            focusedPlayer = player1;
          }
        }
      } else if (waitOnBlackTimer < 1) {
        waitOnBlackTimer = Mathf.Clamp01(waitOnBlackTimer + fadeToBlackSpeed * Time.unscaledDeltaTime);
      } else if (fadeToWhiteTimer < 1) {
        fadeToWhiteTimer = Mathf.Clamp01(fadeToWhiteTimer + fadeToBlackSpeed * Time.unscaledDeltaTime);
        fadeToBlack.color = new Color(0, 0, 0, 1 - fadeToWhiteTimer);
      } else {
        fadeToWhiteTimer = 0;
        fadeToBlackTimer = 0;
        waitOnCharacter = 0;
        waitOnBlackTimer = 0;
      }
    } else {
      cinemachine.m_XAxis.m_InputAxisName = "Mouse X";
      cinemachine.m_YAxis.m_InputAxisName = "Mouse Y";
      waitOnCharacter = 0;
      fadeToBlackTimer = 0;
      fadeToWhiteTimer = 0;
      waitOnBlackTimer = 0;

      if (previousPlayer) {
        cinemachine.Follow = previousPlayer;
        cinemachine.LookAt = previousPlayer;
        previousPlayer = null;
      }
    }
  }
}
