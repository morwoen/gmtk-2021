﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebullet : MonoBehaviour
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
        if(Input.GetButton("Fire1"))
        {
            if (Time.time - lastFired > 1 / fireRate)  
            {
                lastFired = Time.time;

                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}
