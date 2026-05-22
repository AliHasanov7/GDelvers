using UnityEngine;

public class DoorRaycast : MonoBehaviour
{
    public float distance = 2f;
    public GameObject interactUI;

    void Start()
    {
        interactUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                interactUI.SetActive(true);
            }
            else
            {
                interactUI.SetActive(false);
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }
}