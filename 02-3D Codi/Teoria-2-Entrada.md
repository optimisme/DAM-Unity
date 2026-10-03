# Entrada

Referència del **nou Input System** (`UnityEngine.InputSystem`). Requereix el paquet instal·lat i **Active Input Handling = Input System Package (New)** o **Both**.

## Escollir una forma de lectura

| Forma | Ús habitual | Requisit |
|---|---|---|
| `Keyboard.current`, `Mouse.current`, `Gamepad.current` | Controls directes i exemples petits | Comprovar que el dispositiu no és null |
| InputAction | Separar una acció de joc de les tecles o botons | Bindings definits i acció habilitada |
| PlayerInput | Gestionar un conjunt d’accions d’un jugador | Asset d’accions, mapa actiu i mode de notificació |

## Lectura directa

| Expressió | Significat |
|---|---|
| `key.isPressed` | Es manté premuda |
| `key.wasPressedThisFrame` | S’ha premut en aquesta actualització |
| `key.wasReleasedThisFrame` | S’ha alliberat en aquesta actualització |
| `mouse.position.ReadValue()` | Posició del cursor en píxels |
| `mouse.delta.ReadValue()` | Desplaçament acumulat del punter en l’actualització |
| `mouse.scroll.ReadValue()` | Desplaçament de la roda |
| `gamepad.leftStick.ReadValue()` | Vector2 de l’estic |

Dins d’Update, amb `using UnityEngine.InputSystem;`:

```csharp
var keyboard = Keyboard.current;
if (keyboard == null) return;
if (keyboard.spaceKey.wasPressedThisFrame)
    Debug.Log("Prémer espai");
```

## Accions i bindings

| Concepte | Funció |
|---|---|
| Action | Intenció del jugador: Move, Jump, Interact… |
| Binding | Control que activa l’acció: `<Keyboard>/space` |
| Action Map | Grup d’accions: Player, UI… |
| InputActionAsset | Asset `.inputactions` que conté mapes i bindings |
| Button | Acció de botó |
| Value | Valor continu, com un float o Vector2 |
| Pass Through | Notifica canvis dels controls sense seleccionar-ne un únic guanyador |
| Composite 2D Vector | Combina quatre direccions en un Vector2 |

Camps i mètodes d’un component; acció creada per aquest component:

```csharp
private InputAction jump;
void Awake() => jump = new InputAction("Jump", InputActionType.Button, "<Keyboard>/space");
void OnEnable() => jump.Enable();
void OnDisable() => jump.Disable();
void OnDestroy() => jump.Dispose();
void Update()
{
    if (jump.WasPressedThisFrame()) Debug.Log("Saltar");
}
```

## Notificacions

| Forma | Dades rebudes |
|---|---|
| `action.ReadValue<Vector2>()` | Valor llegit quan el codi el necessita |
| `action.started`, `performed`, `canceled` | `InputAction.CallbackContext`; depèn del tipus i les interaccions |
| PlayerInput amb Send Messages | Mètodes com `OnMove(InputValue value)` al mateix GameObject |
| PlayerInput amb Invoke Unity Events | Respostes connectades a l’Inspector amb CallbackContext |

`OnMove` no és un callback automàtic de MonoBehaviour. Amb Send Messages, cal una acció anomenada **Move**, un mapa habilitat i el component PlayerInput configurat. Les accions i els bindings s’editen a l’asset `.inputactions` o es defineixen per codi. [Referència d’Input Actions](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/Actions.html).

**Errors habituals:** barrejar `Input.GetKey` amb el nou sistema sense configurar Both; crear accions repetidament a OnEnable; oblidar Enable; tractar `performed` com un únic clic independentment de la configuració; no donar el focus a Game.

**Demos:** [Objectes](<Demo-0-Objectes.md>) · [Nivells](<Demo-3-Nivells.md>) · [Palanca: Input Actions i PlayerInput](<Demo-5-Palanca.md>).
