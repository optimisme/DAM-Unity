# Resum de tècniques del joc de plataformes

Consulta l'[ordre de treball](./00-Assets.md) per seguir els tutorials. Tots construeixen la mateixa escena Nivell01. El decorat del capítol 10 també forma part de la mateixa escena.

| Component o recurs | Responsabilitat |
|---|---|
| Grid i Tilemap | Organitzar i pintar el terreny |
| TilemapCollider2D + CompositeCollider2D | Unir la col·lisió de les rajoles |
| Controls / Gameplay / PlayerInput | Convertir tecles en Move i Jump |
| Player | Moviment horitzontal i comptador de monedes |
| PlayerJump | Detectar suport i autoritzar el salt |
| PlayerAnimation | Escollir el clip i orientar el sprite |
| CameraFollow | Seguir el jugador tenint en compte la mida visible |
| Coin | Recollir una moneda una sola vegada |
| PlayerDamage | Guardar la vida i aplicar dany per contacte |
| UIHUD | Mostrar les dades amb text i Image Filled |
| LevelFinish | Comprovar la meta i les monedes, mostrar victòria i reiniciar |
| GameOverController | Detectar derrota, desactivar controls, pausar i reiniciar |
| Platform | Moure un cos cinemàtic entre punts |
| ParallaxTilemapLayer | Moure i repetir Tilemaps decoratius segons la profunditat |

<center>
<img src="./assets/tecniques-joc-parallax.png" alt="Recorregut completat a Unity amb cinc monedes i tota la vida" style="width: 95%; max-width: 1000px">
</center>
<br/>

## Conceptes que no s'han de confondre

- **Sprite i collider:** una cosa dibuixa; l'altra defineix el contacte físic.
- **Layer, Sorting Layer i Tag:** filtratge físic, ordre visual i identificació lògica són funcions diferents.
- **Trigger i col·lisió sòlida:** una moneda es travessa i dispara un event; el terra sosté el jugador.
- **Update i FixedUpdate:** llegim estat i actualitzem presentació per fotograma; movem els cossos físics als passos de física.
- **Velocitat i intenció:** una plataforma pot moure el jugador encara que MoveX sigui zero.
- **Pausa i entrada:** Time.timeScale a zero no desactiva per si sol tots els callbacks ni Update.
- **Profunditat i ordre de dibuix:** Depth canvia el moviment aparent; Order in Layer decideix què tapa què.
- **Decoració i terreny:** els Tilemaps de parallax no tenen colliders i no poden servir de plataforma.
- **UI i dades:** la barra llegeix Health; no és la font de la vida del jugador.

## Prova final

1. Comença amb vida completa i zero monedes.
2. Camina, salta i comprova els quatre estats d'animació.
3. Recull monedes i comprova el HUD.
4. Rep dany, allunya't de la trampa i comprova que s'atura.
5. Puja a una plataforma, queda't quiet i deixa que canviï de direcció.
6. Prova el joc a dues proporcions de pantalla.
7. Mor per dany, reinicia i després mor per caiguda.
8. Travessa unions dels fons i comprova que la vegetació del davant no amaga obstacles.
9. Completa el recorregut de comprovació: recull les cinc monedes, arriba al terra dret (X = 28) amb vida i puja i baixa de la plataforma mòbil. Acaba a la bandera: ha d’aparèixer Nivell completat i s’ha de poder tornar a jugar, tal com explica [Final de partida](./12-FinalPartida.md).
10. Si has fet l'ampliació, comprova coyote time, buffer i altura variable sense dobles salts.

Si una prova falla, torna al tutorial del component corresponent i revisa primer les referències de l'Inspector i els errors de Console.
