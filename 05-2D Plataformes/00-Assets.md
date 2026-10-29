# Plataformes

En aquest tema construirem un joc de plataformes 2D. Treballarem amb **Unity 6.6**, el nou **Input System** i la UI de Canvas (uGUI).

## Ordre de treball

Aquest bloc construeix **un únic joc de manera progressiva**, dins de l'escena **Level01**. Segueix els capítols en ordre: primer les bases, després la partida completa i finalment les plataformes mòbils, el decorat i les ajudes de salt.

| Capítol | Tutorial | Resultat |
|---|---|---|
| 00 | [Assets](./00-Assets.md) | Projecte, sprites i escena |
| 01 | [Tilemap](./01-Tilemap.md) | Terreny i col·lisions |
| 02 | [Player](./02-Player.md) | Entrada, moviment i salt bàsic |
| 03 | [Animacions](./03-Animacions.md) | Quiet, caminar, saltar i caure |
| 04 | [Càmera](./04-Camera.md) | Seguiment dins dels límits |
| 05 | [Items](./05-Items.md) | Recollir monedes |
| 06 | [Trampes](./06-Trampes.md) | Vida i dany |
| 07 | [HUD](./07-HUD.md) | Mostrar les dades del jugador |
| 08 | [Game Over](./08-GameOver.md) | Morir i reiniciar |
| 09 | [Plataformes mòbils](./09-Plataformes.md) | Transportar el jugador |
| 10 | [Parallax](./10-Parallax.md) | Tilemaps de fons i primer pla |
| 11 | [Millores del salt](./11-MilloresSalt.md) | Coyote time, buffer i altura variable |
| 12 | [Final de partida](./12-FinalPartida.md) | Meta, victòria i reinici |
| 13 | [Resum de tècniques](./13-Tècniques.md) | Relació entre components |

El pack de decoració del capítol 10 es descarrega des del mateix tutorial.

Quan s'indica «substitueix l'script», reemplaça tot el contingut del fitxer; no afegeixis una segona classe ni dupliquis mètodes. Desa cada fitxer i espera que Unity acabi de compilar. Fes els canvis amb **Play aturat** i desa l'escena després de cada tutorial.


Les captures del nivell fan servir els sprites originals de **Simple 2D Platformer BE2**: el personatge, les rajoles de terra, les monedes i les trampes del mateix paquet que importarem.

## Nou projecte

A Unity Hub, crea un projecte nou amb la plantilla **Universal 2D** i anomena'l **Platform**. La plantilla ja inclou els paquets que farem servir: 2D Sprite, 2D Tilemap Editor, 2D Tilemap Extras, Input System i uGUI.

## Carpetes

A la finestra **Project**, dins d'**Assets**, crea aquestes carpetes amb clic dret > **Create > Folder**. La plantilla ja porta **Scenes**.

- Animations
- Sprites
- Prefabs
- Scripts
- Scenes
- Tiles

## Decorat de profunditat

Al [capítol 10](./10-Parallax.md) importarem **ParallaxTiles.zip**, amb els fons i l'eina per convertir-los en Tilemaps. [BackgroundImages.zip](./BackgroundImages.zip) conté només les imatges, per si les vols consultar; no cal importar-lo.

## Unity Asset Store

Farem servir un asset gratuït de la Unity Asset Store: [Simple 2D Platformer Assets Pack](https://assetstore.unity.com/packages/2d/characters/simple-2d-platformer-assets-pack-188518).

Obre la pàgina de l'asset i prem **Add to My Assets**.

<center>
<img src="./assets/assetstore-add.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Els assets s'afegeixen al teu compte de Unity. Prem **Open in Unity** per veure'ls des de l'editor.

<center>
<img src="./assets/assetstore-open.png" style="width: 90%; max-width: 400px">
</center>
<br/>

S'obre el **Package Manager** de Unity, que gestiona els paquets i els assets del teu compte. Selecciona l'asset i prem **Download**.

<center>
<img src="./assets/assetstore-download.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Un cop descarregat, prem **Import** per afegir-lo al projecte.

<center>
<img src="./assets/assetstore-import.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Si Unity proposa canvis de paquets, comprova què actualitzarà. Aquest tutorial fa servir les versions compatibles amb Unity 6.6; no cal actualitzar tots els paquets per importar sprites.

<center>
<img src="./assets/assetstore-upgrade.png" style="width: 90%; max-width: 400px">
</center>

A la finestra **Import Unity Package** pots escollir què importes. En aquest cas, deixa-ho tot marcat i prem **Import**.

<center>
<img src="./assets/assetstore-importall.png" style="width: 90%; max-width: 400px">
</center>

Un cop importat, apareix la carpeta **Assets/Simple 2D Platformer BE2**.

<center>
<img src="./assets/assetstore-folder.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Ara ja hi ha els assets dins el projecte i els podem utilitzar.

## Preparar l'escena i els sprites

1. La plantilla obre l'escena **SampleScene**. A Project, ves a **Assets/Scenes**, fes clic dret a SampleScene > **Rename** i anomena-la **Level01**.
2. Selecciona **Main Camera**. Comprova **Projection = Orthographic**, **Size = 5**, **Position = (0, 0, -10)** i **Rotation = (0, 0, 0)**. Orthographic fa que la mida dels sprites no depengui de la distància a la càmera.
3. Si utilitzes materials **Sprite-Lit-Default** de Universal 2D, conserva la **Global Light 2D**. Sense llum, aquests sprites poden quedar foscos. Els materials Sprite-Unlit no necessiten aquesta llum.
Els fulls de sprites del paquet (Player, Platforms, Coins, Objects i Enemy) ja vénen preparats per a pixel art:

| Propietat | Valor | Per què |
|---|---|---|
| Texture Type | Sprite (2D and UI) | La imatge es pot dibuixar com a sprite |
| Sprite Mode | Multiple | Cada full conté diversos sprites retallats |
| Pixels Per Unit | 16 | 16 píxels ocupen una unitat del món |
| Filter Mode | Point (no filter) | Els píxels es veuen nítids |
| Compression | None | No apareixen colors estranys |

No els canviïs. Al capítol 01 comprovarem el full Platforms.

## Comprovar el resultat

El projecte té una escena desada i els sprites importats.

1. Obre **Console** i comprova que no hi ha errors vermells.
2. A **Assets/Simple 2D Platformer BE2/Sprites**, prem la fletxa de **Platforms** i comprova que conté sprites com Top Left o Single.

## Errors habituals

- Si no apareixen sprites individuals, revisa **Sprite Mode = Multiple**.
- Si veus la plantilla 3D, comprova que el projecte sigui **Universal 2D**.
