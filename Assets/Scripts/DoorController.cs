using UnityEngine;
using UnityEngine.InputSystem; 

public class DoorController : MonoBehaviour
{
    [Header("Настройки двери")]
    public float openAngle = 90f; 
    public float smoothSpeed = 2f; 

    private bool isPlayerNearby = false;
    private bool isOpen = false;
    
    private Quaternion defaultRotation;
    private Quaternion openRotation;

    void Start()
    {
        defaultRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * defaultRotation;
    }

    void Update()
    {
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isOpen = !isOpen; 
        }

        Quaternion targetRotation = isOpen ? openRotation : defaultRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}