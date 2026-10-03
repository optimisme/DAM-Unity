# Càmera

La càmera determina la projecció i la part del món que es dibuixa. La seva posició i orientació es controlen amb Transform.

## Projecció i visibilitat

| Propietat | Ús |
|---|---|
| `orthographic = false` | Perspectiva: els objectes llunyans es veuen més petits |
| `fieldOfView` | Angle vertical de visió en graus, en perspectiva |
| `orthographic = true` | Projecció ortogràfica: mida independent de la profunditat |
| `orthographicSize` | Meitat de l’alçada visible en unitats del món |
| `nearClipPlane` / `farClipPlane` | Distàncies propera i llunyana del volum visible |
| `cullingMask` | Layers que dibuixa aquesta càmera |
| `Camera.main` | Càmera habilitada amb tag MainCamera; pot ser null |

**Zoom:** variar FOV en perspectiva o `orthographicSize` en ortogràfica. Apropar físicament una càmera ortogràfica no augmenta la mida aparent dels objectes.

## Orientació i seguiment

| Operació | Expressió |
|---|---|
| Mirar un punt | `transform.LookAt(target.position)` |
| Fixar angles | `transform.rotation = Quaternion.Euler(pitch, yaw, 0f)` |
| Posició amb offset global | `target.position + offset` |
| Offset relatiu a una orientació | `target.position + rotation * offset` |
| Suavitzar posició | `Vector3.SmoothDamp(...)` |
| Limitar inclinació | `Mathf.Clamp(pitch, minPitch, maxPitch)` |

Seguiment dins de **LateUpdate**; `target` és un Transform assignat i `offset` un Vector3:

```csharp
if (target == null) return;
transform.position = target.position + offset;
transform.LookAt(target.position);
```

Posició d’una òrbita, dins de LateUpdate; `pivot`, `pitch`, `yaw` i `distance` estan definits:

```csharp
Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
transform.rotation = rotation;
transform.position = pivot.position - transform.forward * distance;
```

## Pantalla i món

| Mètode | Conversió |
|---|---|
| `cam.ScreenPointToRay(screenPosition)` | Píxel de pantalla → raig al món |
| `cam.WorldToScreenPoint(worldPosition)` | Punt del món → coordenades de pantalla |
| `cam.ScreenToWorldPoint(screenPosition)` | Pantalla → món; Z indica profunditat des de la càmera |

Dins d’un mètode; `cam` és una Camera assignada i `screenPosition` la posició del cursor:

```csharp
Ray ray = cam.ScreenPointToRay(screenPosition);
if (Physics.Raycast(ray, out RaycastHit hit, 100f))
    Debug.Log(hit.collider.name);
```

## Criteris de càmera

| Necessitat | Criteri |
|---|---|
| Seguiment després del jugador | LateUpdate |
| Gir per teclat o estic a una velocitat angular | Graus per segon × deltaTime |
| Gir per desplaçament del ratolí | Píxels de delta × sensibilitat; no tornar a multiplicar per deltaTime |
| Suavitzat estable | Evitar un factor Lerp constant per fotograma |
| Detectar obstacles en ortogràfica | Raigs paral·lels a forward; ScreenPointToRay respecta la projecció |

**Errors habituals:** utilitzar FOV com a zoom ortogràfic; donar un Z incorrecte a ScreenToWorldPoint; confondre offset global amb «darrere del jugador»; utilitzar Camera.main sense comprovar-la.

**Demos:** [Objectes](<Demo-0-Objectes.md>) · [Nivells](<Demo-3-Nivells.md>).
