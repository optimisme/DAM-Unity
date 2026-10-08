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
5. Obre l’escena indicada. Totes són a `Assets/Demos/<nom>/Scenes/`.
6. Prem **Play** i clica la vista **Game**. El panell mostra els controls.
7. Atura Play abans d’editar valors que vulguis conservar als materials.

## Descàrregues

Les mides són MB decimals aproximats. La mida descomprimida no inclou la memòria cau que Unity generarà.

| Projecte | Escena | ZIP | Descomprimit | Tutorial |
|---|---|---:|---:|---|
| [Demo-0-Patro](Demo-0-Patro.zip) | `DemoPatro.unity` | 0.07 MB | 0.32 MB | [Tutorial](../Demo-0-Patro.md) |
| [Demo-1-Rajoles](Demo-1-Rajoles.zip) | `DemoRajoles.unity` | 0.08 MB | 0.33 MB | [Tutorial](../Demo-1-Rajoles.md) |
| [Demo-2-Roca](Demo-2-Roca.zip) | `DemoRoca.unity` | 0.16 MB | 0.53 MB | [Tutorial](../Demo-2-Roca.md) |
| [Demo-3-Lava](Demo-3-Lava.zip) | `DemoLava.unity` | 0.07 MB | 0.32 MB | [Tutorial](../Demo-3-Lava.md) |
| [Demo-4-Aigua](Demo-4-Aigua.zip) | `DemoAigua.unity` i `DemoAiguaGalaxy.unity` | 1.36 MB | 3.54 MB | [Tutorial](../Demo-4-Aigua.md) |
| [Demo-5-Holograma](Demo-5-Holograma.zip) | `DemoHolograma.unity` | 0.07 MB | 0.29 MB | [Tutorial](../Demo-5-Holograma.md) |
| [Demo-6-Forat](Demo-6-Forat.zip) | `DemoForat.unity` | 0.08 MB | 0.32 MB | [Tutorial](../Demo-6-Forat.md) |

La variant **DemoAiguaGalaxy** mostra reflexos retallats, un fons de pedra amb distorsió i càustiques, tint segons profunditat i escuma en contacte amb les roques. El controlador té **Time Scale = 2**; posa’l a 1 per reduir la velocitat. Les dues escenes comparteixen el mateix ZIP.

## Biblioteca de materials reutilitzables

[Projecte de catàleg](Biblioteca-Materials.zip) · [Paquet importable](DAM-Shaders.unitypackage) · [Guia d’ús](../Biblioteca-Materials.md)

14 famílies amb colors vius per a jocs infantils, incloses **fusta i plàstic**: 16 Shader Graphs, 36 materials i 84 textures i un prefab d’aigua Galaxy. El `.unitypackage` conté només la biblioteca i es pot importar en altres projectes URP; no inclou els controls del catàleg ni substitueix la configuració del projecte.

## Controls

| Demos | Controls |
|---|---|
| Patró, Rajoles, Roca, Lava, Aigua, Holograma | Lliscador del paràmetre, Espai pausa el temps, R reinicia, H amaga el panell |
| Forat | A/D mouen el personatge, Espai alterna automàtic/manual, F activa el retall, R reinicia, H amaga el panell; lliscador de radi |

Els shaders són a **Assets/Demos/<nom>/Shaders** i els materials a **Materials**. Obre el Shader Graph amb doble clic i selecciona un grup numerat i prem F per comparar-lo amb la captura del pas corresponent. Els scripts són a **Assets/Common**. Roca i Aigua inclouen també la utilitat **GenerarMalles** al menú **Demos**.

Totes les escenes inclouen **DemoOrbitCamera** a Main Camera: **botó dret + arrossegar** per orbitar al voltant del centre de la demo, **roda** per fer zoom i **Home** per recuperar la vista inicial. No actua sobre el panell de controls. El component permet ajustar centre, sensibilitat i límits de distància i inclinació. [Codi del controlador](DemoOrbitCamera.cs).

## Contingut dels ZIP

- **Assets/**, inclosos els fitxers **.meta** que mantenen les referències.
- **Packages/**, amb les versions dels paquets.
- **ProjectSettings/**, amb URP, entrada i l’escena de la demo configurats.
- **LLEGEIX-ME.md**, amb instruccions breus.

No s’inclouen **Library**, **Temp**, **Logs** ni les eines internes utilitzades per preparar les captures. No moguis només l’escena fora del projecte: depèn dels materials, els gràfics i la configuració d’URP.

## Comprovació dels projectes

Els set ZIP s’han descomprimit en carpetes independents i s’han obert a **Unity 6000.6.3f1 a macOS**, amb els paquets disponibles a la memòria cau. S’han comprovat importació, compilació dels shaders i scripts, referències dels components, sis segons en Play i renderització de la càmera URP. No s’han detectat components perduts ni errors dels scripts de les demos. No és una validació en Windows o Linux.

En les escenes de preparació també s’ha verificat que els paràmetres modifiquen el resultat renderitzat i que Lava, Aigua i Holograma canvien amb el temps. A Forat s’ha comprovat l’activació de la paret del davant, la conservació de la paret posterior amb material compartit, la desactivació manual, la sortida lateral del personatge, el cas del personatge darrere de la càmera i la neteja de propietats en desactivar el component.

Les captures de resultat són renderitzacions de les càmeres durant Play a 1440 × 900, amb els controls ocults. Els tutorials afegeixen 50 captures reals de l’editor Shader Graph, dividides en passos numerats. Les 207 connexions dels set gràfics originals es conserven; se n’ha organitzat la disposició visual. Aigua incorpora dos gràfics addicionals per al fons i la superfície de la variant Galaxy. Les seves 89 connexions també s’han contrastat amb les taules del tutorial. Les dues escenes d’aigua s’han provat des del ZIP; s’ha comprovat també que Time Scale = 2 duplica l’avanç del temps en Play.

Unity ha mostrat en execucions automàtiques una excepció del seu cercador intern (`UnityEditor.Search.SearchDatabase`). S’ha distingit dels errors dels scripts de les demos i no ha impedit les proves. 

La càmera orbital s’ha provat en les **vuit escenes**: rotació mantenint el centre i la distància, zoom, límits, retorn a la vista inicial i renderització. A Forat també s’ha comprovat l’oclusió des del davant i des del darrere.

## Si alguna cosa no funciona

- **Projecte no reconegut:** selecciona la carpeta amb Assets, Packages i ProjectSettings, no el ZIP ni la carpeta exterior.
- **Material rosa:** espera que acabi la compilació i comprova la versió d’URP i de Unity.
- **Escena buida:** obre l’escena indicada a la taula.
- **Teclat sense resposta:** clica Game, comprova que Pause estigui desactivat i que Input System sigui el sistema d’entrada actiu.
- **Escuma incorrecta:** comprova Depth Texture a l’URP Asset actiu i a la càmera.
- **Forat inactiu:** comprova les referències del controlador i que els colliders de les parets siguin a la capa 8, DioramaWalls.
