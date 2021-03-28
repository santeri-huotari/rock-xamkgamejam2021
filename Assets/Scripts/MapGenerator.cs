using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnableObjects = new List<GameObject>();
    private GameObject mapScanner;

    private Bounds mapBounds;

    // Start is called before the first frame update
    void Start()
    {
        mapBounds = GameObject.Find("Ground").GetComponent<Collider>().bounds;
        mapScanner = GameObject.Find("MapScanner");
        GenerateMap();
        NavMeshBuilder.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateMap()
    {
        float mapWidth;
        float mapLenght;

        float randomScaleX;
        float randomScaleZ;

        float randomRotation;

        int spawnAnything;

        GameObject prevObject = null;

        mapWidth = GameObject.Find("Ground").GetComponent<Collider>().bounds.size.x;
        mapLenght = GameObject.Find("Ground").GetComponent<Collider>().bounds.size.z;

        for (int x = 0; x < mapWidth; x++)
        {
            spawnAnything = Random.Range(0, 2);

            if (spawnAnything != 1)
            {
                continue;
            }

            randomScaleX = Random.Range(1, 6);
            randomScaleZ = Random.Range(1, 11);
            randomRotation = Random.Range(0, 91);
            if (randomRotation > 45)
            {
                randomRotation = 90;
            }
            else
            {
                randomRotation = 0;
            }

            Vector3 pos = new Vector3(x - 100, 3.5f, 0);
            Quaternion rotation = new Quaternion(0, randomRotation, 0, 0);

            //mapScanner.transform.position = new Vector3(pos.x, mapScanner.transform.position.y, pos.z);
            //mapScanner.GetComponent<BoxCollider>().size = new Vector3(randomScaleX, 3, randomScaleZ);

            if (!Physics.BoxCast(pos, new Vector3(randomScaleX, 6, randomScaleZ), Vector3.down, rotation))
            {
                GameObject spawnedObject = Instantiate(spawnableObjects[0], pos, rotation);
                spawnedObject.transform.localScale += new Vector3(randomScaleX, 0, randomScaleZ);
                spawnedObject.transform.rotation = rotation;

                if (prevObject != null)
                {
                    if (prevObject.GetComponent<BoxCollider>().bounds.Intersects(spawnedObject.GetComponent<BoxCollider>().bounds))
                    {
                        Debug.Log("Lol get yeeted");
                        GameObject.Destroy(gameObject);
                    }
                }

                prevObject = spawnedObject;
            }
            else
            {
                Debug.Log("Cannot spawn. Object already present.");
            }
        }
    }

    GameObject ChooseRandomObject()
    {
        return spawnableObjects[Random.Range(0, spawnableObjects.Count)];
    }
}
