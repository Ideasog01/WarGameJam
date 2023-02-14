using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour
{
    #region Variables
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float sprintSpeed = 4.0f;
    [SerializeField]
    private float crouchSpeed = 1.0f;
    [SerializeField]
    private float cameraRotationSpeed = 3f;

    private float gravityValue = -9.81f;

    private Transform cameraTransform;
    
    public bool isMoving = true;
    public bool isCarrying = false;
    #endregion

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;

        // Turn off Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
