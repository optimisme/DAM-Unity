# Shaders

Els **shaders** permeten fer càlculs a la tarja gràfica, que seríen molt costosos de fer al processador.

Així es poden fer efectes especials, amb acceleració gràfica.

**Unity** a més, té una eina per definir *shaders* de manera gràfica, sense aprendre el llenguatge de programació de cada motor gràfic *(Vulkan, DirectX, Metal, ...)*

Els shaders d'aquest tutorial estàn a:

*Assets > DemoShaders.unitypackage*

*Cal tenir **ProBuilder** per veure els exemples.*

## Avantatges dels shaders vs textures

**Escalabilitat infinita**: Els patrons generats amb matemàtiques no tenen resolució fixa: es veuen sempre nítids, a qualsevol mida o distància.

**Zero memòria de textures**: No cal carregar imatges pesades a la GPU; el shader calcula els detalls a temps real.

**Variació il·limitada**: Un sol shader pot generar infinites versions (canviant paràmetres, soroll, colors…) sense necessitat de crear nous fitxers.

**Animacions naturals**: Valors com time, sin, noise permeten animar el resultat sense seqüències d’imatges.

**Reutilització i flexibilitat**: El mateix codi serveix per múltiples objectes i materials simplement canviant inputs (colors, velocitats, escales…).

**Més fàcil d’optimitzar**: Les textures grans consumeixen més VRAM i bandwidth; els shaders matemàtics gasten només càlcul, que la GPU fa molt eficientment.

**Resultats paramètrics i interactius**: Pots modificar l’aparença en temps real (joc, editor, slider), cosa impossible amb una textura estàtica.

## Projecte

Fes un nou projecte tipus **"Universal 3d"** anomenat **ShadersTest**

Als *Assets* crea una nova carpeta **"Shaders"**

## Primer Shader

A la carpeta *Assets > Shaders* crea un nou **"Unlit Shader"** i anomena'l **"SColor"**

*Create > Shader Graph > URP > Unlit Shader Graph*

<center>
<img src="./assets/primer-createunlitshader.png" style="width: 90%; max-width: 600px">
</center>
<br/>

Amb el *"botó dret"* a sobre de l'icona del shader, crea un nou material amb:

- *Create Material*

Anomena al nou material **"MColor"**, fixa't a l'**Inspector** del material, que automàticament té el shader **"SColor"** assignat:

<center>
<img src="./assets/primer-materialshader.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Fes doble click a sobre del *shader* per obrir la pestanya de definició del shader.

<center>
<img src="./assets/primer-shadertab.png" style="width: 90%; max-width: 600px">
</center>
<br/>

Inicialment només es veuen les sortides del material:

- **Vertex**: controla els valors que afecten la geometria de l’objecte (per exemple, la posició dels vèrtexs o la seva deformació).

- **Fragment** controla els valors que afecten el color, la llum i l’aparença visual de cada fragment (píxel) de la superfície.

A l'esquerra tenim un espai on podem definir els paràmetres que l'usuari podrà configurar del nostra *shader*.

Apreta el botó **+** i afegeix un paràmetre de tipus **Color** anomenat **"Base Color"**

<center>
<img src="./assets/primer-parameters.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Arrossega el nou paràmetre **"Base Color"** cap a l'area del gràfic, i connecta la seva sortida amb l'entrada de **"Fragment > Base Color(3)"**

<center>
<video src="./assets/primer-dragparameter.mov" width="600" controls></video>
</center>

Això farà, que el color escollit per l'usuari s'apliqui com a color de sortida del shader.

> **NOTA**: És **MOLT IMPORTANT!** guardar el shader apretant el **disquet** cada vegada que volguem aplicar els canvis a l'escena.

<center>
<img src="./assets/savedisk.png" style="width: 90%; max-width: 100px">
</center>
<br/>

Crea un nou objecte tipus *3D Object > Cube* a l'escena:

- Posa'l a la posició X:0, Y:0, Z:0
- Aplica-li el material *"MColor"*

Veuràs que inicialment el material és completament negre, però que pots canviar el color amb el selecctor de:

*Inspector > MColor (Material) > Surface Inputs > Base Color*

<center>
<img src="./assets/primer-colorinput.png" style="width: 90%; max-width: 600px">
</center>
<br/>

