using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 720f; 
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;
    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Keyboard.current != null)
        {
            isRunning = Keyboard.current.leftShiftKey.isPressed;
        }

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float currentSpeed = isRunning ? runSpeed : walkSpeed;

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (animator != null)
        {
            float animSpeed = 0f;

            if (moveDirection.magnitude >= 0.1f)
            {
                animSpeed = isRunning ? 1f : 0.5f;
            }

            animator.SetFloat("Speed", animSpeed);
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isRunning = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}