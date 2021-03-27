using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomps : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navAgent;
    public int Health;
    public int PowerLevel;
    public bool stunned;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        Health = 10;
        PowerLevel = 1;

        InvokeRepeating("Tick", 0, 0.5f);
    }
    //called every 0.5 seconds
    void Tick()
    {
        navAgent.SetDestination(player.transform.position);
        PowerLevel++;
        if (PowerLevel >= 300)
        {
            navAgent.speed = 6.5f;
        }
        else if (PowerLevel >= 180)
        {
            navAgent.speed = 5.5f;
        }
        else if (PowerLevel >= 60)
        {
            navAgent.speed = 4.5f;
        }
        if (Health <= 0)
        {
            Die();
        }
    }
    void Stunned()
    {
        navAgent.isStopped = true;
    }
    void Resume()
    {
        navAgent.isStopped = false;
    }
    void Die()
    {
        Destroy(gameObject);
        //teleport
    }
}
