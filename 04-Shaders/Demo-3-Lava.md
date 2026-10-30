# Demo 3: Lava: esquerdes i emissió animada

Construirem una superfície fosca travessada per esquerdes calentes. El temps desplaça el camp de soroll i fa variar l’emissió. Tres materials mostren intensitats i velocitats diferents.

**Projecte acabat:** [Demo-3-Lava.zip](demos/Demo-3-Lava.zip). Descomprimeix-lo i obre la carpeta amb Assets, Packages i ProjectSettings des de Unity Hub. La primera importació obre automàticament `Assets/Demos/Lava/Scenes/DemoLava.unity`; prem Play. Els materials poden trigar una estona a carregar-se.

<a href="assets/demolava-escena.png"><img src="assets/demolava-escena.png" alt="Escena de la demo Lava renderitzada a Unity" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>

## 1. Partir de Universal 3D

1. Crea un projecte **Universal 3D (URP)**.
2. A **Window > Package Management > Package Manager**, comprova **Universal RP**, **Shader Graph** i **Input System**. Shader Graph arriba com a dependència d’URP.
3. A **Edit > Project Settings > Player > Other Settings > Active Input Handling**, selecciona **Input System Package (New)**. Accepta el reinici si Unity el demana.
4. Crea `Assets/Demos/Lava` amb les carpetes **Shaders**, **Materials** i **Scenes**. Crea també **Assets/Common** per als scripts.
5. Obre **Assets/Scenes/SampleScene**, que ja inclou **Main Camera**, **Directional Light** i **Global Volume**. Conserva aquests objectes. Amb **File > Save As…**, desa una còpia com a `DemoLava` dins de Scenes. Més endavant ajustarem la càmera i la llum existents.

## 2. Crear el Shader Graph

A Shaders, crea **Create > Shader Graph > URP > Lit Shader Graph**, amb nom **SLava**. Al **Blackboard**, fes doble clic al subtítol **Shader Graphs** sota el nom del graf i escriu **DAM Shaders**: és la carpeta on apareixerà el shader al selector del material. A Graph Inspector > Graph Settings, fixa:

| Opció | Valor |
|---|---|
| Target | Universal |
| Material | Lit |
| Workflow Mode | Metallic |
| Surface Type | Opaque |
| Alpha Clipping | Desactivat |
| Render Face | Front |
| Cast Shadows | Desactivat |
| Fragment Normal Space | Tangent |

Afegeix al Blackboard aquestes propietats. Activa **Show In Inspector** i escriu les **Reference exactes**, inclòs el guió baix. Els noms s’escriuen sense accents, igual que al projecte acabat. Els colors s’indiquen com a **RGB (0–255)**, tal com apareixen al selector de Unity; mantén **Alpha = 255** llevat que s’indiqui un altre valor. Els valors Float i Vector continuen en les seves unitats originals.

| Nom | Tipus | Reference | Valor inicial |
|---|---|---|---|
| Escorca | Color | `_Escorca` | RGB (5, 7, 9) |
| Groc | Color | `_Groc` | RGB (255, 166, 13) |
| Intensitat emissio | Float | `_Intensitat` | 3 |
| Taronja | Color | `_Taronja` | RGB (255, 18, 1) |
| Temps (controlat pel guio) | Float | `_Temps` | 0 |
| Velocitat | Float | `_Velocitat` | 0.18 |

Arrossega les propietats al gràfic per crear els seus nodes. La propietat no es crea escrivint-ne el nom en un node Float: s’ha de crear al Blackboard.

## 3. Connectar els nodes

Segueix els passos en ordre dins del mateix Shader Graph. Prem **Espai** per crear un node del tipus indicat; arrossega les propietats des del Blackboard. Els identificadors de les taules serveixen per distingir nodes repetits: no cal canviar els noms dels nodes. Si ho necessites, posa una **Sticky Note** al costat del node amb l’identificador.

A cada fila de cables, arrossega des del **port de sortida** del node origen fins al **port d’entrada** del node destí. Reutilitza els nodes dels passos anteriors. `Out` indica la sortida principal; als nodes de propietat pot mostrar el nom de la propietat.

Per organitzar el graf com a la captura, selecciona els nodes de cada pas i tria **Group Selection** al menú del botó dret. Mantén els grups a prop i deixa prou espai per veure els cables.

Per veure el resultat d’un node, fes-hi botó dret i tria **View > Expand Preview**; amb **Collapse Preview** el pots plegar.

Escriu els valors de la taula als camps sense cable. Si el camp és vectorial i només s’indica un valor, repeteix-lo a tots els components. Clica les captures per veure-les a mida completa.

**Graf complet:** segueix els grups numerats fins al **Master Stack**. Les previsualitzacions mostren les esquerdes, els colors i l’emissió. [Obre la imatge a mida completa](assets/demolava-graf-complet.png) per llegir els ports i seguir tots els cables.

<a href="assets/demolava-graf-complet.png"><img src="assets/demolava-graf-complet.png" alt="Graf complet de Lava amb totes les connexions i les previsualitzacions" width="1600" style="width: 100%; max-width: 1600px; height: auto;"></a>

### Pas 1. Animació UV

Les UV es desplacen amb Temps × Velocitat. Combine envia un moviment diferent a cada eix.

**Nodes que has de crear i valors que has d’escriure:**

| Identificador de la guia | Tipus a cercar a Unity | Configuració / valors sense cable |
|---|---|---|
| `UV` | UV | Channel = UV0 |
| `TempsEscalat` | Multiply | Valors per defecte |
| `MigTemps` | Multiply | B = 0.4 |
| `Desplacament` | Combine | Valors per defecte |
| `UVAnimada` | Add | Valors per defecte |

**Cables: arrossega de la sortida a l’entrada indicada.**

| Node origen | Port de sortida | Node destí | Port d’entrada |
|---|---|---|---|
| `Temps (controlat pel guio)` | Out | `TempsEscalat` | A |
| `Velocitat` | Out | `TempsEscalat` | B |
| `TempsEscalat` | Out | `MigTemps` | A |
| `TempsEscalat` | Out | `Desplacament` | R |
| `MigTemps` | Out | `Desplacament` | G |
| `UV` | Out | `UVAnimada` | A |
| `Desplacament` | RG | `UVAnimada` | B |

**Comprova el resultat:** Canvia temporalment el valor per defecte de Temps (controlat pel guio) al Graph Inspector per comprovar el desplaçament de les UV; torna’l a 0. L’animació contínua començarà quan afegeixis els controls de l’escena.

### Pas 2. Màscara de les esquerdes

La proximitat del soroll a 0.5 defineix les esquerdes. Absolute tracta igual els dos costats.

**Nodes que has de crear i valors que has d’escriure:**

| Identificador de la guia | Tipus a cercar a Unity | Configuració / valors sense cable |
|---|---|---|
| `Camp` | Simple Noise | Scale = 38; Hash Type = Deterministic |
| `Centrat` | Subtract | B = 0.5 |
| `DistanciaEsquerda` | Absolute | Valors per defecte |
| `Vores` | Smoothstep | Edge1 = 0.008; Edge2 = 0.035 |
| `Esquerdes` | One Minus | Valors per defecte |

**Cables: arrossega de la sortida a l’entrada indicada.**

| Node origen | Port de sortida | Node destí | Port d’entrada |
|---|---|---|---|
| `UVAnimada` | Out | `Camp` | UV |
| `Camp` | Out | `Centrat` | A |
| `Centrat` | Out | `DistanciaEsquerda` | In |
| `DistanciaEsquerda` | Out | `Vores` | In |
| `Vores` | Out | `Esquerdes` | In |

**Comprova el resultat:** Desplega **Absolute** i **One Minus** (Esquerdes): el primer mostra la distància al llindar i el segon línies clares estretes sobre un fons fosc.

### Pas 3. Colors

La màscara barreja l’escorça fosca amb els colors calents.

**Nodes que has de crear i valors que has d’escriure:**

| Identificador de la guia | Tipus a cercar a Unity | Configuració / valors sense cable |
|---|---|---|
| `Calor` | Lerp | Valors per defecte |
| `ColorFinal` | Lerp | Valors per defecte |

**Cables: arrossega de la sortida a l’entrada indicada.**

| Node origen | Port de sortida | Node destí | Port d’entrada |
|---|---|---|---|
| `Taronja` | Out | `Calor` | A |
| `Groc` | Out | `Calor` | B |
| `Esquerdes` | Out | `Calor` | T |
| `Escorca` | Out | `ColorFinal` | A |
| `Calor` | Out | `ColorFinal` | B |
| `Esquerdes` | Out | `ColorFinal` | T |

**Comprova el resultat:** Desplega l’últim **Lerp** (ColorFinal): les esquerdes han de tenir colors càlids i la resta, el color Escorca.

### Pas 4. Pulsació i emissió

El sinus controla la pulsació. La màscara limita l’emissió a les esquerdes, mentre Normal From Height afecta el relleu aparent.

**Nodes que has de crear i valors que has d’escriure:**

| Identificador de la guia | Tipus a cercar a Unity | Configuració / valors sense cable |
|---|---|---|
| `Sinus` | Sine | Valors per defecte |
| `Pulsacio` | Add | A = 1.5 |
| `Forca` | Multiply | Valors per defecte |
| `Energia` | Multiply | Valors per defecte |
| `Emissio` | Multiply | Valors per defecte |
| `Relleu` | Normal From Height | Strength = 0.002; Output Space = Tangent |
| `Rugositat` | Float | X = 0.15 |

**Cables: arrossega de la sortida a l’entrada indicada.**

| Node origen | Port de sortida | Node destí | Port d’entrada |
|---|---|---|---|
| `Temps (controlat pel guio)` | Out | `Sinus` | In |
| `Sinus` | Out | `Pulsacio` | B |
| `Pulsacio` | Out | `Forca` | A |
| `Intensitat emissio` | Out | `Forca` | B |
| `Esquerdes` | Out | `Energia` | A |
| `Forca` | Out | `Energia` | B |
| `Calor` | Out | `Emissio` | A |
| `Energia` | Out | `Emissio` | B |
| `Camp` | Out | `Relleu` | In |

**Comprova el resultat:** Desplega **Multiply** (Emissio). Les esquerdes han d’aportar el color emissiu; la resplendor de Bloom es comprovarà a l’escena.

### Pas 5. Master Stack i connexions finals

Connecta les sortides als blocs del Master Stack indicats a la taula de cables. El bloc **Normal (Tangent Space)** apareix perquè Fragment Normal Space és Tangent. Els blocs sense cable conserven el valor per defecte.

**Cables: arrossega de la sortida a l’entrada indicada.**

| Node origen | Port de sortida | Node destí | Port d’entrada |
|---|---|---|---|
| `ColorFinal` | Out | `Fragment / Base Color` | Base Color |
| `Emissio` | Out | `Fragment / Emission` | Emission |
| `Relleu` | Out | `Fragment / Normal (Tangent Space)` | Normal (Tangent Space) |
| `Rugositat` | Out | `Fragment / Smoothness` | Smoothness |

Els números de les entrades són valors literals. En un vector, repeteix el valor a tots els components quan s’indica un únic nombre. A Combine, utilitza la sortida RG (Vector 2). Desa el graf abans de crear els materials.

**Comprova el resultat:** Desa el graf. Comprova el color i l’emissió sobre la mostra; activa el Bloom indicat a l’apartat de l’escena.

## 4. Crear els materials

A la carpeta **Materials**, utilitza **Create > Material** per a cada material indicat. A l’Inspector, selecciona **Shader > DAM Shaders > SLava** (o arrossega SLava del Project sobre el material). `MLava0`, `MLava1`, `MLava2`: Intensitat emissio = **1, 3, 5** i Velocitat = **0.09, 0.17, 0.25**, respectivament. La resta de propietats conserva els valors inicials.

## 5. Construir l’escena

Les posicions són globals i les escales corresponen a les primitives de Unity. Mantén els objectes a l’arrel, sense un pare escalat. Assigna MLava0, MLava1 i MLava2 a Nucli0, Nucli1 i Nucli2. Els elements decoratius i textos es poden ometre: no intervenen en el shader.

| Objecte | Primitiva | Posició | Escala |
|---|---|---|---|
| Base | Cube | (0, -0.22, 0) | (12, 0.4, 8) |
| Nucli0 | Sphere | (-3.2, 1.9, 0) | (2.4, 2.4, 2.4) |
| Peanya0 | Cylinder | (-3.2, 0.24, 0) | (2.85, 0.23, 2.85) |
| Nucli1 | Sphere | (0, 1.9, 0) | (2.4, 2.4, 2.4) |
| Peanya1 | Cylinder | (0, 0.24, 0) | (2.85, 0.23, 2.85) |
| Nucli2 | Sphere | (3.2, 1.9, 0) | (2.4, 2.4, 2.4) |
| Peanya2 | Cylinder | (3.2, 0.24, 0) | (2.85, 0.23, 2.85) |

### Càmera i llums

Posa Main Camera a **(8,6,−14)**, amb projecció **Perspective**, Field of View **40**, Near **0.1**, Far **100** i fons de color sòlid **RGB (5,7,13)**. Orienta-la cap al punt **(0,1.7,0)**. La rotació Euler corresponent és aproximadament **(14.932, -29.745, 0)**.

Selecciona la **Directional Light** existent, canvia-li el nom a **Key Light** i ajusta-la: rotació (45,−30,0), intensitat 2 i color RGB (255,224,189); afegeix una segona llum amb **GameObject > Light > Directional Light**, anomena-la **Fill Light** i configura-la: rotació (25,140,0), intensitat 1 i color RGB (89,171,255). A **Window > Rendering > Lighting**, pestanya **Environment**, posa **Environment Lighting > Source = Color** i **Ambient Color** (82,97,122). La base porta un material Lit **Base** gris blavós RGB (14,18,24); les peanyes, un material Lit **Pedestal** RGB (26,36,48). Tots dos tenen Smoothness 0.25. Crea aquests materials a Materials també amb **Create > Material**, selecciona **Shader > Universal Render Pipeline > Lit** i assigna’ls als objectes corresponents.

A **Main Camera**, activa **Rendering > Post Processing** i **Output > HDR Rendering**. Selecciona el **Global Volume** existent; duplica el seu perfil a la carpeta de la demo i assigna-hi la còpia. Conserva només **Bloom** al perfil de la demo (elimina els altres overrides per reproduir aquesta il·luminació) i activa els seus overrides: Intensity **0.35**, Threshold **1**, Scatter **0.6**.

## 6. Afegir els controls

### Càmera orbital i zoom

Descarrega [DemoOrbitCamera.cs](demos/DemoOrbitCamera.cs) i arrossega el fitxer a la carpeta **Assets/Common** de la finestra **Project**. Afegeix el component **Demo Orbit Camera** a **Main Camera**. Al ZIP ja està configurat.

- **Center = (0, 1.7, 0)**: punt fix al voltant del qual gira la càmera.
- **Minimum Distance = 3**, **Maximum Distance = 35**.
- **Rotation Sensitivity = 0.2**, **Zoom Sensitivity = 0.0015**.
- **Minimum Elevation = 5**, **Maximum Elevation = 85**: límits d’inclinació en graus.

En **Play**, clica **Game**. Mantén premut el **botó dret** i arrossega per girar; utilitza la **roda del ratolí** per apropar-te o allunyar-te. **Home** recupera la posició i orientació inicials. El centre no es desplaça i no cal afegir-hi cap objecte Target. La roda positiva apropa la càmera; la negativa l’allunya.

El controlador del panell, que trobaràs a continuació, informa la càmera de quan el punter és sobre els controls perquè el lliscador no mogui el punt de vista. La càmera funciona igualment quan el temps del shader està en pausa.

A **Project > Assets/Common**, fes clic dret i tria **Create > Scripting > MonoBehaviour Script**. Anomena’l **DemoControls**, obre’l i substitueix-ne tot el contingut per aquest codi:

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

Crea un objecte buit **Controls** i afegeix-hi DemoControls. A **Surfaces**, assigna els tres Renderers de les esferes (Nucli0, Nucli1 i Nucli2), en ordre d’esquerra a dreta. No hi afegeixis la base, els textos ni les peanyes.

| Camp | Valor |
|---|---|
| Title | 03 / Lava |
| Description | Compara les variants i modifica el material. |
| Parameter | `_Intensitat` |
| Minimum | 0 |
| Maximum | 8 |
| Initial Value | 3 |

**Controls:** lliscador per modificar `_Intensitat`; Espai pausa el temps; R reinicia temps i paràmetre; H amaga el panell. En moure el lliscador, el valor s’aplica a totes les mostres. Abans de tocar-lo, cada material conserva les variants de l’apartat 4. Els rètols de l’escena identifiquen aquestes variants inicials; el panell mostra el valor actual del control.

Els canvis durant Play són temporals. Per canviar els valors inicials de manera permanent, atura Play i edita els assets de material.

## 7. Provar la demo

1. Prem Play: canvien les esquerdes i la pulsació.
2. Prem Espai: el patró queda congelat; torna a prémer per continuar.
3. Mou Intensitat a 0: desapareix l’emissió, però continua existint el color de la superfície.
4. Prem R: el temps torna a zero i la intensitat al valor del controlador.

<a href="assets/demolava-variant.png"><img src="assets/demolava-variant.png" alt="Lliscador Intensitat al màxim (8): les esquerdes brillen més a totes les mostres" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>

<a href="assets/demolava-intensitat0.png"><img src="assets/demolava-intensitat0.png" alt="Intensitat a 0: desapareix l’emissió i es manté el color de la superfície" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>

<a href="assets/demolava-animacio.png"><img src="assets/demolava-animacio.png" alt="Animació en un altre instant, amb el paràmetre inicial" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>

## Si alguna cosa no funciona

Si no s’anima, comprova la Reference `_Temps`, la llista Surfaces del controlador i que no estigui pausat. Si no hi ha resplendor, comprova HDR, Post Processing i Bloom.

Si el material és rosa, comprova URP, la compilació del gràfic i la Console. Si el teclat no respon, comprova Input System, entra a Play i clica Game.
