# Parallax

Parallax permet moure les capes de decoració, a diferent velocitat, per donar sensació de profunditat al joc.

[Explicació Parallax](https://www.youtube.com/shorts/I5lPwTqGDcU)

Les capes més llunyanes es mouen més lentament que les més properes.

- Les capes per darrera del jugador es mouen més lentament que el jugador
- Les capes més pròximes al jugador es mouen més ràpid que al jugador

## Estructura

Crea un nou objecte buit amb *"Create Empty"* anomenat **"Parallax Root"** a les posicions:

- Pos X: 0
- Pos Y: 0

Fes servir els *Assets* importats de la carpeta **BackgroundImages**

Cal arreglar cada una d'aquestes imatges, sel·lecciona-les i amb l'inspetor configura:

- **Sprite Mode**: Single
(Per totes les imatges *.png*, ignora l'arxiu *"Backgrounds"*)

**Important:** Guarda els canvis per cada imatge.

- Mou la imatge **Sky** dins de l'objecte **ParallaxRoot** i configura:

    - Pos X: 0
    - Pos Y: (Que toqui a baix)
    - Scale X: 3
    - Scale Y: 2.5
    - Order in Layer: -100

- Mou les imatges **Back_X** des de la 0 fins a la 4 i configura:

    - Pos X: 0
    - Pos Y: (Segons la capa, que toquin a baix excepte els núvols)
    - Scale X: 2
    - Scale Y: Segons capa
    - Order in Layer: (la 0 a -99 i anar descomptant 1 per cada capa)

- Mou les imatges **Fore_0** i **Fore_1** i configura:

    - Pos X: 0
    - Pos Y: -4
    - Scale X: 2
    - Scale Y: 1.5
    - Order in Layer: (la 0 a 100 i anar sumant 1 per cada capa)

Ha de quedar així:

<center>
<img src="./assets/parallax-final.png" style="width: 90%; max-width: 600px">
</center>
<br/>

## Script

Crea l'arxiu **"ParallaxRoot"** a la carpeta **"Scripts"** i assigna'l a l'objecte **"ParallaxRoot"**

```csharp
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParallaxRootParallaxManager : MonoBehaviour
{
    [Header("References")]
    public Camera cam;

    [Header("Global")]
    [Tooltip("Multiplicador global per a totes les capes (afinament ràpid).")]
    public float globalSpeedMultiplier = 1f;

    [Tooltip("Si s'estableix, forçarà aquesta Sorting Layer a totes les capes.")]
    public string overrideSortingLayerName = ""; // opcional

    [Header("Vertical Parallax")]
    [Tooltip("Activar efecte parallax vertical.")]
    public bool enableVerticalParallax = true;

    [Tooltip("Multiplicador global per parallax vertical (normalment més subtil que horitzontal).")]
    public float globalVerticalMultiplier = 0.5f;

    [Tooltip("Parallax vertical BACK (0..1). Min a capes més llunyanes, Max a les més properes al midground.")]
    public float backVerticalParallaxMin = 0.05f;
    public float backVerticalParallaxMax = 0.8f;

    [Tooltip("Parallax vertical per fallback (altres capes no etiquetades).")]
    public float minVerticalParallax = 0.1f;
    public float maxVerticalParallax = 1.2f;

    [Header("Auto 'Rule' Generation")]
    [Tooltip("Prefix dels noms de capa de fons (negatius). Exemple: Back_0, Back_1, ...")]
    public string backPrefix = "Back_";

    [Tooltip("Prefix dels noms de capa davanteres (positius). Exemple: Fore_0, Fore_1, ...")]
    public string forePrefix = "Fore_";

    [Tooltip("Ordre inicial per BACK (negatiu). Ex: -100 → -100, -99, -98...")]
    public int backOrderStart = -100;

    [Tooltip("Pas entre ordres BACK (normalment 1).")]
    public int backOrderStep = 1;

    [Tooltip("Parallax BACK (0..1). Min a capes més llunyanes, Max a les més properes al midground.")]
    public float backParallaxMin = 0.10f;
    public float backParallaxMax = 0.95f;

    [Space(6)]
    [Tooltip("Ordre inicial per FORE (positiu). Ex: +100 → 100, 101, 102...")]
    public int foreOrderStart = +100;

    [Tooltip("Pas entre ordres FORE (normalment 1).")]
    public int foreOrderStep = 1;

    [Tooltip("PARALLAX HORITZONTAL FORE (magnitud absoluta; s'aplica signe NEGATIU).")]
    public float foreParallaxAbsMin = 0.10f;   // recomanat 0.05..0.50
    public float foreParallaxAbsMax = 0.40f;

    [Tooltip("PARALLAX VERTICAL FORE (magnitud absoluta; s'aplica signe NEGATIU).")]
    public float foreVerticalAbsMin = 0.00f;   // recomanat 0.00..0.30
    public float foreVerticalAbsMax = 0.20f;

    [Space(6)]
    [Tooltip("Nom exacte del fill que actuarà com a 'Sky' (segueix la càmera; sense loop per defecte).")]
    public string skyName = "Sky";

    [Tooltip("Ordre per al 'Sky' (pot ser positiu).")]
    public int skyOrder = -100;

    [Tooltip("El 'Sky' no crea tiles L/R (loop), queda desactivat per defecte.")]
    public bool skyEnableLoop = false;

    [Header("Fallback (altres capes no etiquetades)")]
    public bool autoOrderFromSiblingIndex = true;
    public float minParallax = 0.10f;
    public float maxParallax = 1.20f;

    [Header("Debug")]
    public bool verboseLogs = true;

    // --- Interns ---
    private readonly List<LayerState> layers = new();

    private class LayerState
    {
        public Transform root;                 // Capa (fill directe del Root)
        public Transform visual;               // Transform del SpriteRenderer principal dins la capa
        public SpriteRenderer visualSR;
        public SpriteRenderer[] allSRsInLayer; // Tots els SRs de la capa (per aplicar-hi order)
        public Transform[] tiles;              // [0]=L, [1]=C(visual), [2]=R
        public float tileWorldWidth;
        public float parallax;                 // horizontal
        public float verticalParallax;         // vertical
        public float anchorX;
        public float anchorY;
        public float startCamX;
        public float startCamY;
        public bool enableLoop = true;         // per desactivar loop (Sky)
    }

    // --- Utilitat: extreu índex numèric d'un nom amb prefix ---
    private static bool TryParseIndexed(string name, string prefix, out int index)
    {
        index = -1;
        if (!name.StartsWith(prefix, StringComparison.Ordinal)) return false;

        string tail = name.Substring(prefix.Length);
        // accepta "Fore_0", "Fore_01", "Fore_12_extra" → agafa la primera seq numèrica
        var m = Regex.Match(tail, @"(\d+)");
        if (!m.Success) return false;

        return int.TryParse(m.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out index);
    }

    void Start()
    {
        if (!cam) cam = Camera.main;

        // --- 1) Escaneja fills i classifica per grups ---
        var backChildren = new List<(Transform child, int idx)>();
        var foreChildren = new List<(Transform child, int idx)>();
        Transform skyChild = null;

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);

            if (string.Equals(child.name, skyName, StringComparison.Ordinal))
            {
                skyChild = child;
                continue;
            }

            if (TryParseIndexed(child.name, backPrefix, out int backIdx))
            {
                backChildren.Add((child, backIdx));
                continue;
            }

            if (TryParseIndexed(child.name, forePrefix, out int foreIdx))
            {
                foreChildren.Add((child, foreIdx));
                continue;
            }
        }

        // Ordena per índex numèric ascendent: Back_0, Back_1, ... / Fore_0, Fore_1, ...
        backChildren.Sort((a, b) => a.idx.CompareTo(b.idx));
        foreChildren.Sort((a, b) => a.idx.CompareTo(b.idx));

        // --- 2) Calcula 'rules' al vol per BACK/FORE/SKY ---
        var rulesByName = new Dictionary<string, (int order, float parallax, float verticalParallax, bool enableLoop)>(StringComparer.Ordinal);

        if (backChildren.Count > 0)
        {
            for (int k = 0; k < backChildren.Count; k++)
            {
                string name = backChildren[k].child.name;
                int order = backOrderStart + k * backOrderStep;
                float t = (backChildren.Count > 1) ? (float)k / (backChildren.Count - 1) : 0f;

                float parallax = Mathf.Lerp(backParallaxMax, backParallaxMin, t);                                // < 1
                float verticalParallax = Mathf.Lerp(backVerticalParallaxMax, backVerticalParallaxMin, t);        // < 1

                rulesByName[name] = (order, parallax, verticalParallax, true);
            }
        }

        if (foreChildren.Count > 0)
        {
            for (int k = 0; k < foreChildren.Count; k++)
            {
                string name = foreChildren[k].child.name;
                int order = foreOrderStart + k * foreOrderStep;
                float t = (foreChildren.Count > 1) ? (float)k / (foreChildren.Count - 1) : 0f;

                // Magnitud petita i signe NEGATIU (mou en oposat a la càmera, però suau)
                float magH = Mathf.Lerp(foreParallaxAbsMin, foreParallaxAbsMax, t);    // 0.10..0.40
                float parallax = -magH;

                float magV = Mathf.Lerp(foreVerticalAbsMin, foreVerticalAbsMax, t);   // 0.00..0.20
                float verticalParallax = -magV;

                // Evita valors exagerats
                parallax = Mathf.Clamp(parallax, -1f, 1f);
                verticalParallax = Mathf.Clamp(verticalParallax, -1f, 1f);

                rulesByName[name] = (order, parallax, verticalParallax, true);
            }
        }

        if (skyChild != null)
        {
            // Sky → segueix la càmera: parallax = 1f, sense loop per defecte
            rulesByName[skyChild.name] = (skyOrder, 1f, 1f, skyEnableLoop);
        }

        // --- 3) Crea 'layers' + aplica settings ---
        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);

            // Troba SpriteRenderers dins de la capa
            var srs = child.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
            if (srs == null || srs.Length == 0) continue; // capa no visual

            var visualSR = srs[0];
            var visualTf = visualSR.transform;

            var state = new LayerState
            {
                root = child,
                visual = visualTf,
                visualSR = visualSR,
                allSRsInLayer = srs,
                tiles = new Transform[3],
                enableLoop = true
            };

            // SortingLayer opcional
            if (!string.IsNullOrEmpty(overrideSortingLayerName))
            {
                int layerId = SortingLayer.NameToID(overrideSortingLayerName);
                if (layerId != 0 || overrideSortingLayerName == "Default")
                {
                    foreach (var sr in srs) sr.sortingLayerID = layerId;
                }
                else
                {
                    Debug.LogWarning($"[ParallaxRoot] Sorting Layer \"{overrideSortingLayerName}\" no existeix.");
                }
            }

            // Assigna ordre + parallax segons regla auto o fallback
            if (rulesByName.TryGetValue(child.name, out var rule))
            {
                ApplySortingOrderToAll(state, rule.order);
                state.parallax = rule.parallax;
                state.verticalParallax = rule.verticalParallax;
                state.enableLoop = rule.enableLoop;

                if (verboseLogs)
                    Debug.Log($"[ParallaxRoot][RULE] {child.name}: order={rule.order}, parallax={rule.parallax:0.00}, vParallax={rule.verticalParallax:0.00}, loop={rule.enableLoop}");
            }
            else
            {
                // Fallback per capes no etiquetades com Back_/Fore_/Sky
                if (autoOrderFromSiblingIndex)
                {
                    int idx = child.GetSiblingIndex();
                    int total = transform.childCount;
                    int order = idx;
                    ApplySortingOrderToAll(state, order);

                    float t = (total > 1) ? (float)idx / (total - 1) : 0f;
                    state.parallax = Mathf.Lerp(minParallax, maxParallax, t);
                    state.verticalParallax = Mathf.Lerp(minVerticalParallax, maxVerticalParallax, t);
                    state.enableLoop = true;

                    if (verboseLogs)
                        Debug.Log($"[ParallaxRoot][AUTO] {child.name}: idx={idx}/{total} → order={order}, parallax={state.parallax:0.00}, vParallax={state.verticalParallax:0.00}, loop={state.enableLoop}");
                }
                else
                {
                    ApplySortingOrderToAll(state, 0);
                    state.parallax = (minParallax + maxParallax) * 0.5f;
                    state.verticalParallax = (minVerticalParallax + maxVerticalParallax) * 0.5f;
                    state.enableLoop = true;

                    if (verboseLogs)
                        Debug.Log($"[ParallaxRoot][AUTO-Fallback] {child.name}: order=0, parallax={state.parallax:0.00}, vParallax={state.verticalParallax:0.00}, loop={state.enableLoop}");
                }
            }

            // Amplada del tile segons el visual
            state.tileWorldWidth = state.visualSR.bounds.size.x;
            if (state.tileWorldWidth <= 0f)
            {
                var sp = state.visualSR.sprite;
                if (sp != null)
                {
                    float ppu = sp.pixelsPerUnit > 0 ? sp.pixelsPerUnit : 100f;
                    state.tileWorldWidth = (sp.rect.width / ppu) * state.visual.lossyScale.x;
                }
            }

            // Crea L/C/R (excepte si loop està desactivat, p.ex. Sky)
            state.tiles[1] = state.visual;
            if (state.enableLoop && state.tileWorldWidth > 0f)
            {
                state.tiles[0] = CreateTileClone(state, -1);
                state.tiles[2] = CreateTileClone(state, +1);
            }
            else
            {
                state.tiles[0] = null;
                state.tiles[2] = null;
            }

            // Ancoratge i càmera (ara amb Y també)
            state.anchorX = state.visual.position.x;
            state.anchorY = state.visual.position.y;
            state.startCamX = cam ? cam.transform.position.x : 0f;
            state.startCamY = cam ? cam.transform.position.y : 0f;

            layers.Add(state);
        }
    }

    void LateUpdate()
    {
        if (!cam) return;

        foreach (var L in layers)
        {
            // Horizontal parallax (existent)
            float camDeltaX = cam.transform.position.x - L.startCamX;
            float targetX = L.anchorX + camDeltaX * L.parallax * globalSpeedMultiplier;

            var pos = L.root.position;
            float dx = targetX - L.visual.position.x;
            pos.x += dx;

            // Vertical parallax (nou)
            if (enableVerticalParallax)
            {
                float camDeltaY = cam.transform.position.y - L.startCamY;
                float targetY = L.anchorY + camDeltaY * L.verticalParallax * globalVerticalMultiplier;
                float dy = targetY - L.visual.position.y;
                pos.y += dy;
            }

            L.root.position = pos;

            if (L.enableLoop) RecenterTilesIfNeeded(L);
        }
    }

    // --- Helpers ---
    private void ApplySortingOrderToAll(LayerState state, int order)
    {
        foreach (var sr in state.allSRsInLayer)
            sr.sortingOrder = order;
    }

    private Transform CreateTileClone(LayerState L, int offsetIndex)
    {
        var go = new GameObject(L.root.name + (offsetIndex < 0 ? "_L" : "_R"));
        go.transform.SetParent(L.root, worldPositionStays: true);

        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = L.visualSR.sprite;
        sr.flipX = L.visualSR.flipX;
        sr.flipY = L.visualSR.flipY;
        sr.drawMode = L.visualSR.drawMode;
        sr.sharedMaterial = L.visualSR.sharedMaterial;
        sr.color = L.visualSR.color;
        sr.sortingLayerID = L.visualSR.sortingLayerID;
        sr.sortingOrder = L.visualSR.sortingOrder;

        go.transform.position = L.visual.position + new Vector3(L.tileWorldWidth * offsetIndex, 0f, 0f);
        go.transform.rotation = L.visual.rotation;
        go.transform.localScale = L.visual.lossyScale;

        if (verboseLogs)
            Debug.Log($"[ParallaxRoot] + Tile {(offsetIndex < 0 ? "L" : "R")} per {L.root.name}");

        return go.transform;
    }

    private void RecenterTilesIfNeeded(LayerState L)
    {
        if (L.tiles[1] == null) return; // loop desactivat

        float camX = cam.transform.position.x;

        // Dreta
        while (camX - L.tiles[1].position.x > L.tileWorldWidth * 0.5f)
        {
            if (L.tiles[0] == null || L.tiles[2] == null) break;
            L.tiles[0].position = L.tiles[2].position + new Vector3(L.tileWorldWidth, 0f, 0f);
            var tmp = L.tiles[0]; L.tiles[0] = L.tiles[1]; L.tiles[1] = L.tiles[2]; L.tiles[2] = tmp;
        }

        // Esquerra
        while (L.tiles[1].position.x - camX > L.tileWorldWidth * 0.5f)
        {
            if (L.tiles[0] == null || L.tiles[2] == null) break;
            L.tiles[2].position = L.tiles[0].position - new Vector3(L.tileWorldWidth, 0f, 0f);
            var tmp = L.tiles[2]; L.tiles[2] = L.tiles[1]; L.tiles[1] = L.tiles[0]; L.tiles[0] = tmp;
        }
    }
}
```

Aquest objecte, automàticament configura el *Parallax*

- La capa **Sky** es queda fixe de fons
- Les capes **Back_X** s'ordenen de 0 al fons, fins a X la més pròxima al jugador
- Les capes **Fore_X** s'ordenen de 0 la més propera al jugador, fins a X la més pròxima a l'espectador

Automàticament s'assigna la propietat **"Order"** perquè es sobrepòssin adequadament.

L'script crea els objectes "Left" i "Right" per tal de mostrar-los automàticament amb el moviment de la càmera.