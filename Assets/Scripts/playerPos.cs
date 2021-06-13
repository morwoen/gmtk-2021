using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerPos : MonoBehaviour
{   //This script need to be attached to both players 
    // Start is called before the first frame update 
  private gameMaster gm;
  void Start() {
    MovePlayer();
  }

  public void MovePlayer() {
    gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameMaster>();
    transform.position = gm.lastCheckpointPos;
  }
}
