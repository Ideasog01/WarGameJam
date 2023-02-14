using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;

    //For the player only
    private bool isSprinting = false;
    private bool isCrouching = false;

    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerInputSystem controls;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        
        controls = new PlayerInputSystem();
        InitialiseInput();
    }

    private void InitialiseInput()
    {
        controls.Player.SprintStart.performed += ctx => SprintPressed();
        controls.Player.SprintFinish.performed += ctx => SprintReleased();
        controls.Player.CrouchStart.performed += ctx => CrouchPressed();
        controls.Player.CrouchFinish.performed += ctx => CrouchReleased();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    #region Player Movement
    public Vector2 GetPlayerMovement()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return controls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJump()
    {
        return controls.Player.Jump.triggered;
    }

    #region Sprinting
    public bool IsSprinting()
    {
        return isSprinting;
    }

    private void SprintPressed()
    {
        isSprinting = true;
    }

    private void SprintReleased()
    {
        isSprinting = false;
    }
    #endregion

    #region Crouching
    public bool IsCrouching()
    {
        return isCrouching;
    }

    private void CrouchPressed()
    {
        isCrouching = true;
    }

    private void CrouchReleased()
    {
        isCrouching = false;
    }
    #endregion
    #endregion

    #region Interact
    public bool PlayerInteract()
    {
        return controls.Player.Interact.triggered;
    }
    #endregion
}
