using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCamera : MonoBehaviour
{
    public Transform target;
    public Transform firstPersonSocket;

    public float distance = 15.0f;
    public float pitchAngle = 30.0f;
    public float yawAngle = 45.0f;
    public float smoothTime = 0.2f;
    
    public float isoFOV = 6f; 

    public float mouseSensitivity = 0.1f;
    public float minPitch = -60f;
    public float maxPitch = 60f;

    private Vector3 currentVelocity;
    private Camera cam;
    private bool isFirstPerson = false;

    private float fpYaw;
    private float fpPitch;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.orthographic = true;
            cam.orthographicSize = isoFOV;
        }
        
        transform.rotation = Quaternion.Euler(pitchAngle, yawAngle, 0f);
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (!isFirstPerson)
        {
            if (cam != null) cam.orthographicSize = isoFOV;

            Quaternion rotation = Quaternion.Euler(pitchAngle, yawAngle, 0f);
            Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
        else
        {
            if (firstPersonSocket == null) return;

            transform.position = firstPersonSocket.position;

            if (Mouse.current != null)
            {
                Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
                
                fpYaw += mouseDelta.x;
                fpPitch -= mouseDelta.y;
                fpPitch = Mathf.Clamp(fpPitch, minPitch, maxPitch);
            }

            transform.rotation = Quaternion.Euler(fpPitch, fpYaw, 0f);
        }
    }

    public void SetFirstPersonMode(bool active)
    {
        isFirstPerson = active;

        if (cam == null) cam = GetComponent<Camera>();

        if (isFirstPerson)
        {
            cam.orthographic = false;
            cam.fieldOfView = 60f; 

            fpYaw = target.eulerAngles.y;
            fpPitch = 0f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            cam.orthographic = true;
            cam.orthographicSize = isoFOV;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}