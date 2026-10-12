# Construir el nivell amb Tilemaps

**Punt de partida:** projecte i escena Nivell01 de [Assets](./00-Assets.md). Pintarem el terra on després caminarà el jugador.

## Què és cada cosa?

| Concepte | Significat |
|---|---|
| Sprite | Una imatge que Unity pot dibuixar |
| Tileset | Conjunt d'imatges per construir un nivell; sovint estan en un mateix full |
| Tile | Recurs que associa un sprite amb propietats d'una rajola, com la col·lisió |
| Tile Palette | Paleta des d'on escollim les rajoles que pintarem |
| Grid | Graella que defineix la mida i la disposició de les cel·les |
| Tilemap | Mapa que guarda quina rajola hi ha a cada cel·la |

## Revisar el tileset

1. A Project, localitza **Simple 2D Platformer BE2 > Sprites > Platforms**.
2. Desplega'l. Si ja conté sprites com **Single Left**, **Single** i **Single Right**, conserva aquest retall.
3. Si utilitzes un full sense retallar, selecciona'l, posa **Texture Type = Sprite (2D and UI)** i **Sprite Mode = Multiple**. Prem Apply i obre **Sprite Editor**.
4. A **Slice > Grid by Cell Size**, escriu la mida real d'una rajola en píxels. Per exemple, només si les rajoles són de 16 × 16, posa `16 / 16`. Fes **Slice** i **Apply**. No tornis a retallar un paquet que ja té noms i animacions configurats.
5. En aquest paquet, les rajoles de Platforms són de **16 × 16 píxels**: posa **Pixels Per Unit = 16**. Per conservar els píxels nítids, posa **Filter Mode = Point (no filter)** i **Compression = None**. Comprova que una rajola ocupa una unitat: el seu costat en píxels ha de coincidir amb **Pixels Per Unit**. Deixa la graella a **Cell Size = (1, 1, 0)** i els objectes amb Scale `(1, 1, 1)`.

## Evitar línies entre les imatges de les rajoles

Encara que les cel·les encaixin, poden aparèixer ratlles molt fines quan es mou la càmera. Unity pot arribar a llegir el marge transparent que hi ha al costat d'un sprite dins del full. És una unió **visual**: el Composite Collider no la corregeix, perquè només controla les col·lisions.

Un **Sprite Atlas** és un recurs que Unity prepara agrupant sprites i reservant un marge entre ells. Les rajoles continuen fent servir els mateixos sprites.

1. A Project, fes **Assets > Create > 2D > Sprite Atlas** i anomena'l **Terreny**. Desa'l a Assets/Tiles.
2. A l'Inspector de l'atles, desactiva **Allow Rotation** i **Tight Packing**. Posa **Padding = 4**.
3. Posa **Filter Mode = Point (no filter)**, desactiva **Generate Mip Maps** i posa **Compression = None** a la configuració de textura de l'atles. Deixa **Include in Build** activat.
4. A **Objects for Packing**, afegeix el full **Platforms.png** del paquet original. No cal arrossegar-hi cada rajola per separat.
5. Prem **Pack Preview**. Quan entrem a Play, Unity utilitzarà els sprites de l'atles automàticament; no cal tornar a pintar el Tilemap.
6. Als capítols de jugador i càmera, comprova les unions mentre camines. Mantén **Cell Gap = (0, 0, 0)** i **Scale = (1, 1, 1)**: no facis les rajoles més grosses ni les superposis per tapar les ratlles.

Aquesta configuració ha eliminat les línies del terreny en el projecte de comprovació. [Referència de les propietats de Sprite Atlas](https://docs.unity.com/en-us/engine/6000.3/manual/unity2d/sprite/atlas/sprite-atlas-reference).

## Crear la paleta

1. Obre **Window > 2D > Tile Palette**. Si el menú no existeix, revisa a **Window > Package Management > Package Manager** que hi hagi **2D Tilemap Editor** i **2D Sprite**.
2. Prem **Create New Palette**, posa **Terreny**, escull **Grid = Rectangle** i desa la paleta dins d'Assets/Tiles.
3. Arrossega els sprites retallats de Platforms a la paleta. Quan Unity demani on crear els recursos Tile, escull **Assets/Tiles/Terreny**; crea la carpeta si cal.
4. Selecciona una rajola a la paleta. La paleta és una caixa de peces; encara no és el nivell.

<center>
<img src="./assets/tilemap-tilepalette-window.png" alt="Finestra Tile Palette" style="width: 90%; max-width: 450px">
</center>
<br/>

## Crear i pintar el mapa

1. Fes **GameObject > 2D Object > Tilemap > Rectangular**. Unity crea **Grid** i un fill **Tilemap**.
2. Anomena el fill **Ground**. Selecciona Grid i comprova **Cell Size = (1, 1, 0)**.
3. A Tile Palette, selecciona **Ground** com a **Active Tilemap**. Activa la vista **2D** a Scene.
4. Escull el **pinzell** i pinta una fila de terra d'unes vint cel·les. Per donar-li gruix, utilitza **Top Left / Top / Top Right** a la superfície, **Middle Left / Middle / Middle Right** a l'interior i **Bottom Left / Bottom / Bottom Right** a la base. Per a plataformes d'una sola fila, utilitza **Single Left / Single / Single Right**. Així conserves l'herba a sobre i acabes les vores amb les peces del paquet.
5. Escull la **goma** per esborrar una cel·la i torna-la a pintar. L'eina d'**omplir** serveix per omplir una regió; prova-la primer dins d'un contorn petit i tancat.
6. Selecciona Main Camera i ajusta la seva posició perquè a Game es vegi el terra. Encara no hi ha jugador.

## Un nivell de comprovació reproduïble

Per comprovar el mateix recorregut que les captures, pinta aquests rectangles a Ground. X i Y són els **números de les cel·les**; els dos extrems de cada interval s'inclouen. Una cel·la amb Y = -1 té la superfície superior a Y = 0. Deixa Grid i Ground a Position zero, Rotation zero i Scale u.

| Tram | Columnes X | Files Y | Superfície superior |
|---|---|---|---|
| Terra esquerre | -10 a 5 | -5 a -3 | -2 |
| Terra central | 8 a 17 | -5 a -3 | -2 |
| Terra dret | 20 a 29 | -5 a -3 | -2 |
| Primera plataforma | -5 a -2 | -1 | 0 |
| Segona plataforma | 1 a 4 | 1 | 2 |
| Tercera plataforma | 10 a 13 | -1 | 0 |

Els forats són les columnes 6–7 i 18–19. Les plataformes elevades són sòlides: cal entrar-hi **pel costat mentre puges**, no travessar-les des de sota. No afegeixis Platform Effector 2D en aquest exemple.

Al capítol següent comprovarem les pujades de 2 unitats i els forats de 2 cel·les abans de posar-hi monedes o trampes. Després podràs ampliar el nivell, comprovant cada salt nou amb el personatge real.

## Unir les col·lisions del terra

1. Selecciona **Ground**, no Grid, i afegeix **Tilemap Collider 2D**.
2. Afegeix **Rigidbody 2D** i posa **Body Type = Static**: el terreny no es mourà.
3. Afegeix **Composite Collider 2D** al mateix Ground.
4. Al **Tilemap Collider 2D**, posa **Composite Operation = Merge**. En versions antigues l'opció es deia Used by Composite.
5. Al **Composite Collider 2D**, deixa **Is Trigger desactivat** i escull **Geometry Type = Polygons** per a terra sòlid.
6. Selecciona un recurs **Tile** d'Assets/Tiles/Terreny. **Collider Type = Grid** omple la cel·la; **Sprite** utilitza la forma física del sprite i **None** no genera col·lisió. Per al terra rectangular comença amb **Grid**.
7. A Scene, activa **Gizmos** i selecciona Ground: comprova que el contorn del terra queda unit, sense vores interiors entre les rajoles adjacents.

<center>
<img src="./assets/revisio-tilemap-collider.png" alt="Tilemap Collider 2D amb Merge, Rigidbody2D Static i Composite Collider 2D" style="width: 90%; max-width: 700px">
</center>
<br/>

El Tilemap Collider genera les peces i el Composite les uneix. Això redueix les juntes on el jugador pot enganxar-se i simplifica les formes de col·lisió. No s'aconsegueix només arrodonint el collider del jugador.

[Referència de Tilemap Collider 2D](https://docs.unity3d.com/6000.0/Documentation/Manual/tilemaps/work-with-tilemaps/tilemap-collider-2d-reference.html).

## Separar terreny i decoració

1. Crea un altre Tilemap rectangular i anomena'l **Decoracio**. Si Unity crea un altre Grid, pots moure Decoracio sota el Grid existent i deixar el seu Transform a posició zero, rotació zero i escala u.
2. Deixa Decoracio **sense colliders**. Pinta-hi elements que no han de bloquejar el jugador.
3. A **Tilemap Renderer**, posa Ground amb **Order in Layer = 0** i Decoracio amb **Order in Layer = -10**, dins de la mateixa Sorting Layer.
4. Abans de pintar, comprova sempre **Active Tilemap**. El jugador farà servir Order `10` i les monedes `20`.

Una **Sorting Layer** controla el dibuix. La **Layer** de la part superior de l'Inspector controla, entre altres coses, consultes i col·lisions. No són la mateixa propietat.

## Ampliació: Rule Tiles

Quan ja sàpigues pintar, pots automatitzar l'elecció de cantonades i vores amb **Rule Tiles**. Necessites el paquet oficial **2D Tilemap Extras** compatible amb la teva versió. Un Rule Tile escull el sprite segons les rajoles veïnes; no crea per si sol el disseny del nivell. Aquesta ampliació és opcional i no la necessitem als següents passos.

## Comprovar el resultat

A Scene es veu un nivell pintat, amb un contorn de col·lisió continu només al terra.

1. Esborra una rajola de Ground i comprova que canvia el contorn.
2. Pinta a Decoracio i comprova que no apareix col·lisió.
3. Desa Nivell01. Al següent apartat provarem el terra amb un jugador.

## Errors habituals

- Si les rajoles no encaixen, revisa PPU, retall, Cell Size i Scale.
- Si tot el terreny cau, posa el seu Rigidbody2D a Static.
- Si no hi ha contorn, revisa Collider Type del Tile, Merge i Gizmos.
