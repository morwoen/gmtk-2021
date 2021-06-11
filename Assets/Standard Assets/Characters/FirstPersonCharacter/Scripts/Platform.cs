using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 2;
    public List<Vector3> waypointsLocal = new List<Vector3>();
    Vector3 startingPoint;
    int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + (waypointsLocal [current].normalized * speed * Time.fixedDeltaTime));

        if (Vector3.Distance(transform.position, startingPoint + waypointsLocal [current]) < 1) {
            current++;
            if (waypointsLocal.Count <= current) current = 0;
        }
    }

    private void OnCollisionStay (Collision collision) {
        CharacterController controller = collision.transform.GetComponent<CharacterController>();
        if (controller != null) 
            controller.Move(waypointsLocal [current].normalized * speed * Time.fixedDeltaTime);
        if (!collision.gameObject.isStatic)
            collision.transform.parent = transform;
    }

    private void OnCollisionExit (Collision collision) {
        if(!collision.gameObject.isStatic)
            collision.transform.parent = null;
    }
}
