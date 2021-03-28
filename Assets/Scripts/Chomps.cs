using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int health = 1;
    private int powerLevel = 1;
    private int phase = 1;
    private int powerGrowthRate = 1;
    private int powerLevelThreshold = 30;
    private float speedIncrease = 0.5f;


    private Button playAgainButton;
    private Button mainMenuButton;
    private GameObject victoryScreenPanel;

    void Start()
    {
        playAgainButton = GameObject.Find("VictoryPlayAgainButton").GetComponent<Button>();
        mainMenuButton = GameObject.Find("VictoryMainMenuButton").GetComponent<Button>();
        victoryScreenPanel = GameObject.Find("VictoryPanel");

        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(GoToMenu);
        victoryScreenPanel.SetActive(false);

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
            health--;
        }
    }
    void Update()
    {
        navAgent.SetDestination(player.transform.position);
    }
    //called every 1.0 seconds
    void Tick()
    {
        powerLevel += powerGrowthRate;
        if (powerLevel >= powerLevelThreshold)
        {
            navAgent.speed += speedIncrease;
            powerLevelThreshold *= 2;
        }
        if (health <= 0)
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
        if (phase == 1)
        {
            gameObject.transform.Translate(new Vector3(-(gameObject.transform.position.x), 2, -(gameObject.transform.position.z)), Space.World);
            SpawnItems(PhaseTwoSpawnList);
            phase++;
            health = 1;
            powerGrowthRate = 2;
            powerLevelThreshold = 30;
            powerLevel = 1;
            Stun();
        }
        else if (phase == 2)
        {
            gameObject.transform.Translate(new Vector3(-(gameObject.transform.position.x), 2, -(gameObject.transform.position.z)), Space.World);
            SpawnItems(PhaseThreeSpawnList);
            phase++;
            health = 1;
            powerGrowthRate = 3;
            powerLevelThreshold = 30;
            powerLevel = 1;
            Stun();
        }
        else if (phase == 3)
        {
            Die();
            WinGame();
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
                            Instantiate(Ammo, new Vector3(0, 0.4f, 0) + randomLocation, Quaternion.Euler(-90, 0, 0));
                            break;
                        case 1:
                            Instantiate(Mines, randomLocation, Quaternion.Euler(-90, 0, 0));
                            break;
                        case 2:
                            Instantiate(Guns, new Vector3(0, 0.5f, 0) + randomLocation, Quaternion.Euler(45, 0, 0));
                            break;
                    }
                }
            }
        }
    }
    void Die()
    {
        navAgent.isStopped = true;
    }

    void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void WinGame()
    {
        victoryScreenPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health--;
        }
    }
}
