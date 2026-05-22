using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BaseMovement : MonoBehaviour
{
    public float moveSpeed = 6.5f;
    public float rotationSpeed = 720f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void MoveTo(Vector3 direction)                                                                                      
    {
        Vector3 moveVector = direction.normalized * moveSpeed;
        controller.Move(moveVector * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            verticalVelocity.y = -2f; 
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }
        controller.Move(verticalVelocity * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed", direction.magnitude);
        }
    }
}