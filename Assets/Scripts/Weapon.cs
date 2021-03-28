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
    private int startingAmmo;
    public int ammo;
    [SerializeField]
    private int bulletSpeed;
    private bool canShoot = true;
    float cooldown;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ammo = startingAmmo;
        cooldown = fireRate;
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (ammo > 0 && canShoot)
        {
            var spawnedBullet = Instantiate(bulletPrefab, gameObject.GetComponentInChildren<BulletSpawnPoint>().transform.position, transform.rotation);
            spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

            audioSource.Play();

            ammo--;
            canShoot = false;
            InvokeRepeating("FireRateCountdown", 0f, 1f);
        }
    }

    void FireRateCountdown()
    {
        cooldown -= 1;
        if (cooldown == 0)
        {
            canShoot = true;
            cooldown = fireRate;
            CancelInvoke();
        }
    }
}
