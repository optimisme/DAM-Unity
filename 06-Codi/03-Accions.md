# Accions

Mentre es juga, hi ha diverses accions que poden requerir cridar funcions del codi.

## Alguns exemples:

| Funció                                         | Quan es crida                                                      | Exemple                     |
| :--------------------------------------------- | :----------------------------------------------------------------- | :-------------------------- |
| `OnCollisionEnter` / `OnCollisionExit`         | Quan dos objectes amb **Rigidbody + Collider** xoquen o es separen | Rebotar una pilota   
| `OnTriggerEnter` / `OnTriggerExit`             | Quan un objecte entra o surt d’un volum *Trigger*                  | Obrir una porta automàtica  |       |
| `OnMouseEnter` / `OnMouseExit` / `OnMouseDown` | Quan el ratolí passa o clica un objecte                            | Ressaltar o seleccionar     |
| `OnBecameVisible` / `OnBecameInvisible`        | Quan la càmera veu o deixa de veure un objecte                     | Pausar animacions llunyanes |
| `OnEnable` / `OnDisable` / `OnDestroy`         | Canvis d’estat de l’objecte                                        | Crear/netejar partícules    |

Cada joc tindrà la seva pròpia lògica. Aquí van alguns exemples:

### OnCollisionEnter / OnCollisionExit

Quan un objecte xoca o toca el propi objecte on hi ha l'script s'executen les funcions *"OnCollisionEnter/OnCollisionExit*".

Per tal que funcionin la propietat **"Is Trigger"** del *collider* ha d'estar **desactivada**.

Els objectes amb **"Is Trigger"** desactivat són sòlids i no es poden *"atravessar"*

```csharp
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor" 
           || collision.gameObject.name == "Player"
           || collision.gameObject.transform.parent.name == "Player") return;
        UpdateColor(true);
    }
```

### OnTriggerEnter / OnTriggerExit

Quan un objecte entra dins del propi objecte on hi ha l'script s'executen les funcions *"OnTriggerEnter/OnTriggerExit*".

Per tal que funcionin la propietat **"Is Trigger"** del *collider* ha d'estar **activada**.

Els objectes amb **"Is Trigger"** activat **NO són sòlids** i es poden *"atravessar"*

```csharp
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
```

## Sobrecarregar accions

A vegades volem que un objecte extern tingui el codi que s'ha d'executar, quan hi ha un esdeveniment al propi objecte.

En aquest cas, es defineixen variables tipus **"Action"**, l'objecte extern s'encarrega de decidir quin codi s'executa.

### Exemple

Codi objecte:
```csharp
public class ActivableExample : MonoBehaviour
{
    public Action OnEnter;

    void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(other); // Executar el codi que han definit a "OnEnter"
    }
}
```

Codi de l'objecte extern que vol executar una funció quan l'objecte 'ActivableExample' té un event *"OnTriggerEnter"*:
```csharp
public class OtherObject : MonoBehaviour
{
    public ActivableExample trigger; // Referència al objecte que observerà *OnTriggerEnter*

    void Awake()
    {
        if (trigger == null) trigger = GetComponentInChildren<ActivableExample>(true);

        trigger.OnEnter += HandleEnter;     // Definir el codi que executa el trigger quan fa *OnEnter?.Invoke()*
    }

    void OnDestroy()
    {
        if (trigger != null) trigger.OnEnter -= HandleEnter; // Esborrar la definició, per netejar
    }

    void HandleEnter(Collider other)
    {
        Debug.Log("Codi que s'executa quan 'ActibableExample' té un event 'OnTriggerEnter'");
        Debug.Log($"El trigger ha detectat: {other.gameObject.name}");
    }
}
```

## Corutines

Una corutina *(coroutine)* és una funció especial de Unity que permet per fer processos “en paral·lel” (com temporitzadors, animacions, ...)

És una funció que:

- Retorna IEnumerator
- Conté una o més instruccions yield return
- Es crida amb **"StartCoroutine()"**

**Exemple:**

Definir la corutina:
```csharp
IEnumerator ExempleCoroutine()
{
    Debug.Log("Inici");
    yield return new WaitForSeconds(2f); // pausa 2 segons
    Debug.Log("Han passat 2 segons!");
}
```

Per posar-la en funcionament:
```csharp
void Start()
{
    StartCoroutine(ExempleCoroutine());
}
```

Això serveix per fer comptes enrrera i cridar acció externa:
```csharp
using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Temps total del compte enrere (en segons)")]
    public float duration = 5f;

    [Tooltip("Cridat cada segon mentre dura el compte enrere")]
    public Action<int> OnTimer; // passa els segons restants

    [Tooltip("Cridat quan el compte arriba a zero")]
    public Action OnFinished;

    float remaining;
    Coroutine timerCo;

    void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        // Si ja hi ha un temporitzador actiu, el parem
        if (timerCo != null) StopCoroutine(timerCo);

        remaining = duration;
        timerCo = StartCoroutine(CountdownCoroutine());
        OnTimer?.Invoke();
    }

    IEnumerator CountdownCoroutine()
    {
        while (remaining > 0f)
        {
            // Cridem l'acció amb el temps restant (enters)
            OnTimer?.Invoke(Mathf.CeilToInt(remaining));

            // Esperem 1 segon real
            yield return new WaitForSeconds(1f);

            remaining -= 1f;
        }

        // Quan arriba a zero
        OnFinished?.Invoke();
        timerCo = null;
    }
}
```

