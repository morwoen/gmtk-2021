using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetHealth : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip clip;
    public int health;

    public void TakeDamage (int damage) {
        health -= damage;
        if (health <= 0) {
            if (clip != null) {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            if (explosion != null) {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
