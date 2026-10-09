# Temps i corutines

Una corutina reparteix una operació entre diferents actualitzacions. No crea un fil d’execució paral·lel: el seu codi habitual s’executa al fil principal.

## Temps

| Propietat | Significat |
|---|---|
| `Time.deltaTime` | Temps de joc entre fotogrames |
| `Time.fixedDeltaTime` | Interval dels passos de física |
| `Time.time` | Temps de joc acumulat |
| `Time.unscaledDeltaTime` | Interval independent de timeScale |
| `Time.timeScale` | Escala del temps de joc; 0 pausa el temps escalat |

| Càlcul | Expressió |
|---|---|
| Distància a velocitat constant | `speed * Time.deltaTime` |
| Compte enrere en Update | `remaining = Mathf.Max(0f, remaining - Time.deltaTime)` |
| Animació de UI durant pausa | Utilitzar unscaledDeltaTime |

Update continua executant-se amb timeScale a zero; els passos de física habituals i les esperes escalades s’aturen.

## Esperes

| Instrucció | Quan continua |
|---|---|
| `yield return null` | Fotograma següent |
| `yield return new WaitForSeconds(2f)` | Després de dos segons de temps de joc |
| `yield return new WaitForSecondsRealtime(2f)` | Després de dos segons de temps no escalat |
| `yield return new WaitUntil(() => ready)` | Quan la condició és certa |

Les esperes reprenen en una actualització posterior al termini; no són temporitzadors d’exactitud absoluta. [WaitForSeconds](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/WaitForSeconds.html).

## Declarar, iniciar i aturar

Dins d’un MonoBehaviour, amb `using System.Collections;` i `using UnityEngine;`:

```csharp
IEnumerator WaitAndNotify()
{
    yield return new WaitForSeconds(2f);
    Debug.Log("Espera acabada");
}
```

Camp del component:

```csharp
private Coroutine pending;
```

Dins del mètode que inicia o reinicia l’espera:

```csharp
if (pending != null) StopCoroutine(pending);
pending = StartCoroutine(WaitAndNotify());
```

| Acció | Efecte |
|---|---|
| `StopCoroutine(pending)` | Aturar aquella execució |
| `StopAllCoroutines()` | Aturar les corutines d’aquest MonoBehaviour |
| Desactivar el GameObject | Atura les seves corutines; reactivar-lo no les reprèn |
| `enabled = false` al MonoBehaviour | No atura automàticament les corutines |
| Destruir el component | Atura les seves corutines |

Si la referència `pending` s’utilitza per indicar «en execució», posa-la a null en acabar o cancel·lar. Una referència no nul·la no demostra que la corutina segueixi activa.

**Errors habituals:** iniciar una corutina a cada Update; considerar WaitForSeconds temps real; utilitzar un bucle llarg sense yield i bloquejar el joc; esperar que una corutina aturada es reprengui sola.

**Demos:** [Prefabs](<Demo-1-Prefabs.md>) · [Nivells](<Demo-4-Nivells.md>).
