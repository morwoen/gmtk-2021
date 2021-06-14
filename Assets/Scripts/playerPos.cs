using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerPos : MonoBehaviour
{
  // This script need to be attached to the parent object of both players

  private gameMaster gm;
  void Start() {
    MovePlayer();
  }

  public void MovePlayer() {
    gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameMaster>();

    CharacterController enabledOne = null;

    var playerCC = transform.GetChild(0).GetComponent<CharacterController>();
    if (playerCC.enabled) {
      enabledOne = playerCC;
      playerCC.enabled = false;
    }
    playerCC = transform.GetChild(1).GetComponent<CharacterController>();
    if (playerCC.enabled) {
      enabledOne = playerCC;
      playerCC.enabled = false;
    }

    transform.position = gm.lastCheckpointPos;
    if (enabledOne != null) {
      enabledOne.enabled = true;
    }
  }
}
