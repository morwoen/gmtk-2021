using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice : MonoBehaviour
{
    public Slider slider;
    public GameObject ice;

    private PlayerController player;

    bool regen;

    const float ICE_RATE = 1.56f; // The magic number

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position + Vector3.down, 0.5f, LayerMask.GetMask("Fire")))
        {
            player.Hurt(.5f * Time.fixedDeltaTime);
        }

        if (!GetComponent<CharacterController>().enabled)
        {
            slider.gameObject.SetActive(false);
            return;
        }

        slider.gameObject.SetActive(true);

        bool isOnIce = Physics.CheckSphere(transform.position + Vector3.down, 0.4f, LayerMask.GetMask("Ice"));

        if (Input.GetButton("Fire1"))
        {

            if (!isOnIce && slider.value > 0.01f)
            {
                regen = false;
                slider.value -= 0.005f;
                slider.value = Mathf.Clamp(slider.value, 0, 1);
                CancelInvoke();
                Quaternion randomRot = Quaternion.Euler(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
                Vector3 randomScale = new Vector3(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
                GameObject g = Instantiate(ice, transform.position + Vector3.down * ICE_RATE, randomRot); 
                g.transform.localScale = randomScale;
                Destroy(g, Random.Range(5, 7));
            }

        }
        if (regen)
        {
            slider.value += 0.01f;
            slider.value = Mathf.Clamp(slider.value, 0, 1); //TODO: separate UI value and timer value
        }
        else if (isOnIce && !IsInvoking())
        {
            Invoke("StartRegen", 0.5f);
        }
    }

    void StartRegen()
    {
        regen = true;
    }
}
