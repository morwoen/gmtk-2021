using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    // UI
    public Canvas UI;
    GameObject thermometer;

    //DEBUG
    private GameObject debugNote;

    //player characters
    public GameObject fireCharacter;
    public GameObject iceCharacter;
    public enum Characters {Fire,Ice};
    Characters selected;
    GameObject currentCharacter;

    //collision
    RaycastHit standingObject;
    bool doesRaycastHit;

    //health
    float health;
    bool regen;
    float canAir;
    
    //camera
    public Camera mainCamera;

    //gameObject components
    Animator animate;
    CharacterController controller;

    //static vectors
    static Vector3 OFFSET = new Vector3(0, 1.5f, 0);
    static Vector3 ORIGIN = new Vector3(0, 10, 0);

    //const variables

    //movement variables
    Vector3 stickPosition;
    Quaternion look;
    Vector3 target;
    float yTarget;
    float gravity;
    float timeSinceMidAir;
    public float coyoteTime = 0.1f;

    //camera variables
    Vector3 cameraTarget;
    Vector3 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        stickPosition = Vector3.zero;
        health = 1;
        selected = Characters.Fire;
        fireCharacter = transform.GetChild(0).gameObject;
        iceCharacter = transform.GetChild(1).gameObject;
        currentCharacter = fireCharacter;
        controller = currentCharacter.GetComponent<CharacterController>();
        gravity = 1.7f;
        debugNote = UI.transform.Find("DebugText").gameObject;
        thermometer = UI.transform.Find("HealthSlider").gameObject;
        animate = fireCharacter.GetComponent<Animator>();

        SetupCinemachine();
    }

    // Update is called once per frame
    void Update()
    {
        doesRaycastHit = false; //DEBUG
        if (Input.GetButtonDown("Swap"))
        {
            Swap();
            yTarget = 0;
            return;
        }

        if (controller.isGrounded) {
            timeSinceMidAir = 0;
        } else {
            timeSinceMidAir += Time.deltaTime;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        animate.SetBool("Moving", direction.magnitude > 0.1f);

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
        if (Input.GetButtonDown("Jump") && timeSinceMidAir < coyoteTime)
        {
            //jump = true;
            //animate.SetTrigger("Jump");
            yTarget = 5;
        }

		if (controller.isGrounded) {
			canAir = Time.timeSinceLevelLoad;
			animate.SetBool("Air", false);
		} else if (Time.timeSinceLevelLoad - canAir > 0.1f) {
			animate.SetBool("Air", true);
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
        if (iceCharacter.transform.position.y < -30 || fireCharacter.transform.position.y < -30) // Teleport to origin if fall
        {
            CommitDie();
        }
        debugNote.GetComponent<Text>().text = "yTarget: " + yTarget+"\nisGrounded: " + controller.isGrounded + "\nCurrently Touching: " + standingObject.collider + "\nIs on top of something: " + doesRaycastHit;
    }

	private void FixedUpdate () {
		if(regen) {
            Hurt(-0.01f);
		}
	}

	public void CancelRegen() {
        CancelInvoke();
        regen = false;
	}

    public void QueueRegen(float time) {
        if(!IsInvoking() && !regen) {
            Invoke("Regen", time);
		}
	}

    void Regen() {
        regen = true;
	}

    private void Swap()
    {
        //CancelRegen();
        // stop moving if the switch was while moving
        animate.SetBool("Moving", false);

        currentCharacter.GetComponent<CharacterController>().enabled = false;
        //currentCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        animate = currentCharacter.GetComponent<Animator>();
        currentCharacter.GetComponent<CharacterController>().enabled = true;
        SetupCinemachine();
    }

    void SetupCinemachine() {
        var cinemachine = FindObjectOfType<CinemachineFreeLook>();
        cinemachine.Follow = currentCharacter.transform;
        cinemachine.LookAt = currentCharacter.transform;
    }

    public void Hurt(float amount)
    {
        health -= amount;

        health = Mathf.Clamp(health, 0, 1);

        if (health <= 0.01f)
        {
            CommitDie();
        }
        //else if (selected == Characters.Fire)
        //else
        //    health += amount;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        Slider tempGauge = thermometer.GetComponent<Slider>();
        tempGauge.value = health;
        //Debug.Log(health + " " + selected);
    }

    public float GetHealth() {
        return health;
	}

    public void CommitDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
