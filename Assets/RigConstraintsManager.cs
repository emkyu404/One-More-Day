using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigConstraintsManager : MonoBehaviour
{
    [SerializeField] private List<WeaponConstraints> weaponConstraints = new List<WeaponConstraints>();
    [SerializeField] private Animator animator;
    [SerializeField] private ShootController shootController;
    private Dictionary<WeaponType, WeaponConstraints> dictionary = new Dictionary<WeaponType, WeaponConstraints>();
    private Dictionary<string, GameObject> weaponModels = new Dictionary<string, GameObject>();


    public void Awake()
    {
        InitDictionary();
        InitWeaponModels();
    }

    private void InitDictionary()
    {
        foreach (WeaponConstraints wp in weaponConstraints)
        {
            dictionary.Add(wp.weaponType, wp);
        }
    }

    private void InitWeaponModels()
    {
        GameObject weaponHolder = GameObject.Find("WeaponHolder");

        foreach (Transform child in weaponHolder.transform)
        {
            GameObject model = child.Find("Model").gameObject;
            weaponModels.Add(child.name, model);
        }
    }

    public void SetWeaponConstraints(WeaponType wt, string weaponName)
    {
        SetConstraints(wt);
        SetModel(weaponName);
        SetAnimator(wt);
    }

    private void SetAnimator(WeaponType wt)
    {
        int rifleIndex = animator.GetLayerIndex("Rifle");
        int pistolIndex = animator.GetLayerIndex("Pistol");
        int shotgunIndex = animator.GetLayerIndex("Shotgun");
        switch (wt)
        {
            case WeaponType.Rifle:
                {
                    animator.SetLayerWeight(rifleIndex, 1);
                    animator.SetLayerWeight(pistolIndex, 0);
                    animator.SetLayerWeight(shotgunIndex, 0);
                    return;
                }
            case WeaponType.Pistol:
                {
                    animator.SetLayerWeight(rifleIndex, 0);
                    animator.SetLayerWeight(pistolIndex, 1);
                    animator.SetLayerWeight(shotgunIndex, 0);
                    return;
                }
            case WeaponType.Shotgun:
                {
                    animator.SetLayerWeight(rifleIndex, 0);
                    animator.SetLayerWeight(pistolIndex, 0);
                    animator.SetLayerWeight(shotgunIndex, 1);
                    return;
                }
        }
    }

    private void SetModel(string weaponName)
    {
        foreach (KeyValuePair<string, GameObject> wm in weaponModels)
        {
            if (wm.Key.Equals(weaponName))
            {
                wm.Value.SetActive(true);
                Transform newBulletSpawnPoint = wm.Value.transform.Find("BulletSpawnPoint");
                shootController.SetBulletSpawnPoint(newBulletSpawnPoint);
                Debug.Log("New Bullet spawn point");
            }
            else
            {
                wm.Value.SetActive(false);
            }
        }
    }

    private void SetConstraints(WeaponType wt)
    {
        foreach (KeyValuePair<WeaponType, WeaponConstraints>wp in dictionary)
        {
            if(wp.Key == wt)
            {
                wp.Value.EnableConstraint();
            }
            else
            {
                wp.Value.DisableConstraint();
            }
        }
    }
}
