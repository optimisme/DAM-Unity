# Referències i jerarquia

Una referència permet accedir a un objecte o component que ja existeix. Declarar un camp no crea l’objecte ni n’assigna la referència.

## Escollir com obtenir una referència

| Necessitat | Expressió | Observació |
|---|---|---|
| Objecte conegut | `[SerializeField] private Transform target;` | Assignació des de l’Inspector |
| Component propi | `GetComponent<Rigidbody>()` | Mateix GameObject; pot tornar null |
| Component opcional | `TryGetComponent<Light>(out var light)` | Retorna true si existeix |
| Component en descendents | `GetComponentInChildren<Light>(true)` | Inclou el mateix objecte; true inclou descendents inactius |
| Component en ascendents | `GetComponentInParent<Rigidbody>()` | Inclou el mateix objecte i puja per la jerarquia |
| Qualsevol instància activa d’un tipus | `FindAnyObjectByType<Light>()` | Cerca global; no escull una llum concreta |
| Totes les instàncies actives | `FindObjectsByType<Light>()` | Sintaxi de Unity 6.6; ordre no garantit |
| Objecte actiu amb un tag | `GameObject.FindWithTag("Player")` | El tag ha d’existir; si n’hi ha diversos, no n’escull un de previsible |
| Objecte actiu amb un nom | `GameObject.Find("Player")` | Fràgil davant canvis de nom o duplicats |

Prioritza Inspector per a dependències conegudes i `GetComponent` per a components propis. Desa les cerques reutilitzades; evita repetir cerques globals a cada Update.

## Inicialització

Camps i mètode dins del component; requereix Rigidbody al mateix objecte:

```csharp
private Rigidbody body;
void Awake()
{
    body = GetComponent<Rigidbody>();
}
```

| Moment | Criteri |
|---|---|
| Inspector | La referència serialitzada ja està assignada quan s’executa Awake |
| Awake | Obtenir referències que ja existeixen |
| Start | Utilitzar inicialització feta a Awake d’altres objectes actius de l’escena |
| Instanciació posterior | Passar explícitament les dependències quan es crea l’objecte |

No hi ha una regla «objecte propi = Awake, objecte extern = Start». Importa quan està disponible allò que necessites.

## Navegar per la jerarquia

| Expressió | Resultat |
|---|---|
| `transform.parent` | Pare immediat; null si no n’hi ha |
| `transform.root` | Transform superior de la jerarquia; no necessàriament el jugador |
| `transform.childCount` | Nombre de fills directes |
| `transform.GetChild(0)` | Primer fill directe; requereix almenys un fill |
| `transform.Find("Visual")` | Fill directe amb aquest nom |
| `transform.Find("Visual/Light")` | Descendent mitjançant un camí explícit |
| `transform.SetParent(parent, true)` | Canviar de pare conservant la transformació global |

Dins d’un mètode:

```csharp
foreach (Transform child in transform)
    Debug.Log(child.name); // Només fills directes
```

## Comprovar una referència

Dins d’un mètode; `target` és una referència de Unity:

```csharp
if (target == null) return;
Debug.Log(target.name);
```

Per als objectes de Unity, prefereix `== null` o `if (target)` a `?.` quan l’objecte pot haver estat destruït: Unity gestiona una nul·litat pròpia.

## Comparar o detectar components

| Pregunta | Forma habitual |
|---|---|
| És el mateix objecte? | `other.gameObject == target.gameObject` |
| Té el tag esperat? | `other.CompareTag("Player")` |
| Té un component concret? | `other.TryGetComponent<Rigidbody>(out var body)` |
| Quin cos físic té aquest collider? | `other.attachedRigidbody` |

`GetComponent<MonoBehaviour>()` no comprova tots els scripts de l’objecte. Demana directament el tipus que necessites.

**Errors habituals:** camp sense assignar; accedir a `parent.name` sense comprovar el pare; confondre un GameObject amb un component; suposar que `transform.Find` cerca recursivament per nom.

**Demos:** [Plataformes](<Demo-2-Plataformes.md>) · [Mecanismes](<Demo-4-Mecanismes.md>).
