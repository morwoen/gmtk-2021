using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectionCollision : MonoBehaviour
{
  public int damage = 3;

  private void OnParticleCollision(GameObject other) {
    TargetHealth th = other.GetComponent<TargetHealth>();
    if (th != null) {
      th.TakeDamage(damage);
    }
  }
}
