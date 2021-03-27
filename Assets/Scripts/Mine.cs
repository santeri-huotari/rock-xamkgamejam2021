using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Light mineLight;
    private ParticleSystem explosionEffect;
    private GameObject chomps;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mineLight = GetComponentInChildren<Light>();
        explosionEffect = GetComponent<ParticleSystem>();
        chomps = GameObject.FindGameObjectWithTag("Chomps");
        InvokeRepeating("Tick",1,1);
    }
    //called every second
    private void Tick()
    {
        mineLight.enabled = !mineLight.enabled;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Explode();
            other.SendMessage("");
        }
        else if(other.tag == "Projectile")
        {
            Explode();
        }
    }
    void Explode()
    {
        meshRenderer.enabled = false;
        mineLight.enabled = false;
        explosionEffect.Play();
        if (Vector3.Distance(gameObject.transform.position, chomps.transform.position) < 4)
        {
            chomps.SendMessage("Stun");
        }

        Invoke("Die", 3);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
