using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private bool soldierExists;

    private static InputManager instance;

    //For the player only
    private bool isSprinting = false;
    private bool isCrouching = false;

    private PlayerCharacter _playerCharacter;
    private SoldierCharacter _soldierCharacter;
    private ItemDisplay _itemDisplay;

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

        _playerCharacter = GameObject.Find("Player").GetComponent<PlayerCharacter>();

        if(soldierExists)
        {
            _soldierCharacter = GameObject.Find("Player").GetComponent<SoldierCharacter>();
        }
        
        _itemDisplay = this.GetComponent<ItemDisplay>();

        controls = new PlayerInputSystem();
        InitialiseInput();
    }

    private void InitialiseInput()
    {
        controls.Player.Interact.performed += ctx => _playerCharacter.Interact();

        if(soldierExists)
        {
            controls.Player.SprintStart.performed += ctx => SprintPressed();
            controls.Player.SprintFinish.performed += ctx => SprintReleased();
            controls.Player.CrouchStart.performed += ctx => CrouchPressed();
            controls.Player.CrouchFinish.performed += ctx => CrouchReleased();

            controls.Player.Fire.performed += ctx => _soldierCharacter.Fire();
            controls.Player.Reload.performed += ctx => _soldierCharacter.Reload();
        }
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
}
