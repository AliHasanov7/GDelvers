using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BaseMovement))]
public class PlayerInputController : MonoBehaviour
{
    private BaseMovement movement;
    private Vector2 moveInput;

    void Start()
    {
        movement = GetComponent<BaseMovement>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        
        movement.MoveTo(direction);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}