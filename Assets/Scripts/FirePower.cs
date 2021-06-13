using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePower : MonoBehaviour
{
    public ParticleSystem fireEffect;

    private PlayerController player;
    private AudioSource audioSource;

    private void Awake () {
        player = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate () {
        if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Ice"))) {
            player.CancelRegen();
            player.Hurt(0.005f);
        } else if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Fire")) || Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Default"))) {
            player.QueueRegen(0.5f);
        } else {
            player.CancelRegen();
        }

        if (!GetComponent<CharacterController>().enabled) {
            fireEffect.Stop();
            return;
        } 
        
        if (Input.GetButton("Fire1")) {
            player.CancelRegen();
            player.Hurt(0.005f);
            if (!fireEffect.isPlaying) {
                audioSource.Play();
                fireEffect.Play();
            }
        } else {
            audioSource.Stop();
            fireEffect.Stop();
        }
    }
}
