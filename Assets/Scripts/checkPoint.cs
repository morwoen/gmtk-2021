using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{   
    //this script should be attack to whatever object we are makeing the checkpoints with(perhaps an empty gameObject) 
    private gameMaster gm; 
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameMaster>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(GameObject.FindGameObjectWithTag("Player")) // both players should have the tag player
        {
            gm.lastCheckpointPos = transform.position; 
        }
    }
}
