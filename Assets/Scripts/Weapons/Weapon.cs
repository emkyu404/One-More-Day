using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private WeaponData weaponData;
    public string name;
    public float shootDelay;
    public float bulletSpeed;

    public bool addBulletSpread;
    public float maxSpread;
    public float minSpread;
    public int ammoPerShoot;

    public float damage;
    public float critRate;
    public float critDmg;

    public bool infiniteAmmo;
    public float reloadDelay;
    public int ammo;
    public int maxAmmo;
    public int totalAmmo;

    public AudioClip audio;
    public Sprite weaponSprite;
    public Sprite ammoSprite;

    public WeaponType weaponType;
    public AmmoType ammoType;

    public Weapon(WeaponData wd)
    {
        weaponData = wd;
        name = wd.name;
        shootDelay = wd.shootDelay;
        bulletSpeed = wd.bulletSpeed;
        addBulletSpread = wd.addBulletSpread;
        maxSpread = wd.maxSpread;
        minSpread = wd.minSpread;
        ammoPerShoot = wd.ammoPerShoot;
        damage = wd.damage;
        critRate = wd.critRate;
        critDmg = wd.critDmg;
        reloadDelay = wd.reloadDelay;
        ammo = wd.ammo;
        maxAmmo = wd.maxAmmo;
        totalAmmo = wd.totalAmmo;
        audio = wd.audio;
        weaponType = wd.weaponType;
        weaponSprite = wd.weaponSprite;
        ammoSprite = wd.ammoSprite;
        infiniteAmmo = wd.infiniteAmmo;
        ammoType = wd.ammoType;
    }

    public void RefillAmmo()
    {
        totalAmmo = weaponData.totalAmmo + weaponData.maxAmmo - ammo;
    }
}
