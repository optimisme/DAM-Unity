# Esdeveniments

Un emissor comunica que ha passat alguna cosa; els receptors decideixen com reaccionar. No és sobrecàrrega de mètodes.

## Escollir mecanisme

| Mecanisme | Ús |
|---|---|
| Crida directa | L’emissor coneix el receptor i vol executar-ne una funció |
| `Action` / `Action<T>` | Delegat sense retorn, amb zero o un paràmetre |
| `event Action<T>` | Subscripció des d’altres classes; només l’emissor pot invocar-lo |
| `UnityEvent` / `UnityEvent<T>` | Respostes configurables a l’Inspector o des del codi |

## Esdeveniment C#

Dins del component emissor, amb `using System;` i `using UnityEngine;`:

```csharp
public event Action<Collider> Entered;
void OnTriggerEnter(Collider other)
{
    Entered?.Invoke(other);
}
```

Dins del receptor; `source` és una referència assignada a l’emissor anterior:

```csharp
void OnEnable() => source.Entered += HandleEnter;
void OnDisable()
{
    if (source != null) source.Entered -= HandleEnter;
}
void HandleEnter(Collider other) => Debug.Log(other.name);
```

| Operació | Efecte |
|---|---|
| `+= Handler` | Afegir una subscripció |
| `-= Handler` | Retirar-la; cal el mateix receptor i mètode |
| `?.Invoke(value)` | Invocar si hi ha subscriptors |

La signatura ha de coincidir: `Action` → `Invoke()`; `Action<int>` → `Invoke(3)`; `Action<Collider>` → `Invoke(other)`.

## UnityEvent

Camp dins del component emissor, amb `using UnityEngine.Events;`:

```csharp
public UnityEvent<bool> stateChanged = new UnityEvent<bool>();
```

Quan l’estat canvia, dins d’un mètode de l’emissor:

```csharp
stateChanged.Invoke(true);
```

| Configuració a l’Inspector | Significat |
|---|---|
| Objecte receptor | Instància que conté el component amb el mètode |
| Dynamic bool | Rep el valor true/false emès pel codi |
| Paràmetre estàtic | Utilitza sempre el valor fixat a l’Inspector |
| Runtime Only | Executa la resposta durant el joc |

Un receptor compatible exposa un mètode públic `void SetOpen(bool value)`. Exemple de connexió: placa → UnityEvent<bool> → reixa.SetOpen.

## Subscripcions

- `OnEnable` / `OnDisable`: el receptor escolta mentre està habilitat.
- Evita subscripcions repetides: cada `+=` pot afegir una altra invocació.
- Guarda el delegat si utilitzes una lambda que hauràs de desubscriure.
- Invoca quan canvia l’estat, si només interessa detectar el canvi.

**Errors habituals:** nombre o tipus d’arguments incorrecte; oblidar retirar una subscripció; assignar un valor estàtic quan es necessita el valor dinàmic; confondre un camp Action públic amb un event encapsulat.

**Demo:** [Mecanismes](<Demo-5-Mecanismes.md>).
