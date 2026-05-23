using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputController))]
public class BloggerCamera : MonoBehaviour
{
    [SerializeField] private GameObject viewfinderRoot; 
    [SerializeField] private GameObject flashUIPanel;   

    [SerializeField] private Light flashLight;          
    [SerializeField] private float flashDuration = 0.1f; 

    private PlayerInputController playerInput;
    private IsometricCamera isoCamera; 
    private BaseMovement baseMovement; 
    private bool isCameraModeActive = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInputController>();
        baseMovement = GetComponent<BaseMovement>();
        isoCamera = Camera.main.GetComponent<IsometricCamera>();

        if (viewfinderRoot != null) viewfinderRoot.SetActive(false);
        if (flashUIPanel != null) flashUIPanel.SetActive(false);
        if (flashLight != null) flashLight.enabled = false;
    }

    private void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                SetCameraMode(true);
            }
            if (Keyboard.current.shiftKey.wasReleasedThisFrame)
            {
                SetCameraMode(false);
            }

            if (isCameraModeActive && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                TakePhoto();
            }
        }

        if (isCameraModeActive && isoCamera != null)
        {
            Vector3 camRotation = Camera.main.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(0, camRotation.y, 0);
        }
    }

    private void SetCameraMode(bool active)
    {
        isCameraModeActive = active;

        if (viewfinderRoot != null) viewfinderRoot.SetActive(active);

        if (isoCamera != null)
        {
            isoCamera.SetFirstPersonMode(active);
        }

        if (baseMovement != null)
        {
            baseMovement.rotationSpeed = active ? 0f : 720f;
        }
    }

    private void TakePhoto()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (flashUIPanel != null) flashUIPanel.SetActive(true);
        if (flashLight != null) flashLight.enabled = true;

        yield return new WaitForSeconds(flashDuration);

        if (flashUIPanel != null) flashUIPanel.SetActive(false);
        if (flashLight != null) flashLight.enabled = false;
    }
}