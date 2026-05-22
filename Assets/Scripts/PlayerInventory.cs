using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform handPosition;

    private GameObject itemInRange;
    private DoorController doorInRange;

    private GameObject currentItemInHand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorInRange != null)
            {
                doorInRange.ToggleDoor(this);
            }
            
            else if (itemInRange != null && currentItemInHand == null)
            {
                PickUpItem();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentItemInHand != null)
            {
                DropItem();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody attachedRigidbody = other.GetComponentInParent<Rigidbody>();

        if (attachedRigidbody != null && attachedRigidbody.CompareTag("Key"))
        {
            itemInRange = attachedRigidbody.gameObject;
        }

        if (other.CompareTag("Door"))
        {
            doorInRange = other.GetComponentInParent<DoorController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody attachedRigidbody = other.GetComponentInParent<Rigidbody>();
    
        if (attachedRigidbody != null && attachedRigidbody.gameObject == itemInRange)
        {
            itemInRange = null;
        }
        
        if (other.CompareTag("Door"))
        {
            doorInRange = null;
        }
    }

    private void PickUpItem()
    {
        currentItemInHand = itemInRange;
        
        itemInRange = null;

        Rigidbody itemRigidbody = currentItemInHand.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = true;
        }

        currentItemInHand.transform.SetParent(handPosition);

        currentItemInHand.transform.localPosition = Vector3.zero;
        currentItemInHand.transform.localRotation = Quaternion.identity;
    }
    
    public bool HasKeyInHand()
    {
        if (currentItemInHand != null && currentItemInHand.CompareTag("Key"))
        {
            return true;
        }
        return false;
    }

    private void DropItem()
    {
        if (currentItemInHand == null)
        {
            return;
        }
        
        currentItemInHand.transform.SetParent(null);
        
        Rigidbody itemRigidbody = currentItemInHand.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = false;
        }

        currentItemInHand = null;
    }
}