using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ZombieController))]
[DefaultExecutionOrder(1)]
public class AIUnit : MonoBehaviour
{
    public ZombieController zombieController;
    private void Awake()
    {
        zombieController = GetComponent<ZombieController>();
        AIManager.Instance.Units.Add(this);
    }
    public void MoveTo(Vector3 position)
    {
        zombieController.SetTarget(position);
    }
}
