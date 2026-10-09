using UnityEngine;
using UnityEngine.InputSystem;

// Gira abans que ForatController calculi l'oclusió al seu LateUpdate.
[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(Camera))]
public class DemoOrbitCamera : MonoBehaviour
{
    public Vector3 center = new Vector3(0, 1.7f, 0);
    public float minimumDistance = 3f;
    public float maximumDistance = 35f;
    public float rotationSensitivity = 0.2f;
    public float zoomSensitivity = 0.0015f;
    public float minimumElevation = 5f;
    public float maximumElevation = 85f;

    Camera view;
    public System.Func<Vector2, bool> IsPointerOverControls;
    Vector3 initialPosition;
    Quaternion initialRotation;
    float distance, azimuth, elevation;

    void Awake()
    {
        view = GetComponent<Camera>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        ReadPosition();
    }

    void ReadPosition()
    {
        Vector3 offset = transform.position - center;
        distance = Mathf.Clamp(offset.magnitude, minimumDistance, maximumDistance);
        azimuth = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
        elevation = Mathf.Asin(Mathf.Clamp(offset.normalized.y, -1f, 1f)) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (Keyboard.current != null && Keyboard.current.homeKey.wasPressedThisFrame) ResetView();
        var mouse = Mouse.current;
        if (mouse == null || !view.pixelRect.Contains(mouse.position.ReadValue())) return;
        Vector2 point = mouse.position.ReadValue();
        point.y = Screen.height - point.y; // OnGUI té l'origen a dalt.
        if (IsPointerOverControls != null && IsPointerOverControls(point)) return;
        Vector2 delta = mouse.rightButton.isPressed ? mouse.delta.ReadValue() : Vector2.zero;
        ApplyInput(delta, mouse.scroll.ReadValue().y);
    }

    // Delta en píxels i scroll d'Input System. No es multipliquen per deltaTime.
    public void ApplyInput(Vector2 drag, float scroll)
    {
        if (drag == Vector2.zero && scroll == 0f) return;
        azimuth = Mathf.Repeat(azimuth + drag.x * rotationSensitivity, 360f);
        elevation = Mathf.Clamp(elevation - drag.y * rotationSensitivity, minimumElevation, maximumElevation);
        distance = Mathf.Clamp(distance * Mathf.Exp(-Mathf.Clamp(scroll * zoomSensitivity, -10f, 10f)), minimumDistance, maximumDistance);
        float yaw = azimuth * Mathf.Deg2Rad, pitch = elevation * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Sin(yaw) * Mathf.Cos(pitch), Mathf.Sin(pitch), Mathf.Cos(yaw) * Mathf.Cos(pitch));
        transform.position = center + offset * distance;
        transform.LookAt(center, Vector3.up);
    }

    public void ResetView()
    {
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        ReadPosition();
    }
}
