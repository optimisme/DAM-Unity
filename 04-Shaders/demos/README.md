# Projectes de les demos de shaders

Cada ZIP conté un **projecte Unity independent** amb escenes acabades, Shader Graphs editables, materials i controls per experimentar. No cal importar cap demo anterior ni baixar textures de tercers: els recursos necessaris són dins dels projectes. Aigua inclou quatre textures pròpies. Pèls inclou un mapa d’altures de pèl fi, el generador de recursos i una escena amb shells i POM esfèric.

## Requisits

- **Unity** amb un projecte Universal 3D (URP).
- **Universal RP**, **Shader Graph** i **Input System**. Les dependències es resolen amb Package Manager.
- Internet durant la primera obertura si els paquets no són a la memòria cau.

## Obrir una demo

1. Descarrega el ZIP i descomprimeix-lo completament.
2. A Unity Hub, utilitza **Add > Add project from disk**.
3. Selecciona la carpeta que conté directament **Assets**, **Packages** i **ProjectSettings**.
4. Obre-la amb **Unity** i espera que acabi la importació.
5. En acabar la primera importació, s’obre automàticament l’escena principal de la demo. Els materials poden trigar una estona a carregar-se. Aigua també inclou una segona escena, `DemoAiguaGalaxy`, que pots obrir amb doble clic des de `Assets/Demos/Aigua/Scenes/` a la finestra **Project**.
6. Prem **Play** i clica la vista **Game**. A **Demo 0: Voronoi animat**, selecciona **Assets/Materials/MVoronoiAnimat** a la finestra **Project** per canviar **Color** i **Velocitat X/Y** a l’Inspector. A les altres demos, el panell mostra els controls.
7. Atura Play abans d’editar valors que vulguis conservar als materials.

El menú **Demo > Obrir escena principal** permet tornar a la demo. L’obertura automàtica només actua el primer cop; després es respecten les escenes amb què estiguis treballant.

## Descàrregues

Les mides són MB decimals aproximats. La mida descomprimida no inclou la memòria cau que Unity generarà.

| Projecte | Escena | ZIP | Descomprimit | Tutorial |
|---|---|---:|---:|---|
| [Demo 0: Voronoi animat](Demo-0-Voronoi.zip) | `SegonShader.unity` | 0.06 MB | 0.21 MB | [Tutorial](../Demo-0-Voronoi.md) |
| [Demo-1-Rajoles](Demo-1-Rajoles.zip) | `DemoRajoles.unity` | 0.08 MB | 0.35 MB | [Tutorial](../Demo-1-Rajoles.md) |
| [Demo-2-Roca](Demo-2-Roca.zip) | `DemoRoca.unity` | 0.23 MB | 0.69 MB | [Tutorial](../Demo-2-Roca.md) |
| [Demo-3-Lava](Demo-3-Lava.zip) | `DemoLava.unity` | 0.08 MB | 0.34 MB | [Tutorial](../Demo-3-Lava.md) |
| [Demo-4-Aigua](Demo-4-Aigua.zip) | `DemoAigua.unity` i `DemoAiguaGalaxy.unity` | 1.27 MB | 2.78 MB | [Tutorial](../Demo-4-Aigua.md) |
| [Demo-5-Holograma](Demo-5-Holograma.zip) | `DemoHolograma.unity` | 0.08 MB | 0.31 MB | [Tutorial](../Demo-5-Holograma.md) |
| [Demo-6-Forat](Demo-6-Forat.zip) | `DemoForat.unity` | 0.08 MB | 0.34 MB | [Tutorial](../Demo-6-Forat.md) |
| [Demo-7-Sombra](Demo-7-Sombra.zip) | `DemoSombra.unity` | 0.07 MB | 0.28 MB | [Tutorial](../Demo-7-Sombra.md) |
| [Demo-8-Pels](Demo-8-Pels.zip) | `DemoPels.unity` | 1.67 MB | 2.80 MB | [Tutorial](../Demo-8-Pels.md) |

La variant **DemoAiguaGalaxy** mostra reflexos retallats, un fons de pedra amb distorsió i càustiques, tint segons profunditat i escuma en contacte amb les roques. El controlador té **Time Scale = 2**; posa’l a 1 per reduir la velocitat. Les dues escenes comparteixen el mateix ZIP.

## Biblioteca de materials reutilitzables

[Projecte de catàleg](Biblioteca-Materials.zip) · [Paquet importable](DAM-Shaders.unitypackage) · [Guia d’ús](../Biblioteca-Materials.md)

14 famílies amb colors vius per a jocs infantils, incloses **fusta i plàstic**: 16 Shader Graphs, 36 materials i 84 textures i un prefab d’aigua Galaxy. El `.unitypackage` conté només la biblioteca i es pot importar en altres projectes URP; no inclou els controls del catàleg ni substitueix la configuració del projecte.

## Controls

| Demos | Controls |
|---|---|
| Voronoi animat (Demo 0) | Color i Velocitat X/Y al material MVoronoiAnimat; (0, 0) atura el patró |
| Rajoles, Roca, Lava, Aigua, Holograma | Lliscador del paràmetre, Espai pausa el temps, R reinicia, H amaga el panell |
| Forat | A/D mouen el personatge, Espai alterna automàtic/manual, F activa el retall, R reinicia, H amaga el panell; lliscador de radi; obertura i tancament progressius (0.35 s per defecte) |
| Sombra | A/D mouen el personatge, Espai alterna automàtic/manual, F activa la silueta, R reinicia, H amaga el panell; el gris omple només les parts ocultes |
| Pèls | Lliscador de llargada; botons de capes i passos POM; C activa la curvatura, R reinicia, H amaga el panell |

A la Demo 0, el shader és a **Assets/Shaders**, el material a **Assets/Materials** i l’escena a **Assets/Scenes**. A les altres demos, els shaders són a **Assets/Demos/<nom>/Shaders** i els materials a **Materials**. Obre el Shader Graph amb doble clic. Quan contingui grups numerats, selecciona un grup i prem F per enquadrar-lo; els tutorials també inclouen una captura del graf complet. Els scripts són a **Assets/Common**; Pèls inclou també **Assets/Demos/Pels/Scripts** i dues Custom Functions al fitxer **Shaders/Pels.hlsl**. Roca i Aigua inclouen també la utilitat **GenerarMalles** al menú **Demos**.

Les demos 1–8 inclouen **DemoOrbitCamera** a Main Camera: **botó dret + arrossegar** per orbitar al voltant del centre de la demo, **roda** per fer zoom i **Home** per recuperar la vista inicial. No actua sobre el panell de controls. El component permet ajustar centre, sensibilitat i límits de distància i inclinació. [Codi del controlador](DemoOrbitCamera.cs).

## Contingut dels ZIP

- **Assets/**, inclosos els fitxers **.meta** que mantenen les referències.
- **Packages/**, amb les versions dels paquets.
- **ProjectSettings/**, amb URP, entrada i l’escena de la demo configurats.
- **LLEGEIX-ME.md**, amb instruccions breus.

No s’inclouen **Library**, **Temp**, **Logs** ni les eines internes utilitzades per preparar les captures. No moguis només l’escena fora del projecte: depèn dels materials, els gràfics i la configuració d’URP.

## Si alguna cosa no funciona

- **Projecte no reconegut:** selecciona la carpeta amb Assets, Packages i ProjectSettings, no el ZIP ni la carpeta exterior.
- **Material rosa:** espera que acabi la compilació, comprova que el projecte utilitza URP i revisa els errors de la Console.
- **Escena buida:** utilitza **Demo > Obrir escena principal**.
- **Teclat sense resposta:** clica Game, comprova que Pause estigui desactivat i que Input System sigui el sistema d’entrada actiu.
- **Escuma incorrecta:** comprova Depth Texture a l’URP Asset actiu i a la càmera.
- **Forat inactiu:** comprova les referències del controlador i que els colliders de les parets siguin a la capa 8, DioramaWalls.
- **Silueta inactiva:** comprova Depth Texture, Depth Test = Always, Depth Write = Force Disabled i les referències Silhouettes del controlador de Sombra.
