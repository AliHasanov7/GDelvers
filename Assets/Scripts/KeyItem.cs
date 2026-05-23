using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeyItem : MonoBehaviour
{
    public string keyID = "GoldKey";

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            
            if (inventory != null)
            {
                inventory.AddKey(keyID);
                
                Destroy(gameObject); 
            }
        }
    }
}