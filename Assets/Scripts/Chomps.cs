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
    public GameObject Mines;
    public GameObject Guns;
    public int[] PhaseOneSpawnList;
    public int[] PhaseTwoSpawnList;
    public int[] PhaseThreeSpawnList;

    public int Health = 10;
    public int PowerLevel = 1;
    public int Phase = 1;

    public int PowerGrowthRate = 1;
    public int PowerLevelThreshold = 30;
    public float Speed = 0.5f;

    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        SpawnItems(PhaseOneSpawnList);
        InvokeRepeating("Tick", 0, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Health--;
        }
    }
    void Update()
    {
        navAgent.SetDestination(player.transform.position);
    }
    //called every 1.0 seconds
    void Tick()
    {
        PowerLevel += PowerGrowthRate;
        if (PowerLevel >= PowerLevelThreshold)
        {
            navAgent.speed += Speed;
            PowerLevelThreshold *= 2;
        }
        if (Health <= 0)
        {
            NextPhase();
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
        if (Phase == 1)
        {
            gameObject.transform.Translate(new Vector3(-(gameObject.transform.position.x),2, -(gameObject.transform.position.x)));
            SpawnItems(PhaseTwoSpawnList);
            Phase++;
            Health = 20;
            PowerGrowthRate = 2;
        }
        else if (Phase == 2)
        {
            gameObject.transform.Translate(new Vector3(-(gameObject.transform.position.x), 2, -(gameObject.transform.position.x)));
            SpawnItems(PhaseThreeSpawnList);
            Phase++;
            Health = 30;
            PowerGrowthRate = 3;
        }
        else if (Phase == 3)
        {
            Die();
        }
    }
    void SpawnItems(int[] _spawnList)
    {
        for (int i = 0; i < 3; i++)
        {
            while (_spawnList[i] > 0)
            {
                randomLocation = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
                if (NavMesh.SamplePosition(randomLocation, out navhit, 0.5f, 1) == true)
                {
                    _spawnList[i]--;
                    switch (i)
                    {
                        case 0:
                            Instantiate(Ammo, new Vector3(0,0.5f,0)+randomLocation, Quaternion.Euler(-90,0,0));
                            break;
                        case 1:
                            Instantiate(Mines, randomLocation, Quaternion.Euler(-90, 0, 0));
                            break;
                        case 2:
                            Instantiate(Guns, new Vector3(0, 0.5f, 0) + randomLocation, Quaternion.Euler(0, 0, 0));
                            break;
                    }
                }
            }
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Health--;
        }
    }
}
