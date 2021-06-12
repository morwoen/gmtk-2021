using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePower : MonoBehaviour
{
    public ParticleSystem fireEffect;

    private PlayerController player;

    private void Awake () {
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate () {
        if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Ice"))) {
            player.CancelRegen();
            player.Hurt(0.005f);
        } else if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Fire"))) {
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
                fireEffect.Play();
            }
        } else {
            fireEffect.Stop();
        }
    }
}
