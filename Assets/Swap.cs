using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    public GameObject dummy;
    public bool freeze = true;

    // Start is called before the first frame update
    void Start()
    {
        dummy.GetComponent<MeshRenderer>().material.color = Color.red;
        transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
            freeze = !freeze;
            Vector3 pos = transform.position;
            GetComponent<CharacterController>().enabled = false;
            transform.position = dummy.transform.position;
            GetComponent<CharacterController>().enabled = true;
            dummy.transform.position = pos;

            if(!freeze) { // fire, so the other should be ice
                dummy.GetComponent<MeshRenderer>().material.color = Color.blue;
                transform.GetComponent<MeshRenderer>().material.color = Color.red;
            } else {
                dummy.GetComponent<MeshRenderer>().material.color = Color.red;
                transform.GetComponent<MeshRenderer>().material.color = Color.blue;
            }

            GetComponent<Ice>().slider.gameObject.SetActive(freeze);
            GetComponent<Ice>().enabled = freeze;
        }
    }
}
