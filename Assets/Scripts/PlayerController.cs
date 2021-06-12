using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // UI
    public Canvas UI;
    GameObject thermometer;

    //DEBUG
    private GameObject debugNote;

    //player characters
    protected GameObject fireCharacter;
    protected GameObject iceCharacter;
    public enum Characters {Fire,Ice};
    Characters selected;
    GameObject currentCharacter;

    //collision
    RaycastHit standingObject;
    bool doesRaycastHit;

    //health
    int health;
    int othersHealth;
    
    //camera
    public Camera mainCamera;

    //gameObject components
    Animator animate;
    CharacterController controller;

    //static vectors
    static Vector3 OFFSET = new Vector3(0, 1.5f, 0);
    static Vector3 ORIGIN = new Vector3(0, 10, 0);

    //const variables
    const int HP_MAX = 3;

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
        health = HP_MAX;
        othersHealth = HP_MAX;
        selected = Characters.Fire;
        fireCharacter = transform.GetChild(0).gameObject;
        iceCharacter = transform.GetChild(1).gameObject;
        currentCharacter = fireCharacter;
        controller = currentCharacter.GetComponent<CharacterController>();
        gravity = 1.7f;
        debugNote = UI.transform.Find("DebugText").gameObject;
        thermometer = UI.transform.Find("HealthSlider").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Hurt();
        doesRaycastHit = false; //DEBUG
        if (Input.GetButtonDown("Swap"))
        {
            Swap();
            yTarget = 0;
            return;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Sets control info for camera direction and player facing direction
        Vector3 cameraVector = mainCamera.transform.forward;
        cameraVector.y = 0.0f;
        stickPosition = Quaternion.FromToRotation(Vector3.forward, cameraVector) * direction;
        if (stickPosition != Vector3.zero) look.SetLookRotation(stickPosition);
        float speed = Mathf.Clamp(stickPosition.magnitude, 0, 1);


        if (yTarget > -50 && !controller.isGrounded)
        {
            yTarget -= 1 * 9.8f * Time.deltaTime; // falling speed
        }
        if (controller.isGrounded)
        {
            yTarget = -0.5f; // sets the desired y position slightly below the ground to make sure the player stays grounded
            doesRaycastHit = Physics.Raycast(currentCharacter.transform.position + Vector3.up, Vector3.down, out standingObject, 1000, ~LayerMask.GetMask("Player")); // maybe this should have an else for standingObject
            if (doesRaycastHit && standingObject.collider.material.bounciness > 0.1f)
                yTarget = standingObject.collider.material.bounciness * 10f;
        }

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
            currentCharacter.transform.rotation = Quaternion.Lerp(currentCharacter.transform.rotation, look, Time.deltaTime * 15); // face correctly
        }
        else
        {
            target = Vector3.zero;
        }

        //animate.SetFloat("Speed", speed);
        target.y = yTarget * gravity * Time.deltaTime;

        controller.Move(target);
        if (currentCharacter.transform.position.y < -20) // Teleport to origin if fall
        {
            currentCharacter.transform.position = ORIGIN;
        }
        debugNote.GetComponent<Text>().text = "yTarget: " + yTarget+"\nisGrounded: " + controller.isGrounded + "\nCurrently Touching: " + standingObject.collider + "\nIs on top of something: " + doesRaycastHit;
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
        int temp = othersHealth;
        othersHealth = health;
        health = temp;
        UpdateHealth();
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

    void Hurt()
    {
        health--;
        UpdateHealth();
        if(health == 0)
        {
            CommitDie();
        }
    }

    void UpdateHealth()
    {
        float healthlevel = (float)health / (float)HP_MAX;
        Image tempGauge = thermometer.transform.GetChild(1).GetComponentInChildren<Image>();
        if (selected == Characters.Ice)
            healthlevel = 1 - healthlevel;
        tempGauge.fillAmount = healthlevel;
        Debug.Log(health + " " + healthlevel + " " + selected);
    }

    private void CommitDie()
    {
        throw new NotImplementedException();
    }
}
