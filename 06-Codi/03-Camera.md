# Camera

Tot i que la càmera es programa com els altres objectes de **Unity**, té paràmetres que la fan especial.

- **Projecció**: amb perspectiva o ortogràfica (sense perspectiva)

```csharp
using UnityEngine;

public class CameraProjectionCheck : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        if (cam.orthographic)
            Debug.Log("La càmera és ORTOGRÀFICA");
        else
            Debug.Log("La càmera és en PERSPECTIVA");
    }
}
```

- **Field of view (FOV)**: angle d'obertura de la càmera (quanta escena es veu, conceptualment un zoom)

```csharp
using UnityEngine;

public class CameraFOVKeys : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            cam.fieldOfView -= 40f * Time.deltaTime;
            Debug.Log("Up - Camp de visió actual: " + fov);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            cam.fieldOfView += 40f * Time.deltaTime;
            Debug.Log("Down - Camp de visió actual: " + fov);
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 15f, 90f);
    }
}
```

## Exemples

Cada joc tindrà la seva pròpia lògica. Aquí van alguns exemples:

### Exemple 0: Seguir al jugador

Cal assignar l'objecte a seguir, arrossegant-lo sobre el paràmtetre **"target"**

```csharp
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                        // Objecte a seguir (jugador)
    public Vector3 offset = new Vector3(0, 5, -8);  // Distància darrera del jugador
    public float smoothSpeed = 0.125f;              // Velocitat d'animació cap al punt desitjat

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset; // Posició ideal darrere del jugador
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Animació suau
        transform.LookAt(target);
    }
}
```

### Exemple 1: Rotar i acostar-se al jugador

Cal assignar l'objecte a seguir, arrossegant-lo sobre el paràmtetre **"target"**

Per rotar:

- Mouse + Tecla dreta apretada

Per zoom:

- Rodeta del mouse (o scroll)

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;                 // Objecte a seguir
    public float distance = 5f;              // Distància entre la càmera i el jugador
    public float zoomSpeed = 10f;            // Velocitat d’apropament/allunyament
    public float rotationSpeed = 250f;       // Sensibilitat de rotació
    public Vector2 pitchLimits = new Vector2(-20, 60); // Límits verticals de rotació (mín, màx en graus)

    private float yaw;                       // Angle de rotació horitzontal acumulat (eix Y)
    private float pitch = 20f;               // Angle vertical inicial (eix X)

    void LateUpdate()
    {
        if (target == null) return;          // Si no hi ha objectiu, no fer res

        // --- Rotació amb el botó dret del ratolí ---
        var mouse = Mouse.current;
        if (mouse.rightButton.isPressed)
        {
            // Actualitza els angles de rotació segons el moviment del ratolí
            yaw += mouse.delta.x.ReadValue() * rotationSpeed * Time.deltaTime;
            pitch -= mouse.delta.y.ReadValue() * rotationSpeed * Time.deltaTime;

            // Limita la rotació vertical per evitar girs de càmera extrems
            pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
        }

        // --- Zoom amb la roda del ratolí ---
        float zoomDelta = mouse.scroll.ReadValue().y * zoomSpeed * (distance * 0.3f) * Time.deltaTime;
        distance -= zoomDelta;      // moviment del zoom segons la distància a l'objecte
        distance = Mathf.Clamp(distance, 2f, 20f);


        // --- Càlcul de la posició i orientació ---
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0); // Construeix la rotació a partir dels angles
        Vector3 direction = rotation * Vector3.back * distance; // Direcció oposada al vector “forward” segons la rotació
        transform.position = target.position + direction;       // Col·loca la càmera a la distància calculada
        transform.LookAt(target);                               // Fa que la càmera miri sempre cap al jugador
    }
}
```

### Exemple 2: Observar al voltant del jugador

Cal assignar l'objecte a seguir, arrossegant-lo sobre el paràmtetre **"target"**

Per canviar els angles:

- Mouse + Botó dret apretat
- Tecles de direcció

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFreeLook : MonoBehaviour
{
    [Header("Rotació amb ratolí")]
    public bool requireRightMouse = true;        // Si true, només gira si mantens premut RMB
    public float mouseSensitivity = 120f;        // Velocitat de gir amb ratolí
    public Vector2 pitchLimits = new Vector2(-85f, 85f); // Límits verticals

    [Header("Rotació amb teclat")]
    public float keyYawSpeed = 120f;             // graus/seg a esquerra-dreta (yaw)
    public float keyPitchSpeed = 120f;           // graus/seg a amunt-avall (pitch)

    private float yaw;   // acumulat eix Y (dreta/esquerra)
    private float pitch; // acumulat eix X (amunt/avall)

    void Start()
    {
        var e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;
    }

    void LateUpdate()
    {
        var mouse = Mouse.current;
        var kb = Keyboard.current;

        // --- ROTACIÓ AMB RATOLÍ ---
        bool canRotateWithMouse = !requireRightMouse || (mouse != null && mouse.rightButton.isPressed);
        if (mouse != null && canRotateWithMouse)
        {
            Vector2 m = mouse.delta.ReadValue();
            yaw   += m.x * mouseSensitivity * Time.deltaTime;
            pitch -= m.y * mouseSensitivity * Time.deltaTime;
        }

        // --- ROTACIÓ AMB TECLAT (fletxes) ---
        if (kb != null)
        {
            float yawInput =
                (kb.leftArrowKey.isPressed ? -1f : 0f) +
                (kb.rightArrowKey.isPressed ?  1f : 0f);

            float pitchInput =
                (kb.upArrowKey.isPressed ? 1f : 0f) +
                (kb.downArrowKey.isPressed ? -1f : 0f);

            yaw   += yawInput   * keyYawSpeed   * Time.deltaTime;
            pitch += pitchInput * keyPitchSpeed * Time.deltaTime;
        }

        // Límits i aplicació
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
```

### Exemple 3: Moure la càmera amb els inputs

Per canviar els angles:

Cal assignar l'objecte a seguir, arrossegant-lo sobre el paràmtetre **"target"**

Per canviar els angles:

- Mouse + Botó dret apretat

Per anar o allunyar-se d'on mira la càmera:

- Tecles de direcció

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFreeFly : MonoBehaviour
{
    [Header("Mouse look")]
    public float mouseSensitivity = 120f;      // Sensibilitat del moviment del ratolí
    public Vector2 pitchLimits = new Vector2(-85f, 85f); // Límits de rotació 

    [Header("Movement")]
    public float moveSpeed = 6f;               // Velocitat de desplaçament (m/s)
    public float boostMultiplier = 3f;         // Multiplicador de velocitat quan es manté premuda la tecla Shift

    private float yaw;                         // Rotació horitzontal (eix Y)
    private float pitch;                       // Rotació vertical (eix X)


    void Start()
    {
        var e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;
    }

    void LateUpdate()
    {
        var mouse = Mouse.current;
        var kb = Keyboard.current;

        // --- Mira amb RMB (evita capturar sempre el ratolí) ---
        if (mouse.rightButton.isPressed)
        {
            yaw   += mouse.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
            pitch -= mouse.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        // --- Input de moviment (WASD + amunt/avall) ---
        float x = (kb.aKey.isPressed ? -1f : 0f) + (kb.dKey.isPressed ? 1f : 0f);
        float z = (kb.sKey.isPressed ? -1f : 0f) + (kb.wKey.isPressed ? 1f : 0f);
        float y = (kb.leftCtrlKey.isPressed ? -1f : 0f) + (kb.spaceKey.isPressed ? 1f : 0f);

        Vector3 move = (transform.right * x) + (transform.forward * z) + (transform.up * y);

        float speed = moveSpeed * (kb.leftShiftKey.isPressed ? boostMultiplier : 1f);
        transform.position += move.normalized * speed * Time.deltaTime;
    }
}
```