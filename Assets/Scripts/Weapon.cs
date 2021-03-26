using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float shotAmount;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int maxAmmo;
    private int ammo;
    [SerializeField]
    private int bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        if (ammo > 0)
        {
            var spawnedBullet = Instantiate(bulletPrefab, gameObject.GetComponentInChildren<BulletSpawnPoint>().transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
