# Superfície i geometria

Un material pot canviar de color, reaccionar a la llum, deixar veure el fons o deformar la malla. Són mecanismes diferents i convé escollir-los segons el resultat desitjat.

## Propietats de superfície Lit

| Propietat | Funció |
|---|---|
| Base Color | Color base que participa en la il·luminació |
| Metallic | Comportament metàl·lic de la superfície |
| Smoothness | Concentració dels reflexos; més alt dona reflexos més definits |
| Normal | Orientació local utilitzada per a la il·luminació |
| Emission | Color emissiu, independent de la llum rebuda |
| Ambient Occlusion | Atenuació de la contribució ambiental segons la configuració |

Una roca sol necessitar variació de color, rugositat i normals; afegir soroll només a Base Color no crea relleu geomètric. Les juntes d'una rajola poden controlar diverses propietats amb una mateixa màscara.

## Normals

Una normal és una direcció perpendicular a la superfície. **Normal Vector** proporciona normals en l'espai escollit; **Normal Map** és una textura que representa variacions d'orientació.

Per utilitzar un normal map habitual en espai tangent:

1. Configura l'asset importat com a **Normal map**.
2. Mostreja'l amb **Sample Texture 2D**, Type **Normal**, en espai **Tangent**.
3. Connecta el resultat al bloc Normal configurat per rebre normals tangents.

El tipus de mostreig i l'espai del bloc han de coincidir. No tractis un normal map com una fotografia RGB ni sumis directament dos normal maps com si fossin colors.

Un normal map modifica la resposta a la llum, però no la silueta ni els colliders. Per seleccionar molsa a les cares superiors es pot utilitzar la normal geomètrica en espai World i un producte escalar amb la vertical.

## Emissió i vores lluminoses

L'emissió permet esquerdes brillants a la lava. Els colors HDR poden superar 1. Per obtenir una resplendor al voltant cal configurar l'efecte de postprocessament **Bloom**; emissió i Bloom són elements diferents. L'emissió no converteix automàticament l'objecte en una llum dinàmica que il·lumina els veïns.

**Fresnel Effect** produeix una intensitat segons l'angle entre la normal i la direcció de vista. És útil per accentuar vores d'un holograma; no detecta interseccions entre objectes. Les direccions han de ser compatibles i Power controla la concentració de l'efecte. [Referència Fresnel](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Fresnel-Effect-Node.html).

## Opaque, Transparent i Alpha Clipping

| Configuració | Comportament | Ús |
|---|---|---|
| Opaque | Superfície opaca | Roca i rajoles |
| Transparent | Barreja amb el que hi ha darrere segons el mode i l'alpha | Aigua o holograma |
| Alpha Clipping | Descarta fragments amb Alpha inferior al llindar | Fulles, dissolució o forat en una paret |

Alpha Clipping es pot activar amb una superfície Opaque. Amb Alpha = 0 dins del forat, Alpha = 1 fora i un llindar de 0.5, l'interior queda descartat. Això permet veure la geometria del darrere sense convertir tota la paret en una superfície semitransparent.

Un Smoothstep connectat a Alpha no crea, per si mateix, una vora semitransparent quan utilitzem clipping: el llindar acaba prenent una decisió de conservar/descartar. Una vora acolorida es pot calcular amb una màscara separada.

La transparència pot tenir problemes d'ordre de dibuix, especialment amb superfícies superposades. Tampoc s'ha de pressuposar que una superfície transparent escriu profunditat igual que una opaca.

## Desplaçament de vèrtexs

Per a una ona en espai Object, sobre una malla orientada al pla XZ:

```text
novaPosicio = posicioObjecte + (0, desplacament, 0)
desplacament = amplitud * sin(posicioObjecte.x * frequencia + temps * velocitat)
```

Connecta **la posició final**, no només el desplaçament, a Vertex / Position en l'espai esperat pel bloc. La freqüència d'aquesta fórmula és angular per unitat espacial.

| Aspecte | Conseqüència |
|---|---|
| Nombre de vèrtexs | Sense subdivisions suficients, l'ona no pot tenir detall |
| Normals | Deformar la posició no garanteix normals correctes per a la nova forma |
| Bounds | Una deformació gran pot sortir dels límits usats per al culling |
| Física | Els colliders no segueixen automàticament la deformació GPU |

A la demo Aigua caldrà triar una malla adequada i distingir el moviment real de vèrtexs del detall aparent de normals.

**Errors habituals:** esperar relleu físic d'un normal map; esperar Bloom sense configurar-lo; connectar alpha sense activar el mode adequat; desplaçar pocs vèrtexs i esperar ones suaus.
