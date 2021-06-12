using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //DEBUG
    public Canvas UI;
    private GameObject debugNote;

    //player characters
    protected GameObject fireCharacter;
    protected GameObject iceCharacter;
    public enum Characters {Fire,Ice};
    Characters selected;
    GameObject currentCharacter;

    //camera
    public Camera mainCamera;

    //gameObject components
    Animator animate;
    CharacterController controller;

    //static vectors
    static Vector3 OFFSET = new Vector3(0, 1.5f, 0);
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
        selected = Characters.Fire;
        fireCharacter = transform.GetChild(0).gameObject;
        iceCharacter = transform.GetChild(1).gameObject;
        currentCharacter = fireCharacter;
        controller = currentCharacter.GetComponent<CharacterController>();
        gravity = 1.7f;
        debugNote = UI.transform.Find("DebugText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            Swap();
            yTarget = 0;
            return;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 cameraVector = mainCamera.transform.forward;
        cameraVector.y = 0.0f;
        stickPosition = Quaternion.FromToRotation(Vector3.forward, cameraVector) * direction;
        if (stickPosition != Vector3.zero) look.SetLookRotation(stickPosition);
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
            target = speed * 8 * Time.deltaTime * currentCharacter.transform.forward;
            currentCharacter.transform.rotation = Quaternion.Lerp(currentCharacter.transform.rotation, look, Time.deltaTime * 15);
        }
        else
        {
            target = Vector3.zero;
        }

        //animate.SetFloat("Speed", speed);
        target.y = yTarget * gravity *Time.deltaTime;

        controller.Move(target);
        if (currentCharacter.transform.position.y < -20)
        {
            currentCharacter.transform.position = ORIGIN;
        }
        debugNote.GetComponent<Text>().text = "yTarget: " + yTarget+"\n"+"isGrounded: " + controller.isGrounded;
    }

    private void Swap()
    {
        currentCharacter.GetComponent<CharacterController>().enabled = false;
        currentCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (selected == Characters.Fire)
        {
            selected = Characters.Ice;
            currentCharacter = iceCharacter;
        }
        else
        {
            selected = Characters.Fire;
            currentCharacter = fireCharacter;
        }
        controller = currentCharacter.GetComponent<CharacterController>();
        currentCharacter.GetComponent<CharacterController>().enabled = true;
    }

    //camera stuff
    void LateUpdate()
    {
        Vector3 characterOffset = currentCharacter.transform.position + OFFSET;
        lookDir = characterOffset - mainCamera.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();

        cameraTarget = characterOffset + currentCharacter.transform.up * 1.5f - lookDir * 6;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraTarget, Time.deltaTime * 7);

        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, Quaternion.LookRotation(currentCharacter.transform.position - mainCamera.transform.position), Time.deltaTime * 7);
    }
}
