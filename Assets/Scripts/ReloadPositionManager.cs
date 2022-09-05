using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPositionManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private bool isActive = true;

    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 mousePos = playerController.GetMousePosition();
            transform.position = mousePos;
        }
    }
}
