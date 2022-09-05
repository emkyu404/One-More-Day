using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShootController))]
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Weapon currentWeapon;
    [SerializeField] private WeaponData[] weaponsData;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    //[SerializeField] private List<Projectile> projectile = new List<Projectile>();
    [SerializeField] private RigConstraintsManager rig;

    [Header("Weapon Change cooldown")]
    [SerializeField] private float cooldown = 1f;
    private bool onCooldown = false;

    [Header("UI")]
    [SerializeField] private Text ammoUI;
    [SerializeField] private Image infiniteAmmoUI;
    [SerializeField] private Text totalAmmoUI;
    [SerializeField] Image ammoBar;
    [SerializeField] Image weaponIcon;
    [SerializeField] Image weaponAmmo1;
    [SerializeField] Image weaponAmmo2;
    [SerializeField] Image weaponAmmo3;


    private ShootController shootController;
    private float lerpSpeed;

    private int currentIndex = 0;

    
    public void Awake()
    {
        shootController = GetComponent<ShootController>();
        InitWeaponsInInventory();
    }

    private void Start()
    {
        ChangeWeapon();
        UpdateUI();
    }

    private void Update()
    {
        lerpSpeed = 10f * Time.deltaTime;
        AmmoBarFiller();
        UpdateUI();
    }

    private void InitWeaponsInInventory()
    {
        foreach (WeaponData wd in weaponsData)
        {
            Weapon wp = new Weapon(wd);
            weapons.Add(wp);
        }
    }

    public void RefillWeaponAmmo()
    {
        foreach(Weapon wp in weapons)
        {
            wp.RefillAmmo();
        }
    }

    public void ChangeWeapon(int index)
    {
        if (onCooldown)
        {
            return;
        }

        if (shootController.isReloading())
        {
            return;
        }

        if (currentIndex < 0 || currentIndex >= weapons.Count)
        {
            return;
        }
        currentIndex = index;
        currentIndex %= weapons.Count;
        ChangeWeapon();
    }

    public void NextWeapon()
    {
        if (onCooldown)
        {
            return;
        }

        if (shootController.isReloading())
        {
            return;
        }
        currentIndex++;
        currentIndex %= weapons.Count;
        ChangeWeapon();
    }

    public void PreviousWeapon()
    {
        if (onCooldown)
        {
            return;
        }

        if (shootController.isReloading())
        {
            return;
        }
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = weapons.Count + currentIndex;
        }
        currentIndex %= weapons.Count;
        Debug.Log(currentIndex);
        Debug.Log("weapons.Count : " + weapons.Count);
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        currentWeapon = weapons[currentIndex];
        rig.SetWeaponConstraints(currentWeapon.weaponType, currentWeapon.name);
        UpdateSpriteUI();
    }

    private void UpdateUI()
    {
        ammoUI.text = currentWeapon.ammo.ToString();
        totalAmmoUI.text = currentWeapon.totalAmmo.ToString();
    }

    private void AmmoBarFiller()
    {
        ammoBar.fillAmount = Mathf.Lerp(ammoBar.fillAmount, (float)currentWeapon.ammo / (float)currentWeapon.maxAmmo, lerpSpeed);
    }

    public IEnumerator WeaponChangeCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private void UpdateSpriteUI()
    {
        totalAmmoUI.gameObject.SetActive(!currentWeapon.infiniteAmmo);
        infiniteAmmoUI.gameObject.SetActive(currentWeapon.infiniteAmmo);
        weaponAmmo1.sprite = currentWeapon.ammoSprite;
        weaponAmmo2.sprite = currentWeapon.ammoSprite;
        weaponAmmo3.sprite = currentWeapon.ammoSprite;
        weaponIcon.sprite = currentWeapon.weaponSprite;
    }

}
