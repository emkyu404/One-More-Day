using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponConstraints : MonoBehaviour
{
    [SerializeField] public WeaponType weaponType;
    [SerializeField] TwoBoneIKConstraint right_twoBoneIK;
    [SerializeField] TwoBoneIKConstraint left_twoBoneIK;

    public void DisableConstraint()
    {
        right_twoBoneIK.weight = 0;
        left_twoBoneIK.weight = 0;
    }

    public void EnableConstraint()
    {
        right_twoBoneIK.weight = 1;
        left_twoBoneIK.weight = 1;
    }
}
