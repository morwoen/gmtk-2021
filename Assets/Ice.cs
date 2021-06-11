using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice : MonoBehaviour
{
    public Slider slider;
    public GameObject ice;

    bool regen;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void FixedUpdate () {
        bool air = !Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Default")) && slider.value > 0;

        if (air && slider.value > 0.005f) {
            regen = false;
            slider.value -= 0.005f;
            CancelInvoke();
            Quaternion randomRot = Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
            Vector3 randomScale = new Vector3(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
            GameObject g = Instantiate(ice, transform.position + Vector3.down * 2, randomRot);
            g.transform.localScale = randomScale;
            Destroy(g, Random.Range(5, 7));
        } else if (regen) {
            slider.value += 0.005f;
        } else if (!air && !IsInvoking()) {
            Invoke("StartRegen", 2);
        }
    }

    void StartRegen () {
        regen = true;
    }
}
