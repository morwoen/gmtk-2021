using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public int damage;
    public ParticleSystem flame;

    // Start is called before the first frame update
    void Start () {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter (Collider hitinfo) {
        targetHealth th = hitinfo.GetComponent<targetHealth>();
        if (th != null) {
            Destroy(gameObject);
            th.TakeDamage(damage);
            Instantiate(flame, transform.position, transform.rotation);
        }
    }
}
