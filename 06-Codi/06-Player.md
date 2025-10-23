# Player

Aquests dos scripts són una combinació de càmera i player, on opcionalment es pot escollir càmera tipus '3rd Person' o bé 'Orbit'


**CameraController.cs**, assigna'l a la **"Main Camera"**:
```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform pivot;

    [Header("Camera Mode")]
    public bool use3rdPersonFollow = false; // TRUE = 3rd person, FALSE = orbit

    [Header("3rd Person Follow Settings")]
    public Vector3 followOffset = new Vector3(0f, 2f, -4f);
    public float followSmoothSpeed = 10f;

    [Header("Distance")]
    public float distance = 4f;
    public float minDistance = 2f, maxDistance = 25f;
    public float zoomSpeed = 10f;

    [Header("Angles")]
    public float minPitch = 10f, maxPitch = 80f;

    [Header("Sensitivity (degrees per pixel)")]
    public float yawPerPixel = 0.25f;
    public float pitchPerPixel = 0.20f;

    [Header("Boost (while Alt/Option held)")]
    public float altBoost = 2.0f;

    private float yaw;
    private float pitch = 35f;
    private Vector3 currentVelocity;

    // Input System actions
    private InputAction lookDelta;
    private InputAction leftBtn;
    private InputAction rightBtn;
    private InputAction altModifier;

    // Estat d'entrada
    private bool _lmbHeld = false;
    private bool _rmbHeld = false;
    private bool _altHeld = false;
    private bool _isOrbiting = false;
    private bool _skipFirstDelta = false;

    // Handlers
    private void OnLmbStarted(InputAction.CallbackContext ctx) { _lmbHeld = true;  TryBeginOrbit(); }
    private void OnLmbCanceled(InputAction.CallbackContext ctx){ _lmbHeld = false; TryEndOrbit(); }

    private void OnRmbStarted(InputAction.CallbackContext ctx) { _rmbHeld = true;  TryBeginOrbit(); }
    private void OnRmbCanceled(InputAction.CallbackContext ctx){ _rmbHeld = false; TryEndOrbit(); }

    private void OnAltStarted(InputAction.CallbackContext ctx) { _altHeld = true;  TryBeginOrbit(); }
    private void OnAltCanceled(InputAction.CallbackContext ctx){ _altHeld = false; TryEndOrbit(); }

    void OnEnable()
    {
        lookDelta = new InputAction("LookDelta", type: InputActionType.Value);
        lookDelta.AddBinding("<Pointer>/delta");
        lookDelta.Enable();

        leftBtn = new InputAction("LMB", type: InputActionType.Button);
        leftBtn.AddBinding("<Mouse>/leftButton");
        leftBtn.started  += OnLmbStarted;
        leftBtn.canceled += OnLmbCanceled;
        leftBtn.Enable();

        rightBtn = new InputAction("RMB", type: InputActionType.Button);
        rightBtn.AddBinding("<Mouse>/rightButton");
        rightBtn.started  += OnRmbStarted;
        rightBtn.canceled += OnRmbCanceled;
        rightBtn.Enable();

        altModifier = new InputAction("Alt", type: InputActionType.Button);
        altModifier.AddBinding("<Keyboard>/leftAlt");
        altModifier.AddBinding("<Keyboard>/rightAlt");
        altModifier.started  += OnAltStarted;
        altModifier.canceled += OnAltCanceled;
        altModifier.Enable();

        Application.focusChanged += OnAppFocusChanged;
    }

    void OnDisable()
    {
        if (lookDelta != null) lookDelta.Disable();

        if (leftBtn != null)  { leftBtn.started  -= OnLmbStarted;  leftBtn.canceled  -= OnLmbCanceled;  leftBtn.Disable(); }
        if (rightBtn != null) { rightBtn.started -= OnRmbStarted;   rightBtn.canceled -= OnRmbCanceled; rightBtn.Disable(); }
        if (altModifier != null){ altModifier.started -= OnAltStarted; altModifier.canceled -= OnAltCanceled; altModifier.Disable(); }

        Application.focusChanged -= OnAppFocusChanged;
        EndOrbitImmediate();
    }

    private void OnAppFocusChanged(bool hasFocus)
    {
        if (!hasFocus) EndOrbitImmediate();
    }

    void Start()
    {
        if (!pivot)
        {
            var go = new GameObject("Pivot");
            pivot = go.transform;
            pivot.position = Vector3.zero;
        }

        if (use3rdPersonFollow && pivot)
        {
            // Mode 3rd person: inicialitza yaw amb la direcció del player
            yaw = pivot.eulerAngles.y;
        }
        else
        {
            // Mode orbit: calcula angles des de la posició inicial
            Vector3 dir = (transform.position - pivot.position).normalized;
            pitch = Mathf.Asin(Mathf.Clamp(dir.y, -0.999f, 0.999f)) * Mathf.Rad2Deg;
            yaw   = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        }
    }

    void Update()
    {
        if (_isOrbiting)
        {
            // Mode manual orbit (RMB o Alt+LMB)
            Vector2 d = lookDelta.ReadValue<Vector2>();
            if (_skipFirstDelta) { d = Vector2.zero; _skipFirstDelta = false; }

            float boost = _altHeld ? altBoost : 1f;
            yaw   += d.x * yawPerPixel   * boost;
            pitch -= d.y * pitchPerPixel * boost;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
        else if (use3rdPersonFollow)
        {
            // Mode 3rd person: segueix la rotació Y del player suaument
            if (pivot)
            {
                float targetYaw = pivot.eulerAngles.y;
                yaw = Mathf.LerpAngle(yaw, targetYaw, followSmoothSpeed * Time.deltaTime);
            }
        }
        // Si use3rdPersonFollow == false i no està orbitant, yaw/pitch es mantenen fixos

        // Zoom (funciona en tots dos modes)
        if (Mouse.current != null)
        {
            float scrollY = Mouse.current.scroll.ReadValue().y;
            if (Mathf.Abs(scrollY) > 0.01f)
            {
                distance -= scrollY * (zoomSpeed * 0.01f);
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }
        }
    }

    void LateUpdate()
    {
        if (!pivot) return;

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPos = pivot.position + rot * new Vector3(0f, followOffset.y, -distance);

        if (use3rdPersonFollow && !_isOrbiting)
        {
            // Suavitza el moviment en mode 3rd person
            Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, 1f / followSmoothSpeed);
            transform.SetPositionAndRotation(smoothedPos, rot);
        }
        else
        {
            // Posició directa en mode orbit o quan està orbitant manualment
            transform.SetPositionAndRotation(targetPos, rot);
        }
    }

    public void SetPivot(Transform newPivot) => pivot = newPivot;

    private bool OrbitCondition() => _rmbHeld || (_altHeld && _lmbHeld);

    private void TryBeginOrbit()
    {
        if (_isOrbiting) return;
        if (OrbitCondition())
        {
            _isOrbiting = true;
            _skipFirstDelta = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void TryEndOrbit()
    {
        if (!_isOrbiting) return;
        if (!OrbitCondition())
        {
            EndOrbitImmediate();
        }
    }

    private void EndOrbitImmediate()
    {
        _isOrbiting = false;
        _skipFirstDelta = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Mètode públic per canviar de mode en temps d'execució
    public void SetCameraMode(bool enable3rdPerson)
    {
        use3rdPersonFollow = enable3rdPerson;
        
        if (enable3rdPerson && pivot && !_isOrbiting)
        {
            // Sincronitza yaw amb el player quan canvies a 3rd person
            yaw = pivot.eulerAngles.y;
        }
    }
}
```

**PlayerMove.cs**, assigna'l al **"Player"**:
```csharp
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.6f;
    public float turnSpeed = 720f;   // graus/segon
    public float gravity = -9.81f;
    public float inputDeadzone = 0.05f;

    [Header("Camera (opcional)")]
    public Transform cameraTransform;     // si és null, usarà Camera.main

    private CharacterController cc;
    private float verticalVel;

    // New Input System
    private InputAction moveAction;

    void OnEnable()
    {
        // Defineix WASD + Fletxes + Stick esquerre
        moveAction = new InputAction("Move", type: InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w").With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s").With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a").With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d").With("Right", "<Keyboard>/rightArrow");
        moveAction.AddBinding("<Gamepad>/leftStick");
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
    }

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Start()
    {
        // Assegura un transform de càmera
        if (!cameraTransform && Camera.main) cameraTransform = Camera.main.transform;

        // Si tens CameraController a l'escena, fes que orbiti el player
        var orbit = FindFirstObjectByType<CameraController>(); // <-- substitució
        if (orbit != null) orbit.SetPivot(transform);
    }

    void Update()
    {
        if (!cameraTransform && Camera.main) cameraTransform = Camera.main.transform;

        // Input
        Vector2 mv = moveAction.ReadValue<Vector2>();
        if (mv.magnitude < inputDeadzone) mv = Vector2.zero;

        // Direccions CÀMERA → mapeig WASD relatiu al punt de vista
        Vector3 camFwd = cameraTransform ? cameraTransform.forward : Vector3.forward;
        Vector3 camRight = cameraTransform ? cameraTransform.right : Vector3.right;
        camFwd.y = 0f; camRight.y = 0f;
        camFwd.Normalize(); camRight.Normalize();

        // Vector de moviment al pla XZ relatiu a càmera
        Vector3 moveDir = (camFwd * mv.y + camRight * mv.x);
        if (moveDir.sqrMagnitude > 1e-6f) moveDir.Normalize();

        // Gravetat bàsica i "enganxar" a terra
        if (cc.isGrounded && verticalVel < 0f) verticalVel = -1f;
        else verticalVel += gravity * Time.deltaTime;

        // Aplicar moviment
        Vector3 velocity = moveDir * moveSpeed + Vector3.up * verticalVel;
        cc.Move(velocity * Time.deltaTime);

        // ROTACIÓ només sobre Y cap a la direcció de cursa (estil Mario)
        if (moveDir.sqrMagnitude > 1e-6f)
        {
            Quaternion target = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, target, turnSpeed * Time.deltaTime
            );
        }
    }
}
```