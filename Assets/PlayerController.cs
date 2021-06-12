using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //DEBUG
    public Canvas UI;
    private GameObject debugNote;

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

    //camera variables
    Vector3 cameraTarget;
    Vector3 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = 0.7f;
        debugNote = UI.transform.Find("DebugText").gameObject;
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

        //jumping code
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            yTarget = 10;
        }
        if (controller.isGrounded)
            yTarget = 0;

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
        target.y = yTarget * gravity *Time.deltaTime;

        controller.Move(target);
        if (transform.position.y < -20)
        {
            transform.position = ORIGIN;
        }
        debugNote.GetComponent<Text>().text = "yTarget: " + yTarget+"\n"+"isGrounded: " + controller.isGrounded;
    }

    //camera stuff
    void LateUpdate()
    {
        Vector3 characterOffset = transform.position + OFFSET;
        lookDir = characterOffset - mainCamera.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();

        cameraTarget = characterOffset + transform.up * 1.5f - lookDir * 6;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraTarget, Time.deltaTime * 7);

        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.LookRotation(transform.position - mainCamera.transform.position), Time.deltaTime * 7);
    }
}
