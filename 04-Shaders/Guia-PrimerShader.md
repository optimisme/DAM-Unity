# Guia: primer shader de color

Crea un shader amb una propietat de color i aplica’l a un cub.

## Projecte

Fes un nou projecte tipus **"Universal 3D"** anomenat **ShadersTest** amb **Unity**. Obre **Assets/Scenes/SampleScene** i conserva **Main Camera**, **Directional Light** i **Global Volume**; no cal crear-los. Desa una còpia amb **File > Save As…**.

Als *Assets* crea una nova carpeta **"Shaders"**

## Primer Shader

A la carpeta *Assets > Shaders* crea un nou **"Unlit Shader"** i anomena'l **"SColor"**

*Create > Shader Graph > URP > Unlit Shader Graph*

<center>
<a href="./assets/primer-createunlitshader.png"><img src="./assets/primer-createunlitshader.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Amb el botó dret a la carpeta del Project, utilitza **Create > Rendering > Material** i anomena’l **MColor**. Arrossega **SColor** del Project sobre el material i comprova a l’**Inspector** que el shader queda assignat:

<center>
<a href="./assets/primer-materialshader.png"><img src="./assets/primer-materialshader.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Fes doble clic a sobre del *shader* per obrir la pestanya de definició del shader.

<center>
<a href="./assets/primer-shadertab.png"><img src="./assets/primer-shadertab.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Inicialment només es veuen les sortides del material:

- **Vertex**: controla els valors que afecten la geometria de l’objecte (per exemple, la posició dels vèrtexs o la seva deformació).

- **Fragment** controla propietats de la superfície com el color. Aquest gràfic és Unlit: el color no depèn de la il·luminació habitual de l’escena.

A l'esquerra tenim un espai on podem definir els paràmetres que l'usuari podrà configurar del nostre *shader*.

Prem el botó **+** i afegeix un paràmetre de tipus **Color** anomenat **"Base Color"**

<center>
<a href="./assets/primer-parameters.png"><img src="./assets/primer-parameters.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>

Arrossega el nou paràmetre **"Base Color"** cap a l'àrea del gràfic, i connecta la seva sortida amb l'entrada de **"Fragment > Base Color(3)"**

<center>
<video src="./assets/primer-dragparameter.mov" width="600" controls></video>
</center>

**Graf complet:** només cal aquest cable entre la propietat i **Fragment / Base Color**. Clica la captura per llegir-la a mida completa.

<a href="assets/primer-graf-complet.png"><img src="assets/primer-graf-complet.png" alt="Graf complet de SColor amb la propietat Base Color connectada al Fragment" width="1600" style="width: 100%; max-width: 1600px; height: auto;"></a>

Comprova que la propietat té **Exposed** activat per poder-la editar al material.

Això farà que el color escollit per l'usuari s'apliqui com a color de sortida del shader.

Desa el shader amb el botó del **disquet** per aplicar els canvis a l’escena.

<center>
<img src="./assets/savedisk.png" style="width: 90%; max-width: 100px">
</center>
<br/>

Crea un nou material i anomena'l "MColor", **arrossega el shader a sobre del material**.

Crea un nou objecte tipus *3D Object > Cube* a l'escena:

- Posa'l a la posició X:0, Y:0, Z:0
- Aplica-li el material *"MColor"*

El color inicial depèn del valor per defecte de la propietat; pots canviar-lo amb el selector de:

*Inspector > MColor (Material) > Surface Inputs > Base Color*

<center>
<a href="./assets/primer-colorinput.png"><img src="./assets/primer-colorinput.png" width="1200" style="width: 100%; max-width: 1200px; height: auto;"></a>
</center>
<br/>


## Comprovar el resultat

- Canvia el color al material i comprova que el cub el mostra.
- Crea un segon material amb el mateix shader: comprova que pot tenir un color diferent.
- Si no apareix la propietat, comprova Exposed i desa el gràfic.
- Si no canvia el cub, comprova quin material té assignat el seu Renderer.
