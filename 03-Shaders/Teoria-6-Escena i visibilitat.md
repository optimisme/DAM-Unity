# Escena i visibilitat

Alguns efectes necessiten informació del que ha dibuixat la càmera o dades enviades pel joc. Aquesta fitxa prepara l'aigua amb escuma i la demo **6-Forat**, que deixa veure el personatge a través d'una paret interposada.

## Color i profunditat

| Node | Dades que llegeix | Preparació en URP |
|---|---|---|
| Scene Color | Còpia del color de l'escena opaca | Opaque Texture; utilitzar-lo en una superfície Transparent |
| Scene Depth | Textura de profunditat de la càmera | Depth Texture; comprovar quan es genera i què hi escriu |

Revisa l'URP Asset actiu i les opcions de la càmera, que poden sobreescriure la configuració. Aquests nodes depenen del pipeline i de l'etapa de dibuix. No s'han d'interpretar com una lectura arbitrària del fotograma final.

Scene Color en URP no inclou habitualment els objectes transparents dibuixats després de la còpia. Scene Depth tampoc és una llista de totes les superfícies al llarg d'un raig. [Scene Color](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Scene-Color-Node.html) · [Scene Depth](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Scene-Depth-Node.html).

## Distorsió del fons

```text
screenUV = Screen Position (Default).xy
uvMostra = screenUV + desplacamentUV
colorFons = Scene Color(uvMostra)
```

El desplaçament pot provenir d'un sinus o d'un soroll recentrat al voltant de zero. Es llegeix el color d'un altre punt de la còpia de pantalla: **no es mouen els objectes ni els píxels de la geometria original**.

Un material transparent sobre una malla limita l'efecte a la superfície d'aquella malla; no equival automàticament a un postprocessament de pantalla completa. Cal controlar les vores de pantalla i els límits de la informació disponible darrere d'objectes.

<video src="assets/tecniques-filterDeformDemo.mov" width="400" controls loop></video>

## Profunditat i interseccions visuals

Per produir escuma prop d'una riba, podem comparar la profunditat del fons amb la de la superfície de l'aigua, a les mateixes coordenades de pantalla.

```text
separacio = profunditatFonsEye - profunditatAiguaEye
escuma = 1 - Smoothstep(0, ampladaEscuma, Maximum(separacio, 0))
```

AmpladaEscuma ha de ser positiva. Totes dues profunditats han d'estar en la mateixa escala lineal de vista. Una opció conceptual per a la superfície és **−Position(View).z**, coherent amb profunditat Eye. No comparis directament profunditat Raw, Linear01, coordenades de món i distància euclidiana a la càmera.

Aquesta diferència mesura separació en profunditat de càmera. **No és una col·lisió física ni la distància mínima entre dues malles.** Cal tractar el fons sense geometria, l'angle de vista i la disponibilitat de profunditat abans de donar l'efecte per acabat.

## Aigua estilitzada amb textures animades

Una superfície plana també pot semblar aigua. En lloc de desplaçar vèrtexs, podem combinar dos materials: un al fons i un a la superfície. La [recreació d’Eyan Martucci inspirada en Super Mario Galaxy](https://eyanmartucci.com/stylized-water-shader/) és una referència visual d’aquest plantejament; no és documentació del shader original de Nintendo.

| Capa | Operacions | Resultat |
|---|---|---|
| Fons | Mostra RG d’una textura, centra el vector i desplaça les UV de la textura de pedra | Deformació aparent del fons |
| Fons | Dues mostres animades de càustiques i un tint segons l’alçada sota l’aigua | Il·luminació mòbil i sensació de profunditat |
| Superfície | Dues textures de reflexos amb desplaçaments diferents, màscara i Alpha Clipping | Taques clares de contorn definit |
| Escuma | Distància a les vores UV i, per als obstacles, comparació de profunditats Eye | Vores del pla i contacte visual amb roques |

Per al fons, la profunditat pot ser **nivellAigua − Position(World).y**. És una distància vertical en món, diferent de la separació Eye utilitzada per l’escuma. No barregis aquestes dues mesures.

L’escuma de vores UV només reconeix el contorn de la malla: una roca al mig del pla no modifica les UV i, per tant, no hi genera escuma. Per detectar-la afegim **Scene Depth Eye − (−Position View.z)** i unim aquesta màscara amb la de les vores. Les roques han de contribuir a la profunditat opaca i Depth Texture ha d’estar activat.

Aquest exemple distorsiona la textura del material del fons; no llegeix Scene Color ni refracta qualsevol objecte de l’escena. La demo **4-Aigua** inclou les dues aproximacions en escenes separades: `DemoAigua` mou vèrtexs i `DemoAiguaGalaxy` anima textures sobre una superfície plana.

## Forat: veure el personatge darrere d'una paret

L'efecte combina dues responsabilitats:

| Part | Responsabilitat |
|---|---|
| Codi del joc | Identificar parets que oculten el personatge i enviar-ne el centre projectat |
| Shader de la paret | Calcular la màscara en pantalla i descartar els fragments interiors |

El shader de la paret no coneix automàticament quin objecte és el jugador. Un script pot projectar un punt del personatge amb la càmera del joc i proporcionar **centreUV**, **radi** i **activació** com a propietats.

### Màscara circular

Amb Screen Position Default i la correcció d'aspecte de la fitxa de coordenades:

```text
delta = screenUV - centreUV
distancia = length((delta.x * amplada / alcada, delta.y))
fora = Step(radi, distancia)
alpha = Lerp(1, fora, activacio)
```

En aquest esquema **activacio és 0 o 1**, no un control de suavitat. Connecta alpha a Alpha i utilitza Alpha Clipping amb llindar 0.5: quan està actiu, s'elimina l'interior del cercle. Per animar-ne l'aparició, varia el radi amb el temps.

### Activar-lo només quan hi ha una paret interposada

El codi pot consultar el segment entre la càmera i el punt del personatge, filtrant una capa de parets. Cal excloure el jugador, tractar diversos obstacles i restaurar les parets que deixen d'interposar-se. El criteri de consulta i el punt objectiu es concretaran a la demo.

Un únic raig comprova un segment, però no tota la silueta del personatge. Caldrà escollir el comportament desitjat per a obstacles parcials. Les parets del darrere del personatge no s'han d'activar només perquè coincideixin amb el cercle en pantalla.

### Límits i comprovacions

- Si el personatge és darrere de la càmera, desactiva l'efecte; no utilitzis cegament la seva projecció.
- Comprova formats de pantalla diferents, moviment de càmera i sortida del personatge del camp visible.
- Evita activar altres parets accidentalment perquè comparteixen material.
- El retall visual no modifica el collider: el personatge no pot travessar la paret per aquest forat.
- Revisa també les ombres: un clipping dependent de la càmera no té necessàriament el comportament desitjat al passi d'ombres.

Aquest enfocament no requereix llegir Scene Color per mostrar el que hi ha darrere: els fragments es descarten i la resta de geometria es renderitza segons profunditat.

## Stencil com a ampliació

Stencil és un buffer de marques que permet condicionar on es dibuixen altres passis. Pot servir per a portals o màscares entre objectes, però no calcula per si mateix si una paret oculta el personatge. La configuració depèn del shader i del renderer; no és un node que es pugui afegir sense preparar els passis. La primera versió de Forat es pot plantejar amb Alpha Clipping.

**Errors habituals:** esperar que Scene Color contingui tots els transparents; restar profunditats en escales diferents; confondre intersecció visual amb col·lisió; activar el forat a qualsevol paret sense comprovar l'oclusió.
