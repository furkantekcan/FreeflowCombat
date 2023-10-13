using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    CombatScript playerCombat;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    
    private void Awake()
    {
        playerCombat = GetComponent<CombatScript>();  
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += Move;
            playerControls.PlayerMovement.Attack.performed += Attack;
            playerControls.PlayerMovement.Counter.performed += Counter;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        playerCombat.OnAttackInput.Invoke();
        Debug.Log(context.ReadValue<float>());
    }

    private void Counter(InputAction.CallbackContext context)
    {
        playerCombat.OnCounterInput.Invoke();
        Debug.Log(context.ReadValue<float>());
    }
}
