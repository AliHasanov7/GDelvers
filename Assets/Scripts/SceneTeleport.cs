using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class SceneTeleport : MonoBehaviour
{
    public string targetSceneName;

    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private string hintText;

    private bool isPlayerInside = false;

    private void Awake()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInside && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TeleportToScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerInputController>() != null)
        {
            isPlayerInside = true;

            if (interactionText != null)
            {
                interactionText.text = hintText;
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerInputController>() != null)
        {
            isPlayerInside = false;

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    private void TeleportToScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            if (interactionText != null) interactionText.gameObject.SetActive(false);
            
            SceneManager.LoadScene(targetSceneName);
        }
    }
}