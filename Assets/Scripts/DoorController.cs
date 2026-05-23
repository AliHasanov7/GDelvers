using UnityEngine;
using UnityEngine.InputSystem; 

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; 
    public float smoothSpeed = 2f; 

    public bool isLocked = false;
    public string requiredKeyID = "GoldKey";

    private bool isPlayerNearby = false;
    private bool isOpen = false;
    
    private Quaternion defaultRotation;
    private Quaternion openRotation;
    private PlayerInventory playerInventory; 

    void Start()
    {
        defaultRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * defaultRotation;
    }

    void Update()
    {
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteractWithDoor();
        }

        Quaternion targetRotation = isOpen ? openRotation : defaultRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    private void TryInteractWithDoor()
    {
        if (isLocked)
        {
            if (playerInventory != null && playerInventory.HasKey(requiredKeyID))
            {
                isLocked = false; // Отпираем дверь навсегда
                isOpen = true;    // Сразу открываем её
            }
        }
        else
        {
            isOpen = !isOpen; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerInventory = null; 
        }
    }
}