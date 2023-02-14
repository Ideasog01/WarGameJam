using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseCharacter : MonoBehaviour
{
    #region Variables


    public static bool disableMovement;
    public static InteractItem interactItem;

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

    void Update()
    {
        if (isMoving && !disableMovement)
        {
            Movement();
            Jump();
        }
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        
        if(inputManager.IsSprinting() && !isCarrying)
            controller.Move(move * sprintSpeed * Time.deltaTime); //zoomies
        else if (inputManager.IsCrouching() && !isCarrying)
            controller.Move(move * crouchSpeed * Time.deltaTime); // Add change collider
        else if(isCarrying)
            controller.Move(move * (playerSpeed*0.6f) * Time.deltaTime); // Reduce speed while carrying "someone"
        else
            controller.Move(move * playerSpeed * Time.deltaTime); //Normal speed

        //Rotate towards camera direction
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, cameraRotationSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // Changes the height position of the player..
        if (inputManager.PlayerJump() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Interact()
    {
        if(interactItem != null)
        {
            if(interactItem.TryGetComponent<Letter>(out Letter letter))
            {
                letter.InteractLetter();
            }
            else if(interactItem.TryGetComponent<Item>(out Item item))
            {
                
            }
        }
    }
}
