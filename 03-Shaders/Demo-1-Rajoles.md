# Demo 1: Rajoles: ceràmica, juntes i desgast

Construirem un material Lit de rajoles amb juntes fosques, alternança de colors, variació procedural i relleu aparent. L’escena compara ceràmica blava, terracota i esmalt verd. El patró es calcula al shader; no necessita imatges externes.

**Projecte acabat:** [Demo-1-Rajoles.zip](demos/Demo-1-Rajoles.zip). Descomprimeix-lo i obre la carpeta amb Assets, Packages i ProjectSettings des de Unity Hub. Obre `Assets/Demos/Rajoles/Scenes/DemoRajoles.unity` i prem Play.

<img src="assets/demorajoles-escena.png" alt="Escena de la demo Rajoles renderitzada a Unity" width="600" style="width: 90%; max-width: 600px; height: auto;">

## 1. Preparar un projecte buit

1. Utilitza **Unity 6000.6.3f1**, la versió de les demos de Codi. Crea un projecte **Universal 3D (URP)**.
2. A Package Manager, comprova **Universal RP 17.6.0**, **Shader Graph 17.6.0** i **Input System 1.20.0**. Shader Graph arriba com a dependència d’URP.
3. A **Project Settings > Player > Active Input Handling**, selecciona **Input System Package (New)**. Accepta el reinici si Unity el demana.
4. Crea `Assets/Demos/Rajoles` amb les carpetes **Shaders**, **Materials**, **Scenes** i **Meshes**. Crea també **Assets/Common** per als scripts.
5. Crea una escena buida, afegeix una Camera amb tag **MainCamera** i desa l’escena com a `DemoRajoles` dins de Scenes.

## 2. Crear el Shader Graph

A Shaders, crea **Shader Graph > URP > Lit Shader Graph**, amb nom **SRajoles**. A Graph Inspector > Graph Settings, fixa:

| Opció | Valor |
|---|---|
| Target | Universal |
| Material | Lit |
| Surface Type | Opaque |
| Alpha Clipping | Desactivat |
| Render Face | Front |
| Cast Shadows | Desactivat |

Afegeix al Blackboard aquestes propietats. Activa **Exposed** i escriu les **Reference exactes**, inclòs el guió baix. Els colors s’indiquen com a RGBA normalitzat; si el selector mostra bytes 0–255, multiplica cada component per 255.

| Nom | Tipus | Reference | Valor inicial |
|---|---|---|---|
| Brillantor | Float | `_Brillantor` | 0.65 |
| ColorA | Color | `_ColorA` | (0.025, 0.19, 0.25, 1) |
| ColorB | Color | `_ColorB` | (0.85, 0.7, 0.32, 1) |
| Gruix | Float | `_Gruix` | 0.065 |
| Junta | Color | `_Junta` | (0.065, 0.075, 0.08, 1) |
| Repeticions | Float | `_Repeticions` | 6 |

Arrossega les propietats al gràfic per crear els seus nodes. La propietat no es crea escrivint-ne el nom en un node Float: s’ha de crear al Blackboard.

## 3. Entendre l’efecte

### Juntes i interior

Les branques de Fraction i Step creen les línies de cada eix. Maximum n’uneix les màscares; l’interior de les rajoles és One Minus d’aquesta quadrícula.

### Color i desgast

Floor i la paritat trien ColorA o ColorB. Simple Noise introdueix una variació d’intensitat entre 0.65 i 1 abans de combinar el color de la rajola amb el de la junta.

### Relleu aparent

Normal From Height converteix la màscara d’interior en una variació de la normal tangent. Strength és 0.035: augmentar-lo massa dona vores exagerades. La silueta continua sent la d’un cub; no s’ha modificat la geometria.

## 4. Connectar els nodes

Cada fila identifica un node. `Eixos:R` vol dir la sortida **R** del node identificat com Eixos; `Nom:Out` vol dir la seva sortida. Si el nom correspon a una propietat del Blackboard, utilitza el seu node Property. Els números sense `:` són valors literals escrits al port d’entrada. Els àlies Temps, Mode i Centre corresponen a les propietats amb Reference `_Temps`, `_Mode` i `_Centre`, quan existeixen en aquesta demo. En un port vectorial, un literal únic s’ha de repetir a tots els components: per exemple Frequency = 12 vol dir (12,12).

Les captures següents provenen del Shader Graph de Unity. A les captures de cada pas s’han ocultat els cables que només travessen el grup sense connectar-hi; es mantenen les seves entrades i sortides. Alguns camps numèrics es veuen arrodonits per l’amplada del node: utilitza els valors exactes de la taula. Cada grup numerat és un pas del mateix graf final. Segueix l’ordre, afegeix els nodes del pas i connecta’ls com a la captura i a la taula. Les propietats es creen al Blackboard segons l’apartat 2; els nodes Property simplement les llegeixen.

Els cables que entren des de fora del grup provenen de passos anteriors: el nom de la taula identifica l’origen. Al projecte acabat pots seleccionar un grup i prémer **F** per enquadrar-lo. Les previsualitzacions dels nodes estan plegades a les captures per deixar llegir ports i valors; pots desplegar-les per inspeccionar una màscara.

### Connexions entre grups

Aquest mapa mostra com es connecten els grups del graf. Cada fletxa agrupa el nombre de cables indicat; la taula inferior detalla **tots els cables entre grups i cap al Master Stack**, amb els ports exactes. Les captures dels passos següents mostren les connexions interiors.

```mermaid
flowchart TD
    G1["01 · UV i repeticio"]
    G2["02 · Juntes"]
    G3["03 · Alternanca"]
    G4["04 · Color i desgast"]
    G5["05 · Juntes i relleu"]
    G6["Sortides · Master Stack"]
    G1 -->|"2 cables"| G2
    G1 -->|"2 cables"| G3
    G1 -->|"1 cable"| G4
    G2 -->|"2 cables"| G5
    G3 -->|"1 cable"| G4
    G4 -->|"1 cable"| G5
    G5 -->|"3 cables"| G6
```

Llegeix cada fila d’esquerra a dreta: **grup d’origen → node i port de sortida → grup de destinació → node i port d’entrada**. Per exemple, `Eixos:R` és el port R del node Eixos. Un mateix port pot alimentar diversos grups; conserva totes les branques indicades.

| Grup origen | Node:sortida | Grup destinació | Node:entrada |
|---|---|---|---|
| 01 | `FaseU:Out` | 02 | `TallU:In` |
| 01 | `FaseV:Out` | 02 | `TallV:In` |
| 01 | `Eixos:R` | 03 | `CelU:In` |
| 01 | `Eixos:G` | 03 | `CelV:In` |
| 01 | `UV:Out` | 04 | `Desgast:UV` |
| 03 | `Alternat:Out` | 04 | `Rajola:T` |
| 02 | `Quadricula:Out` | 05 | `ColorFinal:T` |
| 02 | `Quadricula:Out` | 05 | `Interior:In` |
| 04 | `ColorDesgast:Out` | 05 | `ColorFinal:A` |
| 05 | `ColorFinal:Out` | Master Stack | `Fragment:Base Color` |
| 05 | `Relleu:Out` | Master Stack | `Fragment:Normal (Tangent Space)` |
| 05 | `Brillantor:Out` | Master Stack | `Fragment:Smoothness` |

A Unity, allunya el zoom per veure dos grups alhora. **F** enquadra la selecció; **A** enquadra tot el graf. Per seguir un cable llarg, identifica primer els dos extrems a la taula i localitza els grups numerats.

### Pas 1. UV i repeticio

Parteix de les UV i separa els dos eixos per repetir les cel·les.

<img src="assets/demorajoles-nodes-01.jpg" alt="Shader Graph Rajoles, pas 1: UV i repeticio" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `UV` | UV |   |
| `Escala` | Multiply | A ← UV:Out; B ← Repeticions:Out |
| `Eixos` | Split | In ← Escala:Out |
| `FaseU` | Fraction | In ← Eixos:R |
| `FaseV` | Fraction | In ← Eixos:G |

### Pas 2. Juntes

La màscara blanca representa les juntes. Augmentar Gruix eixampla les dues direccions.

<img src="assets/demorajoles-nodes-02.jpg" alt="Shader Graph Rajoles, pas 2: Juntes" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `TallU` | Step | Edge ← Gruix:Out; In ← FaseU:Out |
| `LiniaU` | One Minus | In ← TallU:Out |
| `TallV` | Step | Edge ← Gruix:Out; In ← FaseV:Out |
| `LiniaV` | One Minus | In ← TallV:Out |
| `Quadricula` | Maximum | A ← LiniaU:Out; B ← LiniaV:Out |

### Pas 3. Alternanca

La paritat alterna les rajoles; no utilitzis Fraction directament sense identificar abans la cel·la amb Floor.

<img src="assets/demorajoles-nodes-03.jpg" alt="Shader Graph Rajoles, pas 3: Alternanca" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `CelU` | Floor | In ← Eixos:R |
| `CelV` | Floor | In ← Eixos:G |
| `SumaCels` | Add | A ← CelU:Out; B ← CelV:Out |
| `Meitat` | Multiply | A ← SumaCels:Out; B ← 0.5 |
| `Paritat` | Fraction | In ← Meitat:Out |
| `Alternat` | Multiply | A ← Paritat:Out; B ← 2 |

### Pas 4. Color i desgast

Barreja els colors de ceràmica i multiplica el resultat per una variació entre 0.65 i 1.

<img src="assets/demorajoles-nodes-04.jpg" alt="Shader Graph Rajoles, pas 4: Color i desgast" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `Rajola` | Lerp | A ← ColorA:Out; B ← ColorB:Out; T ← Alternat:Out |
| `Desgast` | Simple Noise | UV ← UV:Out; Scale ← 65 |
| `Variacio` | Lerp | A ← 0.65; B ← 1; T ← Desgast:Out |
| `ColorDesgast` | Multiply | A ← Rajola:Out; B ← Variacio:Out |

### Pas 5. Juntes i relleu

Lerp introdueix el color de la junta. One Minus dona l’interior de rajola per generar les normals.

<img src="assets/demorajoles-nodes-05.jpg" alt="Shader Graph Rajoles, pas 5: Juntes i relleu" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| `ColorFinal` | Lerp | A ← ColorDesgast:Out; B ← Junta:Out; T ← Quadricula:Out |
| `Interior` | One Minus | In ← Quadricula:Out |
| `Relleu` | Normal From Height | In ← Interior:Out; Strength ← 0.035 |

### Pas 6. Master Stack i connexions finals

Connecta les branques als blocs del Master Stack indicats a continuació. Comprova l’espai de les normals i si les sortides són de Vertex o de Fragment. Els blocs sense cable conserven el valor per defecte.

<img src="assets/demorajoles-nodes-sortides.jpg" alt="Shader Graph Rajoles, pas 6: Master Stack i connexions finals" width="600" style="width: 90%; max-width: 600px; height: auto;">

| ID | Node | Entrades |
|---|---|---|
| Sortida | Normal (Tangent) | Relleu:Out |
| Sortida | Smoothness | Brillantor:Out |
| Sortida | BaseColor | ColorFinal:Out |

Els números de les entrades són valors literals. En un vector, repeteix el valor a tots els components quan s’indica un únic nombre. A Combine, tria RG per a Vector2; a Split, R/G/B equivalen a X/Y/Z. Desa el graf abans de crear els materials.

## 5. Crear els materials

Amb el botó dret sobre SRajoles, crea els materials i assigna-hi el shader. `MRajoles0`, `MRajoles1`, `MRajoles2`: Repeticions = **4, 6, 8** i Brillantor = **0.3, 0.6, 0.9**. ColorA en RGB és (0.025,0.22,0.29), (0.45,0.12,0.07) i (0.08,0.24,0.14), respectivament. La resta de propietats conserva els valors inicials.

## 6. Construir l’escena

Les posicions són globals i les escales corresponen a les primitives de Unity. Mantén els objectes a l’arrel, sense un pare escalat. Els elements decoratius i textos es poden ometre: no intervenen en el shader.

| Objecte | Primitiva | Posicio | Escala |
|---|---|---|---|
| Base | Cube | (0, -0.22, 0) | (12, 0.4, 8) |
| Mostra0 | Cube | (-3.2, 2.25, 0) | (2.7, 2.7, 0.35) |
| Peanya0 | Cylinder | (-3.2, 0.24, 0) | (2.85, 0.23, 2.85) |
| Mostra1 | Cube | (0, 2.25, 0) | (2.7, 2.7, 0.35) |
| Peanya1 | Cylinder | (0, 0.24, 0) | (2.85, 0.23, 2.85) |
| Mostra2 | Cube | (3.2, 2.25, 0) | (2.7, 2.7, 0.35) |
| Peanya2 | Cylinder | (3.2, 0.24, 0) | (2.85, 0.23, 2.85) |

Aplica una rotació Y = **−8°** a les tres mostres i assigna el material numerat que correspon a cadascuna.

### Càmera i llums

Posa Main Camera a **(8,6,−14)**, amb projecció **Perspective**, Field of View **40**, Near **0.1**, Far **100** i fons de color sòlid **RGB (0.018,0.029,0.052)**. Orienta-la cap al punt **(0,1.7,0)**. La rotació Euler corresponent és aproximadament **(14.932, -29.745, 0)**.

Afegeix dues llums Directional: **Key Light**, rotació (45,−30,0), intensitat 2 i color RGB (1,0.88,0.74); **Fill Light**, rotació (25,140,0), intensitat 1 i color RGB (0.35,0.67,1). A Lighting > Environment, utilitza ambient de color (0.32,0.38,0.48). La base porta un material Lit gris blavós (0.055,0.072,0.095), Smoothness 0.25; les peanyes, (0.1,0.14,0.19).

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
| Title | 01 / Rajoles |
| Description | Compara les variants i modifica el material. |
| Parameter | `_Gruix` |
| Minimum | 0.015 |
| Maximum | 0.2 |
| Initial Value | 0.065 |

**Controls:** lliscador per modificar `_Gruix`; Espai pausa el temps; R reinicia temps i paràmetre; H amaga el panell. En moure el lliscador, el valor s’aplica a totes les mostres. Abans de tocar-lo, cada material conserva les variants de l’apartat 5. Els rètols de l’escena identifiquen aquestes variants inicials; el panell mostra el valor actual del control.

Els canvis durant Play són temporals. Per canviar els valors inicials de manera permanent, atura Play i edita els assets de material.

## 8. Provar la demo

1. Comprova els tres colors de ceràmica i el nombre creixent de cel·les.
2. Mou Gruix fins a 0.2: les juntes ocupen una part visible de cada rajola.
3. Atura Play, canvia Brillantor i gira la llum: varia el reflex sense canviar les UV.
4. Desconnecta temporalment NormalTS: desapareix el relleu aparent, però es mantenen color i juntes.

<img src="assets/demorajoles-variant.png" alt="Variant amb el lliscador al màxim" width="600" style="width: 90%; max-width: 600px; height: auto;">

## Si alguna cosa no funciona

Si només veus juntes, revisa Gruix (fracció de cel·la, no píxels). Si el relleu és exagerat, revisa Strength de Normal From Height i l’espai Tangent.

Si el material és rosa, comprova URP, la compilació del gràfic i la Console. Si el teclat no respon, comprova Input System, entra a Play i clica Game.

## Ampliació

Substitueix la paritat per colors que depenguin de l’índex de cel·la. Després limita el desgast a les vores de les rajoles.
