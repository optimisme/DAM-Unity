# Projectes de les demos de shaders

Cada ZIP conté un **projecte Unity independent** amb escenes acabades, Shader Graphs editables, materials i controls per experimentar. No cal importar cap demo anterior ni baixar textures de tercers: els recursos necessaris són dins dels projectes. Aigua inclou quatre textures pròpies.

## Requisits

- **Unity 6000.6.3f1**, la versió amb què s’han construït i provat els projectes.
- **Universal RP 17.6.0**, **Shader Graph 17.6.0** i **Input System 1.20.0**. Les dependències es resolen amb Package Manager.
- Internet durant la primera obertura si els paquets no són a la memòria cau.

## Obrir una demo

1. Descarrega el ZIP i descomprimeix-lo completament.
2. A Unity Hub, utilitza **Add > Add project from disk**.
3. Selecciona la carpeta que conté directament **Assets**, **Packages** i **ProjectSettings**.
4. Obre-la amb **6000.6.3f1** i espera que acabi la importació.
5. En acabar la primera importació, s’obre automàticament l’escena principal de la demo. Els materials poden trigar una estona a carregar-se. Aigua també inclou una segona escena, `DemoAiguaGalaxy`, que pots obrir des de `Assets/Demos/Aigua/Scenes/`.
6. Prem **Play** i clica la vista **Game**. El panell mostra els controls.
7. Atura Play abans d’editar valors que vulguis conservar als materials.

El menú **Demo > Obrir escena principal** permet tornar a la demo. L’obertura automàtica només actua el primer cop; després es respecten les escenes amb què estiguis treballant.

## Descàrregues

Les mides són MB decimals aproximats. La mida descomprimida no inclou la memòria cau que Unity generarà.

| Projecte | Escena | ZIP | Descomprimit | Tutorial |
|---|---|---:|---:|---|
| [Demo-0-Patro](Demo-0-Patro.zip) | `DemoPatro.unity` | 0.08 MB | 0.34 MB | [Tutorial](../Demo-0-Patro.md) |
| [Demo-1-Rajoles](Demo-1-Rajoles.zip) | `DemoRajoles.unity` | 0.08 MB | 0.35 MB | [Tutorial](../Demo-1-Rajoles.md) |
| [Demo-2-Roca](Demo-2-Roca.zip) | `DemoRoca.unity` | 0.23 MB | 0.69 MB | [Tutorial](../Demo-2-Roca.md) |
| [Demo-3-Lava](Demo-3-Lava.zip) | `DemoLava.unity` | 0.08 MB | 0.34 MB | [Tutorial](../Demo-3-Lava.md) |
| [Demo-4-Aigua](Demo-4-Aigua.zip) | `DemoAigua.unity` i `DemoAiguaGalaxy.unity` | 1.27 MB | 2.78 MB | [Tutorial](../Demo-4-Aigua.md) |
| [Demo-5-Holograma](Demo-5-Holograma.zip) | `DemoHolograma.unity` | 0.08 MB | 0.31 MB | [Tutorial](../Demo-5-Holograma.md) |
| [Demo-6-Forat](Demo-6-Forat.zip) | `DemoForat.unity` | 0.08 MB | 0.34 MB | [Tutorial](../Demo-6-Forat.md) |

| [Demo-7-Sombra](Demo-7-Sombra.zip) | `DemoSombra.unity` | 0.07 MB | 0.28 MB | [Tutorial](../Demo-7-Sombra.md) |

La variant **DemoAiguaGalaxy** mostra reflexos retallats, un fons de pedra amb distorsió i càustiques, tint segons profunditat i escuma en contacte amb les roques. El controlador té **Time Scale = 2**; posa’l a 1 per reduir la velocitat. Les dues escenes comparteixen el mateix ZIP.

## Biblioteca de materials reutilitzables

[Projecte de catàleg](Biblioteca-Materials.zip) · [Paquet importable](DAM-Shaders.unitypackage) · [Guia d’ús](../Biblioteca-Materials.md)

14 famílies amb colors vius per a jocs infantils, incloses **fusta i plàstic**: 16 Shader Graphs, 36 materials i 84 textures i un prefab d’aigua Galaxy. El `.unitypackage` conté només la biblioteca i es pot importar en altres projectes URP; no inclou els controls del catàleg ni substitueix la configuració del projecte.

## Controls

| Demos | Controls |
|---|---|
| Patró, Rajoles, Roca, Lava, Aigua, Holograma | Lliscador del paràmetre, Espai pausa el temps, R reinicia, H amaga el panell |
| Forat | A/D mouen el personatge, Espai alterna automàtic/manual, F activa el retall, R reinicia, H amaga el panell; lliscador de radi; obertura i tancament progressius (0.35 s per defecte) |

| Sombra | A/D mouen el personatge, Espai alterna automàtic/manual, F activa la silueta, R reinicia, H amaga el panell; el gris omple només les parts ocultes |

Els shaders són a **Assets/Demos/<nom>/Shaders** i els materials a **Materials**. Obre el Shader Graph amb doble clic i selecciona un grup numerat i prem F per comparar-lo amb la captura del pas corresponent. Els scripts són a **Assets/Common**. Roca i Aigua inclouen també la utilitat **GenerarMalles** al menú **Demos**.

Totes les escenes inclouen **DemoOrbitCamera** a Main Camera: **botó dret + arrossegar** per orbitar al voltant del centre de la demo, **roda** per fer zoom i **Home** per recuperar la vista inicial. No actua sobre el panell de controls. El component permet ajustar centre, sensibilitat i límits de distància i inclinació. [Codi del controlador](DemoOrbitCamera.cs).

## Contingut dels ZIP

- **Assets/**, inclosos els fitxers **.meta** que mantenen les referències.
- **Packages/**, amb les versions dels paquets.
- **ProjectSettings/**, amb URP, entrada i l’escena de la demo configurats.
- **LLEGEIX-ME.md**, amb instruccions breus.

No s’inclouen **Library**, **Temp**, **Logs** ni les eines internes utilitzades per preparar les captures. No moguis només l’escena fora del projecte: depèn dels materials, els gràfics i la configuració d’URP.

## Comprovació dels projectes

Revisió del **9 d’octubre de 2026**, sobre els fitxers de la branca **main**:

- Els set tutorials 0–6 parteixen de **Universal 3D / SampleScene**, conservant Main Camera, Directional Light i Global Volume. Les escenes s’han reconstruït amb aquests objectes originals, els scripts publicats i les utilitats de malles dels tutorials.
- **551 comprovacions** de les escenes: posicions i escales contrastades amb les taules dels Markdown, variants dels materials, controls, referències, càmera, malles, profunditat i configuració de Galaxy.
- **306 connexions, 69 entrades literals i 56 propietats** dels deu Shader Graphs contrastades amb les taules dels tutorials.
- Els **vuit ZIP de les demos 0–6 i del catàleg** s’han extret en carpetes noves i s’han obert amb **Unity 6000.6.3f1 en mode gràfic a macOS**. Les nou escenes han funcionat en Play: **589 comprovacions** d’obertura automàtica, compilació, renderitzat, paràmetres, animacions, càmera orbital, obertura i tancament progressius de Forat i aïllament de les 15 famílies del catàleg.
- **Demo-7-Sombra** també parteix de Universal 3D i s’ha obert des del seu ZIP en una carpeta nova: silueta emplenada en gris, paret intacta, oclusió parcial, activació/desactivació i càmera orbital. La prova completa ha passat **111 comprovacions**, incloses 30 de posició, rotació i escala contra les taules del tutorial. Aquestes 30 comprovacions s’han afegit en una segona obertura reutilitzant la importació inicial.
- Corregida una referència buida al perfil **Bloom** dels projectes de demo: el resplendor es conserva en tornar-los a obrir.
- Els **137 recursos** de la biblioteca del ZIP coincideixen amb el `.unitypackage` importable.

La reconstrucció i les proves són automatitzades; no és una repetició manual de cada clic de l’alumne. S’han reutilitzat els Shader Graphs després de contrastar-ne totes les connexions i entrades. S’han inspeccionat les captures de les deu escenes. El catàleg va superar el límit de temps de la primera prova mentre compilava totes les famílies; la comprovació completa s’ha acabat amb més marge reutilitzant aquella importació. No s’ha provat en Windows o Linux. Una excepció del cercador intern de Unity (`UnityEditor.Search.SearchDatabase`) s’ha distingit dels errors de les demos.

## Si alguna cosa no funciona

- **Projecte no reconegut:** selecciona la carpeta amb Assets, Packages i ProjectSettings, no el ZIP ni la carpeta exterior.
- **Material rosa:** espera que acabi la compilació i comprova la versió d’URP i de Unity.
- **Escena buida:** utilitza **Demo > Obrir escena principal**.
- **Teclat sense resposta:** clica Game, comprova que Pause estigui desactivat i que Input System sigui el sistema d’entrada actiu.
- **Escuma incorrecta:** comprova Depth Texture a l’URP Asset actiu i a la càmera.
- **Forat inactiu:** comprova les referències del controlador i que els colliders de les parets siguin a la capa 8, DioramaWalls.

- **Silueta inactiva:** comprova Depth Texture, Depth Test = Always, Depth Write = Force Disabled i les referències Silhouettes del controlador de Sombra.
