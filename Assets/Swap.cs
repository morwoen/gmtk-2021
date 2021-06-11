using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    public GameObject dummy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) {
            Vector3 pos = transform.position;
            GetComponent<CharacterController>().enabled = false;
            transform.position = dummy.transform.position;
            GetComponent<CharacterController>().enabled = true;
            dummy.transform.position = pos;
		}
    }
}
