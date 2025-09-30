# ProBuilder

L'eina **ProBuilder** permet dissenyar objectes.

Crea un nou projecte tipus **"Universal 3D"** i anomena'l **"Objectes"**

## Instal·lació

- Des del **"Package Manager"**, afegeix el **"ProBuilder"** al projecte.

*Menú Window > Package Management > Package Manager*

- Escollir la opció **"Unity Registry"** i buscar **"ProBuilder"**

- Fer **Install**

<center>
<img src="./assets/probuilder-install.png" style="width: 90%; max-width: 600px">
</center>
<br/>

## Objectes "ProBuilder"

L'eina **"ProBuilder"** permet afegir objectes amb capacitat de ser manipulats fàcilment.

Per poder crear objectes manipulables, s'han de crear com a **"ProBuilder"**

- Al **"Hierarchy"** afegeix un nou **"Cube"** tipus **"ProBuilder"**

<center>
<img src="./assets/probuilder-addcube.png" style="width: 90%; max-width: 300px">
</center>
<br/>

Assegura't que tens aquestes dues icones de la vista actives:

<center>
<img src="./assets/probuilder-viewicons.png" style="width: 90%; max-width: 300px">
</center>
<br/>

- La primera és per veure les eines generals
- La segona és per veure les eines de configuració

Amb els objectes **"ProBuilder"** a la barra d'eines veuràs un icona taronja, si l'apretes, a la barra de configuració apereixen icones per manipular l'objecte.

<center>
<img src="./assets/probuilder-tools.png" style="width: 90%; max-width: 300px">
</center>
<br/>

Aquesta barra, permet fer modificacions bàsiques als objectes, per canviar-ne la forma:

- Vertex
- Cantons
- Cares

<center>
<video src="./assets/probuilder-modifybasics.mov" width="600" controls></video>
</center>

## Modificar Objectes

Entre altres, les principals opcions disponibles per modificar objectes són:

- **Extrude**, afegir cares
- **Bevel**, arrodonir cantonades
- **Subdivide faces**, dividir una cara
- **Merge faces**, unir cares
- **Delete Face**, fer forats
- **Cut Tool**, dividir segons polígon

### Extrude

La opció **"Extrude"** permet agafar una cara d’un objecte i estirar-la per crear volum nou.

Un cop tenim una cara sel·leccionada, escollim extrude al menú o l'estirem amb la tecla **"Mayus"** apretada:

<center>
<video src="./assets/probuilder-modifyextrude.mov" width="600" controls></video>
</center>

### Bevel

La opció **"Bevel"** permet arrodonir cantonades

<center>
<video src="./assets/probuilder-modifybeveledges.mov" width="600" controls></video>
</center>

### Subdivide faces

La opció **"Subdivide faces"** permet dividir una cara en parts.

<center>
<video src="./assets/probuilder-modifysubdivideface.mov" width="600" controls></video>
</center>

> *Nota:* A l'exemple anterior, primer es fa un **"Subdivide"** i després un **"Extrude"**

### Merge faces

La opció **"Merge faces"** permet unir cares.

<center>
<video src="./assets/probuilder-modifymergefaces.mov" width="600" controls></video>
</center>

### Delete face

La opció **"Delete face"** esborrar una cara, per fer forats.

<center>
<video src="./assets/probuilder-modifydeleteface.mov" width="600" controls></video>
</center>

### Cut Tool

La eina **"Cut Tool"** permet dividir cares de manera personalitzada.

<center>
<video src="./assets/probuilder-modifycuttool.mov" width="600" controls></video>
</center>

> *Nota:* A l'exemple anterior, primer es fa servir **"Cut Tool"** i després un **"Extrude"**