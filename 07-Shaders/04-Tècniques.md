# Tècniques habituals

[Vídeo Guinxu, Shaders](https://www.youtube.com/watch?v=U21gkelGwiA)

## Coordenades

Els shaders poden tenir en compte diferents referències de coordenades:

- UV amb **coordenades pantalla**, encara que moguis la càmera o l'objecte mantenen la textura:

<center>
<img src="./assets/tecniques-coordsScreen.png" style="width: 90%; max-width: 600px">
</center>
<br/>

<center>
<video src="./assets/tecniques-coordsScreenDemo.mov" width="400" controls loop></video>
</center>
<br/>


- **Coordenades món**, encara que moguis l'objecte manté la textura (però la càmera es pot moure):

<center>
<img src="./assets/tecniques-coordsWorld.png" style="width: 90%; max-width: 900px">
</center>
<br/>

<center>
<video src="./assets/tecniques-coordsWorldDemo.mov" width="400" controls loop></video>
</center>
<br/>

## Filter

Filter els pixels per crear efectes com canvis de color o de posició dels píxels.

### Shader de canvi de color

Perquè funcioni és important que el shader sigui transparent:

Graph Inspector > Graph Settings > Surface Type > Transparent

- **Screen position** retorna la posició d'un pixel a la pantalla
- **Scene color** retorna el color del píxel en una escena

Després ja només queda operar aquell píxel per canviar-lo de color:

<center>
<img src="./assets/tecniques-filterColor.png" style="width: 90%; max-width: 1024px">
</center>
<br/>

### Shader de canvi de posició

Igualment necessitem un shader transparent.

Apliquem un moviment de sinus a les coordenades UV.

<center>
<img src="./assets/tecniques-filterDeform.png" style="width: 90%; max-width: 1024px">
</center>
<br/>

<center>
<video src="./assets/tecniques-filterDeformDemo.mov" width="400" controls loop></video>
</center>
<br/>



## Intersection Shader

Canvia el color quan un objecte està tocant un altre.

Per exemple fer 'escuma' a l'aigua quan toca els limits.

https://www.youtube.com/watch?v=Uyw5yBFEoXo

- Intersection shader
- Shader normals objecte
- Stencil
