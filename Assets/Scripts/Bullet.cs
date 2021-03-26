using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyObject", 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
