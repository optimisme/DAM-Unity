# Accions

Mentre es juga, hi ha diverses accions que poden requerir cridar funcions del codi.

Alguns exemples:

| Funció                                         | Quan es crida                                                      | Exemple                     |
| :--------------------------------------------- | :----------------------------------------------------------------- | :-------------------------- |
| `OnCollisionEnter` / `OnCollisionExit`         | Quan dos objectes amb **Rigidbody + Collider** xoquen o es separen | Rebotar una pilota          |
| `OnTriggerEnter` / `OnTriggerExit`             | Quan un objecte entra o surt d’un volum *Trigger*                  | Obrir una porta automàtica  |
| `OnMouseEnter` / `OnMouseExit` / `OnMouseDown` | Quan el ratolí passa o clica un objecte                            | Ressaltar o seleccionar     |
| `OnBecameVisible` / `OnBecameInvisible`        | Quan la càmera veu o deixa de veure un objecte                     | Pausar animacions llunyanes |
| `OnEnable` / `OnDisable` / `OnDestroy`         | Canvis d’estat de l’objecte                                        | Crear/netejar partícules    |

## Exemples

Cada joc tindrà la seva pròpia lògica. Aquí van alguns exemples:

### Exemple 0: Detectar proximitat amb OnTriggerEnter

### Exemple 1: Activar accions segons distància a un objecte (Vector3.Distance())

### Exemple 2: Activar accions segons col·lisió (OnCollisionEnter)

### Exemple 3: Activar accions segons col·lisió (OnCollisionEnter)

