# Física i col·lisions

Referència de **física 3D**. Els components i callbacks 2D són diferents i no interactuen amb els 3D.

## Components

| Component / opció | Funció |
|---|---|
| Collider | Forma de contacte; no ha de coincidir exactament amb la malla visible |
| Collider amb Is Trigger | Detecta solapaments sense resposta sòlida |
| Rigidbody dinàmic | Simulació de forces, gravetat i contactes |
| Rigidbody cinemàtic | Moviment dirigit pel codi; no el mouen les forces |
| CharacterController | Moviment controlat amb Move; sense gravetat automàtica |
| Physics Material | Fricció i rebot; diferent del material visual |

## Callbacks

| Mètode | Argument | Situació |
|---|---|---|
| `OnCollisionEnter/Stay/Exit` | Collision | Contacte físic entre colliders sòlids |
| `OnTriggerEnter/Stay/Exit` | Collider | Entrada, permanència o sortida d’un trigger |
| `OnControllerColliderHit` | ControllerColliderHit | Impacte del CharacterController durant Move |

Per a **colliders ordinaris**, els callbacks de col·lisió sòlida requereixen almenys un Rigidbody no cinemàtic. Per als triggers, almenys un collider és trigger i almenys un dels objectes té cos físic; pot ser cinemàtic. Les Layers han de permetre la interacció. [Configuració de triggers](https://docs.unity3d.com/6000.0/Documentation/Manual/collider-interactions-create-trigger.html).

Amb **CharacterController**, utilitza OnControllerColliderHit per als impactes. Per als sensors de les demos, un trigger amb Rigidbody cinemàtic funciona amb el controlador sense afegir Rigidbody al jugador.

## Filtrar contactes

Mètode dins del component que rep el trigger; el tag Player ha d’existir:

```csharp
void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player")) return;
    Debug.Log("Ha entrat el jugador");
}
```

| Filtre | Ús |
|---|---|
| `CompareTag` | Identificar una categoria |
| `TryGetComponent<T>` | Detectar una funcionalitat |
| `other.attachedRigidbody` | Obtenir el cos físic, encara que el collider sigui un fill |
| LayerMask | Limitar consultes com Raycast |
| Layer Collision Matrix | Permetre o impedir contactes entre capes |

## Moviment físic

| Operació | Ús habitual |
|---|---|
| `body.AddForce(...)` | Accelerar un cos dinàmic |
| `body.MovePosition(...)` | Desplaçar un Rigidbody cinemàtic en FixedUpdate |
| `body.linearVelocity` | Velocitat en unitats/segon; Unity 6 |
| `body.isKinematic` | Activar control cinemàtic |
| `body.useGravity` | Gravetat de la simulació |
| `body.constraints` | Bloquejar eixos de posició o rotació |
| `body.interpolation` | Suavitzar la representació entre passos físics |

Moviment cinemàtic dins de **FixedUpdate**; `body`, `destination` i `speed` estan definits:

```csharp
Vector3 next = Vector3.MoveTowards(body.position, destination,
    speed * Time.fixedDeltaTime);
body.MovePosition(next);
```

**Errors habituals:** moure un Rigidbody dinàmic modificant Transform contínuament; pensar que un trigger és sòlid; esperar OnCollisionEnter al controlador; desactivar una placa quan surt un ocupant sense comptar els que hi queden.

**Demos:** [Objectes](<Demo-0-Objectes.md>) · [Mecanismes](<Demo-5-Mecanismes.md>).
