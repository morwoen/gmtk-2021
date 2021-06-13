using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour
{    
    //You have to create an empty gameObject and attack THIS script to it, be sure to give the empty gameObject the tag GM 
    private static gameMaster instance;
    public Vector3 lastCheckpointPos;  //this varible is important you have to set this to the position of the first checkpoint

    void Awake()
    {
        if(instance == null) //if the instance is eqaul to null then this will become the instance
        {
            instance = this;
            DontDestroyOnLoad(instance); //calling this will make sure that the object does destroy it's self between scences and reset the vaules
        } else
        {
            Destroy(gameObject); //this will make sure thier are not multiple gameMasters in the scence 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (lastCheckpointPos == Vector3.zero) {
          lastCheckpointPos = transform.position;
          FindObjectOfType<playerPos>().MovePlayer();
        }
    }
}
