using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(ShootController))]
public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float bulletHitMissDistance = 25f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float rotateSmoothing = 1000f;

    [Header("Animator")]
    [SerializeField] private Animator animator;


    private CharacterController controller;
    private ShootController shootController;
    private PlayerInput playerInput;
    private InteractionRange interaction;
    private Inventory inventory;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    [SerializeField] Transform bulletSpawnLocation;

    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction aimAction;
    private InputAction reloadAction;
    private InputAction interactAction;
    private InputAction weapon1Action;
    private InputAction weapon2Action;
    private InputAction weapon3Action;
    private InputAction scrollWeaponAction;

    private bool isShooting;
    private bool isGamepad;
    private Vector3 mousePosToWorldPosition;
    
    private void Awake()
    {
        shootController = GetComponent<ShootController>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        interaction = GetComponent<InteractionRange>();
        inventory = GetComponent<Inventory>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        reloadAction = playerInput.actions["Reload"];
        interactAction = playerInput.actions["Interact"];
        scrollWeaponAction = playerInput.actions["ScrollWeapon"];
        weapon1Action = playerInput.actions["WeaponSlot1"];
        weapon2Action = playerInput.actions["WeaponSlot2"];
        weapon3Action = playerInput.actions["WeaponSlot3"];
    }

    private void Start()
    {
        shootAction.started += ctx => HandleShoot(ctx);
        shootAction.canceled += ctx => HandleShoot(ctx);
        reloadAction.performed += ctx => shootController.Reload();
        interactAction.performed += ctx => interaction.Interact(this);
        scrollWeaponAction.performed += ctx =>
        {
            if (scrollWeaponAction.ReadValue<float>() < 0)
            {
                inventory.NextWeapon();
            }
            else
            {
                inventory.PreviousWeapon();
            }
        };
        weapon1Action.performed += ctx => inventory.ChangeWeapon(0);
        weapon2Action.performed += ctx => inventory.ChangeWeapon(1);
        weapon3Action.performed += ctx => inventory.ChangeWeapon(2);
    }

    private void OnEnable()
    {
        shootAction.Enable();
        reloadAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
        reloadAction.Disable();
    }

    

    void Update()
    {
        PerformMovement();
        PerformRotation();
        PerformShooting();
    }

    private void PerformMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        animator.SetFloat("Velocity X", input.x);
        animator.SetFloat("Velocity Z", input.y);
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void PerformRotation()
    {
        Vector2 aim = aimAction.ReadValue<Vector2>();
        if (isGamepad)
        {
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if(playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            LookAt(GetMouseToWorldPosition());
        }


    }

    public Vector3 GetMouseToWorldPosition()
    {
        CalculateMouseToWorldPosition();
        return this.mousePosToWorldPosition;
    }

    public Vector2 GetMousePosition()
    {
        return aimAction.ReadValue<Vector2>();
    }

    public void CalculateMouseToWorldPosition()
    {
        Vector2 aim = GetMousePosition();
        Ray ray = Camera.main.ScreenPointToRay(aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            this.mousePosToWorldPosition = heightCorrectedPoint;
        }
    }
    private void HandleShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
        }

        if (context.canceled)
        {
            isShooting = false;
        }
    }

    private void PerformShooting()
    {
        if (isShooting)
        {
            shootController.Shoot();
        }
    }

    private void LookAt(Vector3 LookPoint)
    {
        CalculateMouseToWorldPosition();
        transform.LookAt(mousePosToWorldPosition);
        animator.SetFloat("Rotation Y", transform.rotation.y);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}