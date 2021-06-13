using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrentai : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject enemyFirePoint;
    public GameObject player;
    public float range = 700;
    public float distToplayer;
    public int damage = 2;
    public GameObject missilePrefab;
    private float TimeBTWShots;
    public float startTimeBTWShots;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        TimeBTWShots = startTimeBTWShots;
    }

    // Update is called once per frame
    void Update()
    {
        distToplayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToplayer <= range)
        {
            EnemyShoot();


        }
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0, angle, 0);
    }

    void EnemyShoot()
    {
        if (TimeBTWShots <= 0)
        {
            Instantiate(missilePrefab, enemyFirePoint.transform.position, enemyFirePoint.transform.rotation);
            TimeBTWShots = startTimeBTWShots;
        }
        else
        {
            TimeBTWShots -= Time.deltaTime;
        }



    }

}
