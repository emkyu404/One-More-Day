using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Inventory), typeof(AudioSource))]
public class ShootController : MonoBehaviour
{
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private ParticleSystem ImpactParticleSystem;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private Bullet bulletPrefab;



    [Header("Animator")]
    [SerializeField] private Animator animator;


    [Header("Weapon audio")]
    [SerializeField] private AudioSource weaponAS;

    [Header("Voice Lines")]
    [SerializeField] private AudioClip[] reloadingVL;
    [SerializeField] private AudioSource playerAS;


    private Inventory inventory;
    public bool reloading = false;
    private  float LastShootTime;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAS = GetComponent<AudioSource>();
        inventory = GetComponent<Inventory>();
        BulletSpawnPoint = GameObject.Find("BulletSpawnPoint").transform;
    }


    private void Update()
    {
        if (inventory.currentWeapon.ammo <= 0)
        {
            Reload();
        }
    }

    public void Shoot()
    {
        if (inventory.currentWeapon.ammo > 0 && !reloading && LastShootTime + inventory.currentWeapon.shootDelay < Time.time)
        { 
            Vector3 direction = GetDirection();
            for(int i = 0; i < inventory.currentWeapon.ammoPerShoot; i++)
            {
                Bullet instance = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.identity);
                instance.transform.SetParent(GameObject.Find("Bullets").transform, true);
                SpawnBullet(instance, direction);
                weaponAS.clip = inventory.currentWeapon.audio;
                weaponAS.Play();
            }
            LastShootTime = Time.time;
            animator.SetTrigger("Shoot");
            inventory.currentWeapon.ammo--;
        }
    }

    public IEnumerator Reloading()
    {
        if(inventory.currentWeapon.ammo == inventory.currentWeapon.maxAmmo)
        {
            Debug.Log("Already reloaded");
            yield break;
        }
        PlayRandomReloadingVoiceLine();
        Debug.Log("Reloading ...");
        animator.SetTrigger("Reload");
        CursorManager.GetInstance().ReloadCursor();
        reloading = true;
        int reloadAmount = 0;

        if (inventory.currentWeapon.infiniteAmmo)
        {
            reloadAmount = inventory.currentWeapon.maxAmmo - inventory.currentWeapon.ammo;
        }
        else
        {
            if (inventory.currentWeapon.totalAmmo >= inventory.currentWeapon.maxAmmo)
            {
                reloadAmount = inventory.currentWeapon.maxAmmo - inventory.currentWeapon.ammo;
            }
            else
            {
                if (inventory.currentWeapon.ammo + inventory.currentWeapon.totalAmmo > inventory.currentWeapon.maxAmmo)
                {
                    reloadAmount = inventory.currentWeapon.maxAmmo - inventory.currentWeapon.ammo;
                }
                else
                {
                    reloadAmount = inventory.currentWeapon.totalAmmo;
                }
            }
        }

        yield return new WaitForSeconds(inventory.currentWeapon.reloadDelay);
        inventory.currentWeapon.ammo += reloadAmount;
        if (!inventory.currentWeapon.infiniteAmmo)
        {
            inventory.currentWeapon.totalAmmo -= reloadAmount;
        }
        reloading = false;
        CursorManager.GetInstance().AimCursor();
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = playerController.GetMouseToWorldPosition() - transform.position;

        if (inventory.currentWeapon.addBulletSpread)
        {

            direction.x += Random.Range(-inventory.currentWeapon.minSpread, inventory.currentWeapon.maxSpread) * RandomSign(); 
        }

        direction.Normalize();

        return direction;
    }

    private int RandomSign() {
        return Random.value< .5? 1 : -1;
    }

    private void SpawnBullet(Bullet instance, Vector3 direction)
    {
        instance.Shoot(BulletSpawnPoint.position, direction, inventory.currentWeapon.bulletSpeed, inventory.currentWeapon);
    }

    private void PlayRandomReloadingVoiceLine()
    {
        if (reloadingVL.Length <= 0)
        {
            return;
        }
        playerAS.clip = reloadingVL[Random.Range(0, reloadingVL.Length)];
        playerAS.Play();
    }

    public void Reload()
    {
        if (inventory.currentWeapon.totalAmmo > 0 || inventory.currentWeapon.infiniteAmmo)
        {
            if (!reloading)
            {
                StartCoroutine(Reloading());
                return;
            }
        }
    }

    public bool isReloading()
    {
        return reloading;
    }

    public void SetBulletSpawnPoint(Transform transform)
    {
        BulletSpawnPoint = transform;
    }
}
