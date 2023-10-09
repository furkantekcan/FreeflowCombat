using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Camera cameraObject;
    CharacterController charController;
    Animator animator;

    private Vector3 moveDirection;
    private Vector3 moveVector;
    private float verticalVelocity;

    [Header("Settings")]
    [SerializeField] private bool blockPlayerRotation;
    [SerializeField] private bool grounded;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float fallSpeed = 0.2f;
    [SerializeField] float rotationSpeed = 0.1f;

    public float lerpSpeed = 0.1f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        charController = GetComponent<CharacterController>();
        cameraObject = Camera.main;
        animator = GetComponentInChildren<Animator>();
    }

    private void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        //HandleAttack();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.transform.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.transform.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        grounded = charController.isGrounded;
        if (grounded)
        {
            verticalVelocity -= 0;
        }

        else
        {
            verticalVelocity -= 1;
        }

        moveVector = new Vector3(0, verticalVelocity * fallSpeed * Time.deltaTime, 0);
        charController.Move(moveVector);

        charController.Move(moveDirection * Time.deltaTime * playerSpeed);
    }

    private void HandleRotation()
    {
        if (blockPlayerRotation == false)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * acceleration);
			charController.Move(moveDirection * Time.deltaTime * (playerSpeed * acceleration));
		}
		else
		{
			charController.Move((transform.forward * inputManager.verticalInput + transform.right * inputManager.verticalInput) * Time.deltaTime * (playerSpeed * acceleration));
		}
    }

    public void LookAt(Vector3 position)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(position), rotationSpeed);
    }

    public void RotateToCamera(Transform objT)
    {
        moveDirection = cameraObject.transform.forward;
        Quaternion lookAtRotation = Quaternion.LookRotation(moveDirection);
        Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.eulerAngles.z);
        objT.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationY, rotationSpeed);        
    }

    public void InputMagnitude()
    {
        float inputMagnitude = new Vector2(inputManager.horizontalInput, inputManager.verticalInput).sqrMagnitude;

        if (inputMagnitude > 0.1f)
        {
            HandleAllMovement();
            animator.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f, Time.deltaTime);

        }

        else
        {
            animator.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f, Time.deltaTime);
        }
    }

	private void OnDisable()
	{
		animator.SetFloat("InputMagnitude", 0);
	}
}
