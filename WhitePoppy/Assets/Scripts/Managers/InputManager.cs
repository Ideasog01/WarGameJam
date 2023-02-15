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

    private PlayerInteract _playerInteract;

    private SoldierCharacter _soldierCharacter;

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

        _playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
        _soldierCharacter = GameObject.Find("Player").GetComponent<SoldierCharacter>();

        controls = new PlayerInputSystem();
        InitialiseInput();
    }

    private void InitialiseInput()
    {
        controls.Player.Interact.performed += ctx => _playerInteract.Interact();
        controls.Player.Fire.performed += ctx => _soldierCharacter.Fire();
        controls.Player.Reload.performed += ctx => _soldierCharacter.Reload();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
