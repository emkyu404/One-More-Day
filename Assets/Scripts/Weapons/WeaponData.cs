using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon", menuName ="Weapon/New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shoot behavior")]
    public float shootDelay;
    public float bulletSpeed;

    [Header("Spread Parameters")]
    public bool addBulletSpread;
    public float maxSpread;
    public float minSpread;
    public int ammoPerShoot;

    [Header("Damage")]
    public float damage;
    public float critRate;
    public float critDmg;

    [Header("Ammo & Reload")]
    public bool infiniteAmmo;
    public float reloadDelay;
    public int ammo;
    public int maxAmmo;
    public int totalAmmo;

    [Header("UI")]
    public Sprite weaponSprite;
    public Sprite ammoSprite;

    [Header("Sound")]
    public AudioClip audio;

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Ammo Type")]
    public AmmoType ammoType;
}

public enum WeaponType
{
    Rifle,
    Pistol,
    Shotgun
}

public enum AmmoType
{
    Bullets,
    Laser,
    Projectile // bomb
}