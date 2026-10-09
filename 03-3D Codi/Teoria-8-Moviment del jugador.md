# Moviment del jugador

El moviment combina **entrada → direcció → velocitat → desplaçament**. La càmera i l’animació poden utilitzar el resultat sense formar part del mateix script.

## Escollir el controlador

| Opció | Ús | Gravetat i col·lisions |
|---|---|---|
| Transform | Moviment visual o sense resposta física | No resol col·lisions per si sol |
| CharacterController | Personatge dirigit per codi | Move respecta colliders; gravetat calculada per codi |
| Rigidbody dinàmic | Personatge governat per forces | Gravetat i resposta física de la simulació |

No combinis CharacterController i Rigidbody dinàmic per governar el mateix jugador en aquests exemples.

## Direcció i velocitat

| Concepte | Significat |
|---|---|
| Direcció | Cap on es mou; Vector3 |
| Velocitat | Direcció × rapidesa, en unitats/segon |
| Desplaçament | Velocitat × interval de temps |
| `Vector3.normalized` | Direcció de longitud 1; perd la intensitat de l’entrada |
| `Vector2.ClampMagnitude(input, 1f)` | Limita diagonals conservant la intensitat analògica |

Dins d’Update; `speed` és la rapidesa en unitats/segon i `input` un Vector2 obtingut del [sistema d’entrada](<Teoria-2-Entrada.md>):

```csharp
input = Vector2.ClampMagnitude(input, 1f);
Vector3 direction = new Vector3(input.x, 0f, input.y);
Vector3 velocity = direction * speed;
```

## Moviment relatiu a la càmera

Alternativa al càlcul anterior; `view` és el Transform d’una càmera inclinada, no completament vertical:

```csharp
Vector3 forward = Vector3.ProjectOnPlane(view.forward, Vector3.up).normalized;
Vector3 right = Vector3.ProjectOnPlane(view.right, Vector3.up).normalized;
Vector3 direction = forward * input.y + right * input.x;
```

Projectar sobre el pla XZ evita avançar cap amunt o cap avall quan la càmera està inclinada. Limita prèviament la magnitud d’input.

## CharacterController i gravetat

Requereix una referència `controller`, els camps `speed`, `gravity` negativa i `verticalSpeed`, i la direcció calculada. Fragment dins d’Update:

```csharp
if (controller.isGrounded && verticalSpeed < 0f)
    verticalSpeed = -2f;
verticalSpeed += gravity * Time.deltaTime;
Vector3 velocity = direction * speed + Vector3.up * verticalSpeed;
controller.Move(velocity * Time.deltaTime);
```

`Move` rep un **desplaçament**, no una velocitat. `isGrounded` informa del contacte de l’últim moviment; la petita velocitat descendent ajuda a mantenir el contacte.

## Orientació

Dins d’Update; `direction` és el moviment horitzontal i `turnSpeed` s’expressa en graus/segon:

```csharp
if (direction.sqrMagnitude > 0.001f)
{
    Quaternion target = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.RotateTowards(transform.rotation,
        target, turnSpeed * Time.deltaTime);
}
```

## Configuració habitual

| Propietat | Què controla |
|---|---|
| Center, Height, Radius | Forma del CharacterController; ha de correspondre al model |
| Step Offset | Alçada d’esglaó que pot superar |
| Slope Limit | Pendent màxim transitable |
| Skin Width | Marge de contacte; no és l’alçada del personatge |
| Min Move Distance | Desplaçament mínim acceptat; normalment 0 en aquestes demos |

**Errors habituals:** duplicar el collider de la càpsula amb un CharacterController; multiplicar dues vegades per deltaTime; normalitzar un estic i perdre el moviment lent; moure la càmera abans d’actualitzar el jugador; esperar que una plataforma mòbil transporti automàticament el controlador.

**Demos:** [Nivells](<Demo-4-Nivells.md>) · [Mecanismes](<Demo-5-Mecanismes.md>). **Relacionat:** [Física](<Teoria-4-Fisica i collisions.md>) · [Càmera](<Teoria-3-Camera.md>).
