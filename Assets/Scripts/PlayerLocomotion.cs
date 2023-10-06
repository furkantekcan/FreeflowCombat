using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Transform cameraObject;
    CharacterController charController;

    private Vector3 moveDirection;

    private bool grounded = true;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private float lerpSpeed = 15.0f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        charController = GetComponent<CharacterController>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        
        Vector3 movementVelocity = moveDirection;

        grounded = charController.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        charController.Move(moveDirection * Time.deltaTime * playerSpeed);

        if (moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        charController.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
