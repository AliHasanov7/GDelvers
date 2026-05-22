using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    public bool isLocked = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor(PlayerInteraction player)
    {
        if (isLocked)
        {
            if (player.HasKeyInHand())
            {
                isLocked = false;
                Debug.Log("Дверь отперта ключом!");
            }
            else
            {
                Debug.Log("Дверь заперта! Нужен ключ в руке.");
                return; 
            }
        }

        isOpen = !isOpen;
        animator.SetBool("IsOpen", isOpen);
    }
}