using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -6f);

    [Header("Rotación")]
    public float sensitivity = 3f;
    public float minPitch = -35f;
    public float maxPitch = 60f;

    [Header("Zoom")]
    public float minZoom = -2f;    // Más cerca
    public float maxZoom = -10f;   // Más lejos
    public float zoomSpeed = 5f;

    private float yaw = 0f;
    private float pitch = 10f;
    private float currentZoomZ; // la parte Z del offset (profundidad)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentZoomZ = offset.z;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoomZ += scroll * zoomSpeed;
        currentZoomZ = Mathf.Clamp(currentZoomZ, maxZoom, minZoom);

        // Actualizar offset con nuevo zoom
        Vector3 zoomedOffset = new Vector3(offset.x, offset.y, currentZoomZ);

        // Posición y rotación
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * zoomedOffset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
