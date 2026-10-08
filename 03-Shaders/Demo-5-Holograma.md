# Demo 5: Holograma: franges i vores lluminoses

Construirem tres figures semitransparents amb franges animades i vores que s’il·luminen segons l’angle de vista. Separarem el patró de franges, Fresnel, emissió i alpha.

**Projecte acabat:** [Demo-5-Holograma.zip](demos/Demo-5-Holograma.zip). Descomprimeix-lo i obre la carpeta amb Assets, Packages i ProjectSettings des de Unity Hub. Obre `Assets/Demos/Holograma/Scenes/DemoHolograma.unity` i prem Play.

<img src="assets/demoholograma-escena.png" alt="Escena de la demo Holograma renderitzada a Unity" width="600" style="width: 90%; max-width: 600px; height: auto;">

## 1. Preparar un projecte buit

1. Utilitza **Unity 6000.6.3f1**, la versió de les demos de Codi. Crea un projecte **Universal 3D (URP)**.
2. A Package Manager, comprova **Universal RP 17.6.0**, **Shader Graph 17.6.0** i **Input System 1.20.0**. Shader Graph arriba com a dependència d’URP.
3. A **Project Settings > Player > Active Input Handling**, selecciona **Input System Package (New)**. Accepta el reinici si Unity el demana.
4. Crea `Assets/Demos/Holograma` amb les carpetes **Shaders**, **Materials**, **Scenes** i **Meshes**. Crea també **Assets/Common** per als scripts.
5. Crea una escena buida, afegeix una Camera amb tag **MainCamera** i desa l’escena com a `DemoHolograma` dins de Scenes.

## 2. Crear el Shader Graph

A Shaders, crea **Shader Graph > URP > Lit Shader Graph**, amb nom **SHolograma**. A Graph Inspector > Graph Settings, fixa:

| Opció | Valor |
|---|---|
| Target | Universal |
| Material | Lit |
| Surface Type | Transparent |
| Alpha Clipping | Desactivat |
| Render Face | Front |
| Cast Shadows | Desactivat |

Per a Transparent, utilitza Blending Mode **Alpha**.

Afegeix al Blackboard aquestes propietats. Activa **Exposed** i escriu les **Reference exactes**, inclòs el guió baix. Els colors s’indiquen com a RGBA normalitzat; si el selector mostra bytes 0–255, multiplica cada component per 255.

| Nom | Tipus | Reference | Valor inicial |
|---|---|---|---|
| Color | Color | `_Color` | (0.015, 0.7, 1, 1) |
| Frequencia franges | Float | `_Frequencia` | 18 |
| Intensitat | Float | `_Intensitat` | 3 |
| Temps (controlat pel guio) | Float | `_Temps` | 0 |

Arrossega les propietats al gràfic per crear els seus nodes. La propietat no es crea escrivint-ne el nom en un node Float: s’ha de crear al Blackboard.

## 3. Entendre l’efecte

### Franges

La coordenada Y en espai Object, multiplicada per Frequencia i sumada a Temps, entra a Sine. Smoothstep entre 0.3 i 0.6 selecciona les bandes. La freqüència és angular per unitat local; el número del material no és un recompte exacte de franges.

### Vores

Fresnel amb Power = 3 destaca les zones on la superfície es veu de gairell. Maximum combina aquesta màscara amb les franges. No és una detecció de col·lisió ni un contorn geomètric nou.

### Transparència

La màscara barreja alpha entre 0.08 i 0.8 i controla l’emissió. La superfície és Transparent amb barreja Alpha; no activis Alpha Clipping. L’ordre de dibuix pot ser problemàtic amb diversos hologrames superposats.

## 4. Connectar els nodes

Cada fila identifica un node. `Eixos:R` vol dir la sortida **R** del node identificat com Eixos; `Nom:Out` vol dir la seva sortida. Si el nom correspon a una propietat del Blackboard, utilitza el seu node Property. Els números sense `:` són valors literals escrits al port d’entrada. Els àlies Temps, Mode i Centre corresponen a les propietats amb Reference `_Temps`, `_Mode` i `_Centre`, quan existeixen en aquesta demo. En un port vectorial, un literal únic s’ha de repetir a tots els components: per exemple Frequency = 12 vol dir (12,12).

Les captures següents provenen del Shader Graph de Unity. A les captures de cada pas s’han ocultat els cables que només travessen el grup sense connectar-hi; es mantenen les seves entrades i sortides. Alguns camps numèrics es veuen arrodonits per l’amplada del node: utilitza els valors exactes de la taula. Cada grup numerat és un pas del mateix graf final. Segueix l’ordre, afegeix els nodes del pas i connecta’ls com a la captura i a la taula. Les propietats es creen al Blackboard segons l’apartat 2; els nodes Property simplement les llegeixen.

Els cables que entren des de fora del grup provenen de passos anteriors: el nom de la taula identifica l’origen. Al projecte acabat pots seleccionar un grup i prémer **F** per enquadrar-lo. Les previsualitzacions dels nodes estan plegades a les captures per deixar llegir ports i valors; pots desplegar-les per inspeccionar una màscara.

### Connexions entre grups

Aquest mapa mostra com es connecten els grups del graf. Cada fletxa agrupa el nombre de cables indicat; la taula inferior detalla **tots els cables entre grups i cap al Master Stack**, amb els ports exactes. Les captures dels passos següents mostren les connexions interiors.

```mermaid
flowchart TD
    G1["01 · Franges animades"]
    G2["02 · Vora Fresnel i alpha"]
    G3["03 · Color i emissio"]
    G4["Sortides · Master Stack"]
    G1 -->|"1 cable"| G2
    G2 -->|"1 cable"| G3
    G2 -->|"1 cable"| G4
    G3 -->|"2 cables"| G4
```

Llegeix cada fila d’esquerra a dreta: **grup d’origen → node i port de sortida → grup de destinació → node i port d’entrada**. Per exemple, `Eixos:R` és el port R del node Eixos. Un mateix port pot alimentar diversos grups; conserva totes les branques indicades.

| Grup origen | Node:sortida | Grup destinació | Node:entrada |
|---|---|---|---|
| 01 | `Franges:Out` | 02 | `Mascara:A` |
| 02 | `Mascara:Out` | 03 | `IntensitatFinal:A` |
| 02 | `Transparencia:Out` | Master Stack | `Fragment:Alpha` |
| 03 | `Color:Out` | Master Stack | `Fragment:Base Color` |
| 03 | `Emissio:Out` | Master Stack | `Fragment:Emission` |

A Unity, allunya el zoom per veure dos grups alhora. **F** enquadra la selecció; **A** enquadra tot el graf. Per seguir un cable llarg, identifica primer els dos extrems a la taula i localitza els grups numerats.

### Pas 1. Franges animades

Position Object.y fixa les franges a l’objecte. Temps anima la fase sense moure la geometria.

<img src="assets/demoholograma-nodes-01.jpg" alt="Shader Graph Holograma, pas 1: Franges animades" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `UV` | UV |   |
| `Posicio` | Position | Space: Object |
| `Eixos` | Split | In ← Posicio:Out |
| `Altura` | Multiply | A ← Eixos:G; B ← Frequencia:Out |
| `Fase` | Add | A ← Altura:Out; B ← Temps:Out |
| `Ona` | Sine | In ← Fase:Out |
| `Franges` | Smoothstep | Edge1 ← 0.3; Edge2 ← 0.6; In ← Ona:Out |

### Pas 2. Vora Fresnel i alpha

Maximum combina les franges amb Fresnel. Lerp conserva un mínim d’alpha de 0.08.

<img src="assets/demoholograma-nodes-02.jpg" alt="Shader Graph Holograma, pas 2: Vora Fresnel i alpha" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `Vores` | Fresnel Effect | Power ← 3 |
| `Mascara` | Maximum | A ← Franges:Out; B ← Vores:Out |
| `Transparencia` | Lerp | A ← 0.08; B ← 0.8; T ← Mascara:Out |

### Pas 3. Color i emissio

Multiplica el color per la màscara i Intensitat per alimentar Emission.

<img src="assets/demoholograma-nodes-03.jpg" alt="Shader Graph Holograma, pas 3: Color i emissio" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `IntensitatFinal` | Multiply | A ← Mascara:Out; B ← Intensitat:Out |
| `Emissio` | Multiply | A ← Color:Out; B ← IntensitatFinal:Out |

### Pas 4. Master Stack i connexions finals

Connecta les branques als blocs del Master Stack indicats a continuació. Comprova l’espai de les normals i si les sortides són de Vertex o de Fragment. Els blocs sense cable conserven el valor per defecte.

<img src="assets/demoholograma-nodes-sortides.jpg" alt="Shader Graph Holograma, pas 4: Master Stack i connexions finals" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| Sortida | BaseColor | Color:Out |
| Sortida | Emission | Emissio:Out |
| Sortida | Alpha | Transparencia:Out |

Els números de les entrades són valors literals. En un vector, repeteix el valor a tots els components quan s’indica un únic nombre. A Combine, tria RG per a Vector2; a Split, R/G/B equivalen a X/Y/Z. Desa el graf abans de crear els materials.

## 5. Crear els materials

Amb el botó dret sobre SHolograma, crea els materials i assigna-hi el shader. `MHolograma0`, `MHolograma1`, `MHolograma2`: Frequencia = **12, 24, 36**. Color RGB: **(0.02,0.8,1), (0.4,0.15,1), (1,0.18,0.35)**.

## 6. Construir l’escena

Les posicions són globals i les escales corresponen a les primitives de Unity. Mantén els objectes a l’arrel, sense un pare escalat. Els elements decoratius i textos es poden ometre: no intervenen en el shader.

| Objecte | Primitiva | Posicio | Escala |
|---|---|---|---|
| Base | Cube | (0, -0.22, 0) | (12, 0.4, 8) |
| Holograma0 | Capsule | (-3.2, 2.15, 0) | (1.4, 1.3, 1.4) |
| Peanya0 | Cylinder | (-3.2, 0.24, 0) | (2.85, 0.23, 2.85) |
| Holograma1 | Sphere | (0, 2.15, 0) | (2.1, 2.1, 2.1) |
| Peanya1 | Cylinder | (0, 0.24, 0) | (2.85, 0.23, 2.85) |
| Holograma2 | Capsule | (3.2, 2.15, 0) | (1.4, 1.3, 1.4) |
| Peanya2 | Cylinder | (3.2, 0.24, 0) | (2.85, 0.23, 2.85) |

### Càmera i llums

Posa Main Camera a **(8,6,−14)**, amb projecció **Perspective**, Field of View **40**, Near **0.1**, Far **100** i fons de color sòlid **RGB (0.018,0.029,0.052)**. Orienta-la cap al punt **(0,1.7,0)**. La rotació Euler corresponent és aproximadament **(14.932, -29.745, 0)**.

Afegeix dues llums Directional: **Key Light**, rotació (45,−30,0), intensitat 2 i color RGB (1,0.88,0.74); **Fill Light**, rotació (25,140,0), intensitat 1 i color RGB (0.35,0.67,1). A Lighting > Environment, utilitza ambient de color (0.32,0.38,0.48). La base porta un material Lit gris blavós (0.055,0.072,0.095), Smoothness 0.25; les peanyes, (0.1,0.14,0.19).

Activa **HDR** i **Post Processing** a la càmera. Afegeix un **Global Volume**, crea un perfil i afegeix **Bloom**, activant els overrides: Intensity **0.35**, Threshold **1**, Scatter **0.6**.

## 7. Afegir els controls

### Càmera orbital i zoom

Descarrega [DemoOrbitCamera.cs](demos/DemoOrbitCamera.cs) i desa’l a **Assets/Common/DemoOrbitCamera.cs**. Afegeix el component **Demo Orbit Camera** a **Main Camera**. Al ZIP ja està configurat.

- **Center = (0, 1.7, 0)**: punt fix al voltant del qual gira la càmera.
- **Minimum Distance = 3**, **Maximum Distance = 35**.
- **Rotation Sensitivity = 0.2**, **Zoom Sensitivity = 0.0015**.
- **Minimum Elevation = 5**, **Maximum Elevation = 85**: límits d’inclinació en graus.

En **Play**, clica **Game**. Mantén premut el **botó dret** i arrossega per girar; utilitza la **roda del ratolí** per apropar-te o allunyar-te. **Home** recupera la posició i orientació inicials. El centre no es desplaça i no cal afegir-hi cap objecte Target. La roda positiva apropa la càmera; la negativa l’allunya.

El controlador del panell, que trobaràs a continuació, informa la càmera de quan el punter és sobre els controls perquè el lliscador no mogui el punt de vista. La càmera funciona igualment quan el temps del shader està en pausa.

Crea **Assets/Common/DemoControls.cs** amb aquest codi complet:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

// Controls comuns a les demos. Crea còpies dels materials per conservar els originals del projecte.
public class DemoControls : MonoBehaviour
{
    public string title;
    public string description;
    // Arrossega els objectes visibles als quals aquest panell modificarà el material.
    public Renderer[] surfaces;
    // Nom intern (Reference) de la propietat del Shader Graph que controlarà el lliscador.
    public string parameter = "_Gruix";
    public float minimum = 0.02f;
    public float maximum = 0.3f;
    public float initialValue = 0.12f;
    public float timeScale = 1f;
    public bool paused;
    // Temps propi de la demo: es pot pausar sense aturar la càmera ni tota la partida.
    public float elapsed;
    public bool showPanel = true;
    Material[] materials;
    float value;

    // Prepara els materials i la connexió amb la càmera quan es carrega el component.
    void Awake()
    {
        // Reserva una entrada per al material de cada superfície assignada a l'Inspector.
        materials = new Material[surfaces.Length];
        // Accedir a .material crea una còpia per a cada Renderer; així es conserven els materials del
        // projecte.
        for (int i = 0; i < surfaces.Length; i++) materials[i] = surfaces[i].material;
        value = initialValue;
        // Busca el controlador orbital a la càmera amb el tag MainCamera, si n'hi ha.
        var orbit = Camera.main ? Camera.main.GetComponent<DemoOrbitCamera>() : null;
        // Passa una funció a la càmera perquè detecti el punter sobre el panell i no mogui la vista en
        // usar-lo.
        if (orbit) orbit.IsPointerOverControls = point => showPanel && new Rect(16, 16, 380, 180).Contains(point);
    }
    void Update()
    {
        // Comprova que hi hagi teclat abans de llegir les tecles de control.
        if (Keyboard.current != null)
        {
            // Alterna la pausa amb una sola pulsació, sense repetir-la mentre la tecla es manté premuda.
            if (Keyboard.current.spaceKey.wasPressedThisFrame) paused = !paused;
            if (Keyboard.current.hKey.wasPressedThisFrame) showPanel = !showPanel;
            if (Keyboard.current.rKey.wasPressedThisFrame) { elapsed = 0; SetParameter(initialValue); }
        }
        // Avança el rellotge de l'efecte només si no està en pausa; timeScale en regula la velocitat.
        if (!paused) elapsed += Time.deltaTime * timeScale;
        // Envia aquest temps als shaders que tenen la propietat _Temps.
        ApplyTime(elapsed);
    }
    public void ApplyTime(float seconds)
    {
        if (materials == null) return;
        foreach (Material material in materials)
            // Comprova que la propietat existeixi abans d'actualitzar l'animació del material.
            if (material.HasProperty("_Temps")) material.SetFloat("_Temps", seconds);
    }
    public void SetParameter(float next)
    {
        // Limita el valor triat al rang permès per a aquesta demo.
        value = Mathf.Clamp(next, minimum, maximum);
        if (materials == null) return;
        foreach (Material material in materials)
            // Actualitza la propietat indicada pel seu nom intern a cada material compatible.
            if (material.HasProperty(parameter)) material.SetFloat(parameter, value);
    }
    // Unity crida OnGUI per dibuixar i gestionar aquest panell senzill de controls.
    void OnGUI()
    {
        if (!showPanel) return;
        GUI.Box(new Rect(16, 16, 380, 180), "");
        // Delimita en píxels l'àrea on GUILayout col·locarà els textos i el lliscador.
        GUILayout.BeginArea(new Rect(30, 25, 350, 162));
        GUILayout.Label(title);
        GUILayout.Label(description);
        GUILayout.Label(parameter + ": " + value.ToString("0.00"));
        // Dibuixa el lliscador i llegeix el valor que l'usuari hi selecciona.
        float next = GUILayout.HorizontalSlider(value, minimum, maximum);
        // Aplica el paràmetre als materials només quan l'usuari en canvia el valor.
        if (next != value) SetParameter(next);
        GUILayout.Label("Espai: pausa | R: reinicia | H: amaga el panell");
        GUILayout.Label("Boto dret: orbita | Roda: zoom | Home: vista inicial");
        GUILayout.EndArea();
    }
    void OnDestroy()
    {
        if (materials == null) return;
        // Allibera les còpies de materials creades a Awake quan es destrueix el component.
        foreach (Material material in materials) Destroy(material);
    }
}
```

Crea un objecte buit **Controls** i afegeix-hi DemoControls. A **Surfaces**, assigna els tres Renderers de les mostres, en ordre d’esquerra a dreta. No hi afegeixis la base, els textos ni les peanyes.

| Camp | Valor |
|---|---|
| Title | 05 / Holograma |
| Description | Compara les variants i modifica el material. |
| Parameter | `_Frequencia` |
| Minimum | 3 |
| Maximum | 50 |
| Initial Value | 18 |

**Controls:** lliscador per modificar `_Frequencia`; Espai pausa el temps; R reinicia temps i paràmetre; H amaga el panell. En moure el lliscador, el valor s’aplica a totes les mostres. Abans de tocar-lo, cada material conserva les variants de l’apartat 5. Els rètols de l’escena identifiquen aquestes variants inicials; el panell mostra el valor actual del control.

Els canvis durant Play són temporals. Per canviar els valors inicials de manera permanent, atura Play i edita els assets de material.

## 8. Provar la demo

1. Comprova les franges de les tres figures i l’animació vertical.
2. Prem Espai per congelar el temps.
3. Mou Frequencia: augmenta o disminueix la densitat de les franges.
4. Gira la càmera a l’editor: Fresnel continua destacant les zones vistes de gairell.

<img src="assets/demoholograma-variant.png" alt="Variant amb el lliscador al màxim" width="600" style="width: 90%; max-width: 600px; height: auto;">

<img src="assets/demoholograma-animacio.png" alt="Animació en un altre instant, amb el paràmetre inicial" width="600" style="width: 90%; max-width: 600px; height: auto;">

## Si alguna cosa no funciona

Si és opac, revisa Surface Type Transparent i la connexió Alpha. Si les franges no avancen, comprova `_Temps` i Surfaces.

Si el material és rosa, comprova URP, la compilació del gràfic i la Console. Si el teclat no respon, comprova Input System, entra a Play i clica Game.

## Ampliació

Multiplica l’emissió per una pulsació lenta i afegeix una franja vertical de lectura. Mantén independent el temps de les bandes i el del parpelleig.
