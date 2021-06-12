using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InduvidualController : MonoBehaviour
{
    private bool isSelected;

    //camera
    public Camera mainCamera;

    //gameObject components
    Animator animate;
    CharacterController controller;

    //static vectors
    static Vector3 OFFSET = new Vector3(0, 1.5f, 0);
    static Vector3 ZERO = new Vector3(0, 0, 0);
    static Vector3 ORIGIN = new Vector3(0, 10, 0);

    //movement variables
    Vector3 stickPosition;
    Quaternion look;
    Vector3 target;
    float yTarget;
    float gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 1.7f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 cameraVector = mainCamera.transform.forward;
        cameraVector.y = 0.0f;
        stickPosition = Quaternion.FromToRotation(Vector3.forward, cameraVector) * direction;
        if (stickPosition != ZERO) look.SetLookRotation(stickPosition);
        float speed = Mathf.Clamp(stickPosition.magnitude, 0, 1);

        if (yTarget > -50 && !controller.isGrounded)
        {
            yTarget -= 1 * 9.8f * Time.deltaTime;
        }
        if (controller.isGrounded)
            yTarget = -0.5f;

        //jumping code
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            yTarget = 10;
        }

        //Movement code
        if (speed > 0.1f)
        {
            //target = speed*8*Time.deltaTime*stickPosition;
            target = speed * 8 * Time.deltaTime * transform.forward;
            transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * 15);
        }
        else
        {
            target = ZERO;
        }

        //animate.SetFloat("Speed", speed);
        target.y = yTarget * gravity * Time.deltaTime;

        controller.Move(target);
        if (transform.position.y < -20)
        {
            transform.position = ORIGIN;
        }
    }

    public void Activate()
    {
        isSelected = true;
    }
    public void Deactivate()
    {
        isSelected = false;
    }
}
