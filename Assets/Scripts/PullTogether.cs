using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTogether : MonoBehaviour
{
  public Collider object1;
  public Collider object2;

  public float maxVelocity = 5;
  public float maxAcceleration = 1;

  private LineRenderer lineRenderer;
  private float velocity = 0;
  private float acceleration = 0;

  private Rigidbody object2rb;

  void Awake() {
    lineRenderer = gameObject.GetComponent<LineRenderer>();
    if (!object2.CompareTag("MovablePlatform") && object1.CompareTag("MovablePlatform")) {
      var tmp = object1;
      object1 = object2;
      object2 = tmp;
    }
    object2rb = object2.GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    // Update the line renderer
    lineRenderer.SetPosition(0, object1.transform.position);
    lineRenderer.SetPosition(1, object2.transform.position);

    // Check if the platforms collider. Done this way as they are kinematic and
    // we don't want to setup events between the platforms and the "Roper"
    if (object1.bounds.Intersects(object2.bounds)) {
      Destroy(gameObject);
      return;
    }

    // Increase acceleration as time goes along to gradually speed up
    acceleration = Mathf.Clamp(acceleration + Time.fixedDeltaTime * 0.8f, 0, maxAcceleration);
    // Apply acceleration to velocity
    velocity = Mathf.Clamp(velocity + Time.fixedDeltaTime * acceleration, 0, maxVelocity);
    // Clac the direction between the 2 platforms
    var dir = (object1.transform.position - object2.transform.position).normalized;
    // Move the platform
    object2rb.MovePosition(object2.transform.position + (dir * velocity));
  }
}
