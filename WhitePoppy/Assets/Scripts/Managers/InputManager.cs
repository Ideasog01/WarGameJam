using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector2 movementInput;

    public static Vector2 mouseInput;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        InitialiseInput();
    }

    private void InitialiseInput()
    {
        //Assign input actions here. e.g _playerInput.Gameplay.Jump.performer += ctx => Jump();
    }

    private void Update()
    {
        AssignUpdateInput();
    }

    private void AssignUpdateInput()
    {
        movementInput = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
        mouseInput = _playerInput.Gameplay.MouseLook.ReadValue<Vector2>();

        Debug.Log(movementInput);
        Debug.Log(mouseInput);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
