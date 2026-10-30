# Guia: primer shader de color

Crea un shader amb una propietat de color i aplica’l a un cub.

## Projecte

1. Crea un projecte nou amb la plantilla **Universal 3D** i anomena'l **ShadersTest**.
2. A la finestra **Project**, obre **Assets/Scenes/SampleScene**. Conserva **Main Camera**, **Directional Light** i **Global Volume**; no cal crear-los.
3. A la finestra **Project**, selecciona la carpeta **Assets** i crea una carpeta nova amb **Assets > Create > Folder**. Anomena-la **Shaders**.

## Primer shader

1. Selecciona la carpeta **Assets/Shaders** a la finestra **Project**.
2. Crea un **Unlit Shader Graph** amb **Assets > Create > Shader Graph > URP > Unlit Shader Graph** (també el trobes amb el botó dret a la carpeta, **Create > Shader Graph > URP > Unlit Shader Graph**).
3. Anomena'l **SColor**.

<center>
<a href="./assets/primer-createunlitshader.png"><img src="./assets/primer-createunlitshader.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

## Material

1. Amb la carpeta **Assets/Shaders** seleccionada, crea un material amb **Assets > Create > Material**.
2. Anomena'l **MColor**.
3. Arrossega **SColor** des de la finestra **Project** a sobre del material **MColor**.
4. Selecciona **MColor** i comprova a l’**Inspector** que el camp **Shader** mostra **Shader Graphs/SColor**:

<center>
<a href="./assets/primer-materialshader.png"><img src="./assets/primer-materialshader.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

## Editar el gràfic

Fes doble clic a **SColor** per obrir-lo a la finestra de **Shader Graph**.

<center>
<a href="./assets/primer-shadertab.png"><img src="./assets/primer-shadertab.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Al centre hi ha el **Master Stack**, amb els dos blocs de sortida del material:

- **Vertex**: controla els valors que afecten la geometria de l’objecte (per exemple, la posició dels vèrtexs o la seva deformació).

- **Fragment**: controla propietats de la superfície com el color. Aquest gràfic és Unlit: el color no depèn de la il·luminació habitual de l’escena.

A l'esquerra hi ha el **Blackboard**, on es defineixen les propietats que es podran configurar des del material. A la dreta hi ha el **Graph Inspector** i el **Main Preview**.

1. Al **Blackboard**, prem el botó **+** i tria **Color**.
2. Anomena la propietat **Base Color**.

<center>
<a href="./assets/primer-parameters.png"><img src="./assets/primer-parameters.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

3. Arrossega la propietat **Base Color** del **Blackboard** cap a l'àrea del gràfic.
4. Connecta la sortida del node **Base Color** amb l'entrada **Base Color(3)** del bloc **Fragment**.

<center>
<video src="./assets/primer-dragparameter.mov" width="600" controls></video>
</center>

**Graf complet:** només cal aquest cable entre la propietat i **Fragment / Base Color**. Clica la captura per llegir-la a mida completa.

<a href="assets/primer-graf-complet.png"><img src="assets/primer-graf-complet.png" alt="Graf complet de SColor amb la propietat Base Color connectada al Fragment" width="1600" style="width: 100%; max-width: 1600px; height: auto;"></a>

5. Selecciona la propietat **Base Color** al **Blackboard** i comprova a **Graph Inspector > Node Settings** que **Show In Inspector** està activat. Així la podràs editar des del material.

Això farà que el color escollit al material s'apliqui com a color de sortida del shader.

6. Desa el gràfic amb el botó **Save Asset** (el **disquet** de la part superior esquerra de la finestra de Shader Graph) per aplicar els canvis.

<center>
<img src="./assets/savedisk.png" style="width: 90%; max-width: 100px">
</center>
<br/>

## Aplicar-lo a un cub

1. Crea un cub amb el botó **+** de la finestra **Hierarchy**: **3D Object > Cube**.
2. Arrossega el material **MColor** des de la finestra **Project** a sobre del cub (a l'escena o a la **Hierarchy**).

El color inicial depèn del valor per defecte de la propietat; pots canviar-lo amb el selector de:

*Inspector > MColor (Material) > Surface Inputs > Base Color*

<center>
<a href="./assets/primer-colorinput.png"><img src="./assets/primer-colorinput.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Desa l'escena amb **File > Save**.

## Comprovar el resultat

- Canvia el color al material i comprova que el cub el mostra.
- Crea un segon material amb el mateix shader: comprova que pot tenir un color diferent.
- Si no apareix la propietat, comprova **Show In Inspector** i desa el gràfic.
- Si no canvia el cub, comprova quin material té assignat el seu **Mesh Renderer**.
