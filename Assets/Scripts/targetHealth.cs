using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetHealth : MonoBehaviour
{

    public AudioClip clip;
    public int health;

    ParticleSystem effect;
    Animator melt;
    bool begun;

    private void Awake()
    {
        begun = false;
        effect = transform.GetComponentInChildren<ParticleSystem>();
        melt = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (begun)
        {
            if(!effect.isPlaying)
                gameObject.SetActive(false);
        }
    }

    public void TakeDamage (int damage) {
        health -= damage;
        if (health <= 0) {
            if (clip != null) {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            if (effect != null) {
                if (!effect.isPlaying) {
                    begun = true;
                    //Instantiate(explosion, transform.position, Quaternion.identity);
                    effect.Play();
                    melt.SetTrigger("Melting");
                }
            } else {
                gameObject.SetActive(false);
            }
        }
    }
}
