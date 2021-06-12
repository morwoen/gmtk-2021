using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 10;
    public float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Ice"))) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (!GetComponent<CharacterController>().enabled) {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastFired > 1 / fireRate)  
            {
                lastFired = Time.time;

                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}
