# Fonaments dels shaders

Un **shader** és un programa que executa la GPU. En renderització determina com es transforma i es dibuixa una superfície. **Shader Graph** permet construir aquest programa connectant nodes; Unity genera el codi necessari per a les plataformes compatibles.

## Shader, material i textura

| Element | Què representa | Exemple |
|---|---|---|
| Shader / Shader Graph | Regles de càlcul i propietats disponibles | Barrejar dos colors amb un patró |
| Material | Un shader amb uns valors concrets | Rajoles blaves, juntes grises, escala 8 |
| Textura | Dades mostrejades pel shader | Imatge de roca, normal map, màscara |
| Mesh Renderer | Component que associa materials a una malla visible | Material assignat a una paret |

Un mateix shader pot servir per a molts materials. Dos objectes que comparteixen material comparteixen els seus valors: modificar aquell material afecta tots dos.

Una textura no substitueix un shader: el shader pot llegir-la, tenyir-la, barrejar-la amb altres textures o animar-ne les coordenades. També pot calcular patrons sense imatges.

## Del model a la imatge

```text
Vèrtexs de la malla → etapa Vertex → rasterització → etapa Fragment → imatge
```

- **Vertex**: treballa sobre els vèrtexs existents. Pot modificar posició, normal i tangent.
- **Rasterització**: determina els fragments coberts pels triangles i interpola dades com les UV.
- **Fragment**: calcula propietats de la superfície per als fragments. Un fragment és un candidat a contribuir a un píxel; pot quedar ocult o ser descartat.

Els blocs Vertex i Fragment del Master Stack són destinacions del gràfic, no nodes generadors de textures. Les operacions connectades a cada etapa es calculen en aquell context, subjectes a les restriccions dels nodes.

## Lit i Unlit

| Tipus | Ús |
|---|---|
| **Unlit** | Color independent de la il·luminació habitual; útil per entendre patrons |
| **Lit** | Superfícies que reaccionen a la llum: pedra, rajoles, metall, aigua |

A Unlit, Base Color defineix el color de la superfície, encara que el resultat final pugui passar per postprocessament. A Lit, Base Color participa en el càlcul d'il·luminació juntament amb normals i altres propietats.

## Patrons procedurals i imatges

| Tècnica | Avantatge | Cost o límit |
|---|---|---|
| Patró matemàtic | Parametritzable i sense una imatge per al patró | Càlcul GPU i possibles vores dentades o parpelleig |
| Textura d'imatge | Detall artístic complex amb mostreig | Memòria, resolució, filtratge i repetició visible |
| Combinació | Detall de textura amb variació procedural | Cal controlar tots dos costos |

Un patró matemàtic no té una resolució d'imatge fixa, però la pantalla sí: els detalls massa fins poden produir aliasing. Un shader amb molt soroll no és automàticament més ràpid que llegir una textura. Cal comparar amb el resultat i el dispositiu objectiu.

## Llegir un gràfic

Segueix el recorregut **dades → transformacions → resultat**. Per exemple:

```text
UV → patró blanc/negre → Lerp entre dos colors → Fragment / Base Color
```

Les fletxes transporten valors; no són un ordre d'execució com les instruccions d'un script. Les previsualitzacions ajuden a inspeccionar resultats intermedis.

**Errors habituals:** confondre material i shader; esperar ombres d'un material Unlit; pensar que els shaders només serveixen per a efectes especials; assumir que procedural sempre és més barat.
