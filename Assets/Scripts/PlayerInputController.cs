using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseMovement))]
public class PlayerInputController : MonoBehaviour
{
    private BaseMovement movement;
    private Vector2 moveInput;
    private Transform mainCameraTransform;

    void Start()
    {
        movement = GetComponent<BaseMovement>();
        
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (mainCameraTransform != null)
        {
            Vector3 camForward = mainCameraTransform.forward;
            Vector3 camRight = mainCameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            direction = (camForward * moveInput.y) + (camRight * moveInput.x);
        }
        else
        {
            direction = new Vector3(moveInput.x, 0f, moveInput.y);
        }
        
        movement.MoveTo(direction);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}