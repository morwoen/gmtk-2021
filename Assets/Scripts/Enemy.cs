using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { FIRE, ICE };
    public EnemyType type;
    
    GameObject player;
    NavMeshAgent ai;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        ai = GetComponent<NavMeshAgent>();

        if(type == EnemyType.FIRE) {
            player = playerController.fireCharacter;
		} else {
            player = playerController.iceCharacter;
		}
    }

    // Update is called once per frame
    void Update()
    {
        ai.SetDestination(player.transform.position);
    }

	private void FixedUpdate () {
        if (Physics.CheckSphere(transform.position, 0.6f, LayerMask.GetMask("Player"))) {
            playerController.CancelRegen();
            playerController.Hurt(0.01f);
        }

    }
}
