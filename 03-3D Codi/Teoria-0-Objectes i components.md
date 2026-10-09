# Objectes i components

Referència per a **Unity 6.6**. Els fragments són independents: els camps van dins d’un `MonoBehaviour` i les instruccions dins del mètode indicat. No són scripts complets.

## Conceptes

| Element | Què representa |
|---|---|
| GameObject | Objecte de l’escena; conté components |
| Transform | Posició, rotació, escala i relació amb pare i fills |
| Component | Funcionalitat associada a un GameObject: Light, Collider, Camera… |
| MonoBehaviour | Base dels scripts que Unity executa com a components |
| Prefab | Asset reutilitzable que serveix de plantilla per crear instàncies |

`gameObject` identifica l’objecte del component; `transform`, el seu Transform. Els components s’obtenen mitjançant [referències](<Teoria-1-Referencies i jerarquia.md>).

## Activació i identificació

| Expressió | Ús |
|---|---|
| `gameObject.SetActive(false)` | Desactivar l’objecte i, efectivament, els seus fills |
| `gameObject.activeSelf` | Estat local d’activació |
| `gameObject.activeInHierarchy` | Estat efectiu, tenint en compte els pares |
| `enabled = false` | Desactivar aquest MonoBehaviour; el GameObject continua actiu |
| `gameObject.name` | Nom; no ha de ser únic |
| `CompareTag("Player")` | Comparar el tag; ha d’existir al projecte |
| `gameObject.layer` | Capa numèrica per a filtres de renderització o física |
| `gameObject.isStatic` | Indicadors d’optimització; no impedeixen moure l’objecte per codi |

## Transform

| Propietat | Significat |
|---|---|
| `position` / `localPosition` | Posició global / relativa al pare |
| `rotation` / `localRotation` | Rotació global / relativa al pare, com a Quaternion |
| `eulerAngles` / `localEulerAngles` | Rotació expressada en graus |
| `localScale` | Escala relativa al pare |
| `lossyScale` | Aproximació de l’escala global; només lectura |
| `forward`, `right`, `up` | Eixos de l’objecte expressats en coordenades globals |

Instruccions dins d’un mètode:

```csharp
transform.position = new Vector3(0, 2, 0);
transform.rotation = Quaternion.Euler(0, 90, 0);
transform.localScale = Vector3.one * 2f;
```

## Camps i Inspector

| Declaració o atribut | Efecte |
|---|---|
| `public float speed = 3f;` | Accessible des d’altres classes; Unity el serialitza si el tipus és compatible |
| `[SerializeField] private float speed = 3f;` | Camp privat serialitzat i editable a l’Inspector |
| `[Header("Moviment")]` | Títol de grup a l’Inspector |
| `[Tooltip("Metres per segon")]` | Text d’ajuda |
| `[Range(0f, 10f)]` | Lliscador a l’Inspector; no limita assignacions fetes per codi |
| `[RequireComponent(typeof(Rigidbody))]` | Atribut de classe: afegeix la dependència quan s’afegeix l’script |
| `[DisallowMultipleComponent]` | Atribut de classe: impedeix duplicar el component |

`public` controla l’accés en C#; `[SerializeField]` controla la serialització. Les propietats C# no apareixen automàticament a l’Inspector.

## Cicle de vida

| Mètode | Quan / per a què |
|---|---|
| `Awake()` | Inicialització de la instància; obtenir components propis |
| `OnEnable()` | Cada vegada que el component queda habilitat i actiu; subscriure esdeveniments |
| `Start()` | Una vegada, abans del primer Update del component habilitat |
| `Update()` | Cada fotograma; entrada i lògica de joc |
| `FixedUpdate()` | Passos fixos de física; zero, un o diversos per fotograma |
| `LateUpdate()` | Després d’Update; seguiment de càmera |
| `OnDisable()` | Desactivació; retirar subscripcions |
| `OnDestroy()` | Destrucció, per a objectes que han estat actius |
| `OnValidate()` | Validació de dades a l’Editor; no és un bucle de joc |

No pressuposis l’ordre d’`Awake` entre objectes diferents. Desactivar i reactivar no torna a executar `Start`.

## Crear i destruir

Dins d’un mètode; `prefab` és un GameObject assignat prèviament:

```csharp
GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
Destroy(instance); // Eliminació diferida, no immediata dins d’aquesta instrucció
```

**Errors habituals:** confondre desactivació amb invisibilitat; considerar que tots els components són MonoBehaviour; creure que Static bloqueja els canvis; esperar conservar canvis fets durant Play.

**Demos:** [Objectes](<Demo-0-Objectes.md>) · [Prefabs](<Demo-1-Prefabs.md>).
