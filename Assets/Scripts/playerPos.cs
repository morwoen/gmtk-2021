using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;  

public class playerPos : MonoBehaviour
{   //This script need to be attached to both players 
    // Start is called before the first frame update 
    private gameMaster gm; 
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<gameMaster>();
        transform.position = gm.lastCheckPointPos; 
    }

    // Update is called once per frame
    void Update()
    {
        if (//the player dies) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }
}
