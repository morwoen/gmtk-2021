using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public int damage;
    public ParticleSystem flame; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }  
   void OnTriggerEnter(Collider hitinfo)
    {
         targetHealth th = hitinfo.GetComponent<targetHealth>();  
        if(th != null) 
        {
            Debug.Log("wehitSomething"); 
            Destroy(gameObject);
            th.TakeDamage(damage);
            Instantiate(flame, transform.position, transform.rotation); 
        }
       
    }

  
}
