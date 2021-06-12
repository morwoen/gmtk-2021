using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectionCollision : MonoBehaviour
{
  public int damage = 3;

  private void OnParticleCollision(GameObject other) {
    targetHealth th = other.GetComponent<targetHealth>();
    if (th != null) {
      th.TakeDamage(damage);
    }
  }
}
