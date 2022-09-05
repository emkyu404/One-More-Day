using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging; 

public class RigHandTarget : MonoBehaviour
{
    [Header("Rifle Constraints")]
    [SerializeField] private TwoBoneIKConstraint rifle_RightHandIK;
    [SerializeField] private TwoBoneIKConstraint rifle_LeftHandIK;

    [SerializeField] private Transform rifle_RightHandTarget;
    [SerializeField] private Transform rifle_LeftHandTarget;

    [Header("Pistol Constraints")]
    [SerializeField] private TwoBoneIKConstraint pistol_RightHandIK;
    [SerializeField] private TwoBoneIKConstraint pistol_LeftHandIK;

    [SerializeField] private Transform pistol_RightHandTarget;
    [SerializeField] private Transform pistol_LeftHandTarget;

    [Header("Shotgun Constraints")]
    [SerializeField] private TwoBoneIKConstraint shotgun_RightHandIK;
    [SerializeField] private TwoBoneIKConstraint shotgun_LeftHandIK;

    [SerializeField] private Transform shotgun_RightHandTarget;
    [SerializeField] private Transform shotgun_LeftHandTarget;
}
