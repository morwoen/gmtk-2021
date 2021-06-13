using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice : MonoBehaviour
{
    public float iceAge = 15;
    public GameObject ice;

    private PlayerController player;

    AudioSource audioSource;
    bool airIceDamage;

    //bool regen;

    const float ICE_RATE = 1.56f; // The magic number

    private void Awake () {
        player = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void FixedUpdate () {
        bool isOnIce = Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Ice"));

        if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Fire"))) {
            player.CancelRegen();
            player.Hurt(0.005f);
        } else if (isOnIce || Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Default"))) {
            airIceDamage = false;
            player.QueueRegen(0.5f);
        } else {
            player.CancelRegen();
            if (airIceDamage) player.Hurt(0.0032f);
        }

        if (!GetComponent<CharacterController>().enabled) {
            return;
        }

        if (Input.GetButton("Fire1")) {
            if (!isOnIce && player.GetHealth() > 0.01f) {
                airIceDamage = true;
                if (!audioSource.isPlaying) audioSource.Play();
                //regen = false;
                Quaternion randomRot = Quaternion.Euler(Random.Range(-8, 9), Random.Range(0, 359), Random.Range(-8, 8));
                Vector3 randomScale = new Vector3(Random.Range(0.1f, 3), Random.Range(0.8F, 1), Random.Range(0.1f, 3));
                GameObject g = Instantiate(ice, transform.position + Vector3.down * ICE_RATE, randomRot);
                g.transform.localScale = randomScale;
                Destroy(g, Random.Range(iceAge - 2, iceAge));
            }

        } else {
            audioSource.Stop();
        }

        //if (regen) {
        //    bool air = !Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Ice"));

        //    if (air) {
        //        cooldown.StartLose();
        //        player.Hurt(0.005f);
        //        CancelInvoke();
        //        Quaternion randomRot = Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
        //        Vector3 randomScale = new Vector3(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        //        GameObject g = Instantiate(ice, transform.position + Vector3.down * 2, randomRot);
        //        g.transform.localScale = randomScale;
        //        Destroy(g, Random.Range(5, 7));
        //    } else if (regen) { } else if (isOnIce && !IsInvoking()) {
        //        Invoke("StartRegen", 0.5f);
        //    }
        //}
    }

    //void StartRegen()
    //{
    //    regen = true;
    //}
}
