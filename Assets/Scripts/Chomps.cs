using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomps : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navAgent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        InvokeRepeating("tick", 0, 0.5f);
    }
    //called every 0.5 seconds
    void tick()
    {
        navAgent.SetDestination(player.transform.position);
    }
}
