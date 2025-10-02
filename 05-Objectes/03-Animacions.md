# Animacions

## Crear un objecte per animar (porta)

Per fer aquesta part cal tenir **ProBuilder** instal·lat.

Desde *"Window > Package Management > Package Manager > Unity Registry > Busca ProBuilder"*

Amb *ProBuilder* afegeix un objecte a la *Hierarchy*:

*ProBuilder > Door*

Anomena l'objecte *"DoorFrame"*

<center>
<img src="./assets/animacions-adddoorframe.png" style="width: 90%; max-width: 500px">
</center>
<br/>

Afegeix un cub, i mou-lo dins del doorframe:

*ProBuilder > Cube*

Anomena l'objecte *"Door"*

<center>
<img src="./assets/animacinos-adddoor.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Escull l'objecte *"Door"* i entra al mode d'edició de *"ProBuilder"*

<center>
<img src="./assets/animacions-probuildermode0.png" style="width: 90%; max-width: 50px">
</center>
<br/>

- Escull la opció d'editar cares:

<center>
<img src="./assets/animacions-probuildermode1.png" style="width: 90%; max-width: 50px">
</center>
<br/>

- Fes Ctrl+A per escollir totes les cares de l'objecte
- Fes servir la **"fletxa vermella"** per desplaçar horitzontament totes les cares fins al centre de coordenades (més o menys)

<center>
<img src="./assets/animacions-movecube0.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Torna al mode d'edició normal, i comprova que l'origen de coordenades és a l'esquerra del cub.

<center>
<img src="./assets/animacions-movecube1.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Fes servir la **"fletxa vermella"** per desplaçar horitzontament el cub sencer cap a l'esquerra. Gairebé tocant la paret del marc de la porta.

<center>
<img src="./assets/animacions-movecube2.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Posa el punt de vista superior apretant el **"cono verd (y)"**
- Escull la eina d'escalat
- Fes servir el **"quadre blau"** per treure profunditat al cub, transformant-lo en una *porta*
- Fes servir el **"quadre vermell"** per allargar l'ample de la *porta*

<br/>
<center>
<video src="./assets/animacions-scalecube.mov" width="400" controls></video>
</center>

- Entra al mode d'edició de *"ProBuilder"*
- Apreta el botó d'editar cares
- Fes Ctrl+A per escollir totes les cares
- Fes servir la **"fletxa verda"** per moure el polígon cap amunt

<center>
<img src="./assets/animacions-movecube3.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Torna al mode d'edició normal i comprova que l'origen està a baix a l'esquerra

<center>
<img src="./assets/animacions-movecube4.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Canvia a perspectiva isomètrica apretant 

<center>
<img src="./assets/animacions-isometrica.png" style="width: 90%; max-width: 100px">
</center>
<br/>

- Mou l'objecte perquè quedi alineat a baix amb el marc de la porta 

<center>
<img src="./assets/animacions-movecube5.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Escull la eina d'escalat
- Fes servir el **"quadrat verd"** per escalar la porta fins a dalt

<center>
<img src="./assets/animacions-movecube6.png" style="width: 90%; max-width: 500px">
</center>
<br/>

- Torna a la perspectiva normal

## Animar la porta

- Si no existeix, crea la carpeta **"Animations"** dins dels **"Assets"**

- Obre la finestra d'animacions: *Menú Window > Animation > Animation*

- Escull l'objecte **"Door"**

Per poder posar animacions a un objecte, cal definir-li un **"Animator"**

- Apreta el botó **"Create"** per crear un objecte **"Animator"**, guarda la primera animació a la carpeta **"Animations"** com a **"OpenDoor"**

<center>
<img src="./assets/animacions-createanimator0.png" style="width: 90%; max-width: 600px">
</center>
<br/>

<center>
<img src="./assets/animacions-createanimator1.png" style="width: 90%; max-width: 400px">
</center>
<br/>

<center>
<img src="./assets/animacions-createanimator2.png" style="width: 90%; max-width: 400px">
</center>
<br/>

> **Una animació és la definició dels valors d'algunes propietats en moments determinats**

Per definir una animació, afegirem les propietats que volem animar, en aquest cas la **rotació a l'eix Y**

- Apreta **"Add Property"**
- Escull **"Transform Rotation"**
- Apreta el símbol **"+"**

<center>
<img src="./assets/animacions-animate0.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Originalment, s'han introduit **keyframes** automàticament a les posicions 0:00 o 1:00

Els **keyframes** són les posicions clau, on es defineixen els valors de l'animació.

- Marca el **rombo** de la fila **"Rotation Y"** a la posició **"1:00"**
- Defineix el valor de **"Rotation Y"** a 90
- Comprova que l'animació obre la porta

<center>
<img src="./assets/animacions-animate1.png" style="width: 90%; max-width: 400px">
</center>
<br/>

- Escull la pestanya **"Curves"**

<center>
<img src="./assets/animacions-animate2.png" style="width: 90%; max-width: 400px">
</center>
<br/>

- Amb el **"botó dret"** afegeix una **"key"** a la curva i deixa-la així:

<center>
<img src="./assets/animacions-animate3.png" style="width: 90%; max-width: 400px">
</center>
<br/>

> **Nota:** Fixa't que ara l'animació obre la porta més ràpid al principi, i fa com un rebot al final. Les curves serveixen perquè les animacions no es comportin de manera lineal, fent-le més naturals

- Escull la opció **"Create new clip"** dins del menú desplegable:

<center>
<img src="./assets/animacions-newclip.png" style="width: 90%; max-width: 400px">
</center>
<br/>

- Crea un nou clip d'animació a la carpeta **"Animations"** i anomena'l **"CloseDoor"**

- Apreta **"Add Property"**
- Escull **"Transform Rotation"**
- Apreta el símbol **"+"**

Aquest cop cal canviar la posició al valor de temps 0:00

- Marca el **rombo** de la fila **"Rotation Y"** a la posició **"0:00"**
- Defineix el valor de **"Rotation Y"** a 90
- Comprova que l'animació tanca la porta

