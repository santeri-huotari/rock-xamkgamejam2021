using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomps : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navAgent;
    private NavMeshHit navhit;
    private Vector3 randomLocation;
    private Animator anim;

    public GameObject Ammo;
    public int Health;
    public int PowerLevel;
    public bool Stunned;
    public int AmmoSpawnCount;
    public int Phase;

    public int PowerGrowthRate;
    public int PowerLevelThreshold;
    public float Speed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        Health = 3;
        PowerLevel = 1;
        Phase = 1;

        SpawnItems();
        InvokeRepeating("Tick", 0, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Health--;
        }
    }
    //called every 0.5 seconds
    void Tick()
    {
        navAgent.SetDestination(player.transform.position);
        PowerLevel += PowerGrowthRate;
        if (PowerLevel >= PowerLevelThreshold)
        {
            navAgent.speed += Speed;
            PowerLevelThreshold *= 2;
        }
        if (Health <= 0)
        {
            Die();
        }
    }
    void Stun()
    {
        Invoke("Stop", 0f);
        Invoke("Resume", 5f);
    }
    void Stop()
    {
        anim.SetTrigger("Stop");
        navAgent.isStopped = true;
    }
    void Resume()
    {
        anim.SetTrigger("Resume");
        navAgent.isStopped = false;
    }
    void NextPhase()
    {
        //increase power growth rate 
    }
    void SpawnItems()
    {
        while (AmmoSpawnCount > 0)
        {
            randomLocation = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            if (NavMesh.SamplePosition(randomLocation, out navhit, 0.5f, 1) == true)
            {
                Instantiate(Ammo, randomLocation, transform.rotation);
                AmmoSpawnCount--;
            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
        //teleport
    }

    void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Health--;
        }
    }
}
