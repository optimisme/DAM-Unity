# Dades i propietats

Un node rep valors pels ports d'entrada i produeix valors pels de sortida. Abans de connectar-lo, identifica **el tipus, el rang i el significat** de cada valor.

## Tipus habituals

| Tipus | Components | Ús |
|---|---|---|
| Float | Un valor | Velocitat, gruix, intensitat o màscara |
| Vector2 | X, Y | Coordenades UV o velocitat de desplaçament |
| Vector3 | X, Y, Z | Posició, direcció, normal o RGB |
| Vector4 | X, Y, Z, W | Quatre canals; per exemple RGBA |
| Color | R, G, B, A | Color amb selector a l'Inspector |
| Texture2D | Recurs de textura | Entrada de Sample Texture 2D |
| Boolean | Cert/fals | Selecció de comportament |

X/Y/Z/W i R/G/B/A són noms de components. Un Vector3 no sap si representa un color o una posició: ho determina l'ús. Una Texture2D és un recurs; **Sample Texture 2D** en llegeix un valor en unes coordenades.

## Constants i propietats

- Una **constant** queda definida dins del gràfic.
- Una **propietat** del Blackboard permet reutilitzar un paràmetre en diferents punts.
- Amb **Exposed** activat, una propietat compatible es pot editar al material.
- **Display Name** és l'etiqueta visible. **Reference** és l'identificador que utilitza el codi, per exemple `_LineWidth`.

| Propietat d'exemple | Tipus | Valor inicial | Significat |
|---|---|---|---|
| Color A | Color | Blau | Color de fons |
| Color B | Color | Blanc | Color del patró |
| Repeticions | Float | 8 | Nombre de cel·les en una unitat UV |
| Gruix | Float | 0.1 | Fracció de cada cel·la ocupada per la línia |
| Velocitat | Vector2 | (0.1, 0) | Unitats UV per segon |

El valor inicial del Blackboard serveix de valor per defecte. Un material pot tenir-ne un de propi. Un lliscador facilita l'edició, però el shader ha de tractar els rangs que necessiti, especialment si el valor s'assigna per codi.

## Components i conversions

**Split** separa components; **Combine** els torna a agrupar. Per a les UV, R correspon a U i G a V. Per als colors, els mateixos ports corresponen a vermell i verd.

Molts nodes accepten vectors de mida variable i operen component a component. Un escalar utilitzat com a vector pot repetir-se en els components. És millor fer explícita la intenció:

```text
UV + (0.2, 0) → modifica només U
UV + (0.2, 0.2) → modifica U i V
```

No connectis un Vector3 a un port escalar esperant que calculi automàticament la mitjana. Extreu o calcula el valor desitjat.

## Colors i rangs

Els valors no estan sempre limitats entre 0 i 1. Posicions, temps, sumes i colors HDR poden sortir d'aquest rang. Una previsualització blanca pot ocultar un valor superior a 1.

L'alpha és un canal de dades: per tenir efecte s'ha de connectar al bloc adequat i configurar la superfície. Connectar un Color només a Base Color no activa la transparència.

## Propietats controlades pel joc

Un script pot enviar temps propi, intensitat o una posició al shader mitjançant la Reference de la propietat. Aquest és el pont necessari per informar el shader de la posició del personatge a la demo Forat.

Cal decidir si el valor és compartit per tots els objectes o específic d'un Renderer. Modificar un material compartit afecta tots els objectes que l'utilitzen; crear còpies de materials també té un cost. L'elecció concreta i les seves implicacions de batching es tractaran a la demo.

**Errors habituals:** propietat no exposada; canviar la Reference sense actualitzar el codi; interpretar una UV com un color; confondre un recurs de textura amb el valor mostrejat.
