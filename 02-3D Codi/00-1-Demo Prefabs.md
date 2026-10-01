# Demo Prefabs

En aquesta demo:

- Un generador crea un objecte recollible.
- El jugador recull l'objecte en tocar-lo.
- Els objectes recollits formen una cua darrere del jugador.
- Quan es recull l'objecte del generador, al cap de 2 segons se'n genera un altre.

# Crear l'escena

Crea una escena **Basic**, amb **Main Camera** i **Directional Light** (o crea-les amb **GameObject > Camera** i **GameObject > Light > Directional Light** si l’escena és buida), i desa-la com a **DemoPrefabs**.

Comprova que **Input System** està instal·lat a **Window > Package Manager > Unity Registry**. A **Edit > Project Settings > Player > Other Settings**, **Active Input Handling** ha de ser **Input System Package (New)** o **Both**; reinicia Unity si ho demana.

Mou la càmera a:

```text
Position X: 0
Position Y: 5
Position Z: -5
Rotation: 45, 0, 0
Field of View: 60
```

Afegeix:

- Un **Plane** a `(0, 0, 0)`, amb rotació zero i escala `(1, 1, 1)`
- Un **Cube** que farà de jugador, a 
  ```text
  Position X: 0
  Position Y: 0.5
  Position Z: 0
  ```
- Un objecte buit amb **"Create Empty"** i anomena'l **ItemGenerator**

Al **Cube**:

- Canvia el nom a `Player`
- Afegeix el tag `Player`
- Afegeix un component **Character Controller** amb els valors següents.
- Esborra el `Box Collider` que té per defecte: el `Character Controller` ja és suficient. Deixa el Player sense Rigidbody.

```text
Center: 0, 0, 0
Height: 1
Radius: 0.5
```

# Crear els scripts

Crea i completa els quatre scripts abans de configurar els components a l’Inspector.

Crea l'script **PrefabPlayerMovement.cs** i afegeix-lo al `Player`. El nom evita confondre’l amb el PlayerMovement d’altres demos.

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PrefabPlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        Vector3 movement = Vector3.zero;

        if (Keyboard.current.upArrowKey.isPressed)
            movement += Vector3.forward;

        if (Keyboard.current.downArrowKey.isPressed)
            movement += Vector3.back;

        if (Keyboard.current.leftArrowKey.isPressed)
            movement += Vector3.left;

        if (Keyboard.current.rightArrowKey.isPressed)
            movement += Vector3.right;

        controller.Move(
            movement.normalized * speed * Time.deltaTime
        );
    }
}
```

Crea l'script **PlayerQueue.cs** i afegeix-lo al `Player`.

```csharp
using UnityEngine;
using System.Collections.Generic;

public class PlayerQueue : MonoBehaviour
{
    public float distance = 0.6f;
    public float followSpeed = 10f;

    private List<Transform> items = new List<Transform>();

    // Add an item to the queue
    public void AddItem(Transform item)
    {
        items.Add(item);
    }

    // Update the queue after the player has moved
    void LateUpdate()
    {
        Transform target = transform;

        // Each item follows the previous one
        foreach (Transform item in items)
        {
            Vector3 direction = item.position - target.position;

            if (direction.magnitude > distance)
            {
                Vector3 targetPosition =
                    target.position + direction.normalized * distance;

                item.position = Vector3.Lerp(
                    item.position,
                    targetPosition,
                    followSpeed * Time.deltaTime
                );
            }

            target = item;
        }
    }
}
```

Crea l'script **Collectable.cs**:

```csharp
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemGenerator generator;

    private bool collected = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectat amb: " + other.name);

        if (collected || !other.CompareTag("Player"))
            return;

        collected = true;

        PlayerQueue queue = other.GetComponent<PlayerQueue>();
        queue.AddItem(transform);
        Debug.Log("Objecte recollit!");

        if (generator != null)
            generator.ItemCollected();
    }
}
```

Crea l'script **ItemGenerator.cs** i assigna'l a l'objecte buit `ItemGenerator`:

```csharp
using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour
{
    public GameObject itemPrefab;
    public float generateDelay = 2f;

    private GameObject currentItem;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        currentItem = Instantiate(
            itemPrefab,
            transform.position,
            Quaternion.identity
        );

        Collectable collectable = currentItem.GetComponent<Collectable>();
        collectable.generator = this;
    }

    public void ItemCollected()
    {
        currentItem = null;
        StartCoroutine(GenerateDelayed());
    }

    IEnumerator GenerateDelayed()
    {
        yield return new WaitForSeconds(generateDelay);
        Generate();
    }
}
```

# Objectes recollibles (Prefab)

Afegeix un **3D Object > Sphere**.

Configura'l:

- Nom: `Collectable`
- Escala: aproximadament `0.5, 0.5, 0.5`
- Crea el tag `Collectable` a **Inspector > Tag > Add Tag** i després assigna’l a l’esfera
- Activa **Is Trigger** al seu `Sphere Collider`
- Afegeix un **Rigidbody**
- Activa **Is Kinematic**
- Desactiva **Use Gravity**

Assigna l'script `Collectable.cs` a aquesta esfera. Deixa el seu camp **Generator** a **None**: ItemGenerator l’assigna per codi quan crea cada instància.

Arrossega `Collectable` des de la jerarquia fins a **Assets** per convertir-lo en un **Prefab**.

**IMPORTANT:** **Esborra** després l'objecte `Collectable` de l'escena.

# Generador d'objectes

Selecciona l'objecte buit `ItemGenerator`.

Situa'l, per exemple, a:

```text
X: 0
Y: 0.5
Z: 3
```

Comprova que `ItemGenerator.cs` està afegit una sola vegada a `ItemGenerator`.

A l'Inspector:

![ItemGenerator amb el prefab Collectable assignat i Generate Delay a 2](assets/demoprefabs-generador.png)

- Arrossega el prefab **Collectable** des de *Assets* al camp **Item Prefab** del component **Item Generator**
- Deixa **Generate Delay** a `2`

# Provar la demo

Comprova que:

- `Player` té:
  - Tag `Player`
  - `Character Controller`
  - `PrefabPlayerMovement`
  - `PlayerQueue`

- El prefab `Collectable` té:
  - Tag `Collectable`
  - `Sphere Collider` amb **Is Trigger**
  - `Rigidbody` amb **Is Kinematic**
  - `Collectable`

- `ItemGenerator` té:
  - `ItemGenerator.cs`
  - El prefab assignat a **Item Prefab**

Fes **Play**, clica la vista **Game** i mou el jugador amb les fletxes fins a l’objecte del generador. Després allunya-te’n perquè es vegi com el recollible el segueix.

El funcionament ha de ser:

```text
Es genera un objecte
        ↓
El jugador el toca
        ↓
S'afegeix a la cua
        ↓
Espera 2 segons
        ↓
Es genera un altre objecte
        ↓
El jugador el recull
        ↓
La cua creix
```

![Tres objectes recollits a la cua i un altre disponible al generador](assets/demoprefabs-cua.png)

En aturar Play, desapareixen les instàncies creades durant la partida. En tornar a fer Play, la cua comença buida i el generador crea un únic recollible.
