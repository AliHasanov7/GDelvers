using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction1 : MonoBehaviour
{
    public GameObject interactText;
    public string SceneName="Room1";

    private bool playerNear = false;

    private void Start()
    {
        interactText.SetActive(false);
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            interactText.SetActive(false);
        }
    }
}