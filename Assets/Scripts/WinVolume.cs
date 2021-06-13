using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
  HashSet<string> playersInside;

  private void Awake() {
    playersInside = new HashSet<string>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
      playersInside.Add(other.gameObject.name);
      if (playersInside.Count == 2) {
        FindObjectOfType<Menu>().WinTheGame();
      }
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
      playersInside.Remove(other.gameObject.name);
    }
  }
}
