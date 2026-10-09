# Projectes de les demos

Cada ZIP conté un **projecte Unity independent**, amb l’escena acabada, els scripts, els recursos i la configuració necessària. No cal completar les demos anteriors ni importar el ZIP dins d’un altre projecte.

## Requisits

- **Unity Hub** i **Unity 6.6, versió 6000.6.3f1**, la versió amb què s’han preparat aquests projectes. Utilitza preferiblement aquesta mateixa versió.
- Connexió a Internet per descarregar els paquets en la primera obertura, si encara no són a la memòria cau.
- Espai lliure per als fitxers que Unity generarà: el projecte obert ocupa força més que el ZIP.

## Obrir una demo

1. Descarrega el ZIP i **descomprimeix-lo completament** en una carpeta local on puguis escriure.
2. Obre **Unity Hub** i selecciona **Add → Add project from disk** (el nom pot variar segons l’idioma o la versió del Hub).
3. Selecciona la carpeta descomprimida que conté directament **Assets**, **Packages** i **ProjectSettings**.
4. Obre el projecte amb **6000.6.3f1**. Espera que Unity instal·li els paquets i importi els recursos; la primera obertura pot trigar uns minuts.
5. En acabar la primera importació, s’obre automàticament l’escena de la demo i queda enquadrada a Scene.
6. Prem **Play** i clica la vista **Game** perquè rebi el teclat. Consulta el tutorial de la demo per als controls i les proves.
7. Atura Play abans de desar canvis permanents a l’escena.

La primera vegada, els materials poden trigar una estona a mostrar els colors correctes.

El menú **Demo > Obrir escena principal** permet tornar a la demo. L’obertura automàtica només actua el primer cop; després es respecten les escenes amb què estiguis treballant.

## Descàrregues

Les mides són aproximades, en MB decimals; la mida descomprimida és abans que Unity generi la memòria cau.

| Projecte | Escena | ZIP | Descomprimit | Tutorial |
|---|---|---:|---:|---|
| [Demo-0-Objectes](<Demo-0-Objectes.zip>) | `DemoObjectes.unity` | 0.07 MB | 0.24 MB | [Veure tutorial](<../Demo-0-Objectes.md>) |
| [Demo-1-Prefabs](<Demo-1-Prefabs.zip>) | `DemoPrefabs.unity` | 0.07 MB | 0.23 MB | [Veure tutorial](<../Demo-1-Prefabs.md>) |
| [Demo-2-Plataformes](<Demo-2-Plataformes.zip>) | `DemoPlataformes.unity` | 0.06 MB | 0.23 MB | [Veure tutorial](<../Demo-2-Plataformes.md>) |
| [Demo-3-Monedes](<Demo-3-Monedes.zip>) | `DemoMonedes.unity` | 0.89 MB | 4.28 MB | [Veure tutorial](<../Demo-3-Monedes.md>) |
| [Demo-4-Nivells](<Demo-4-Nivells.zip>) | `DemoNivells.unity` | 0.87 MB | 4.27 MB | [Veure tutorial](<../Demo-4-Nivells.md>) |
| [Demo-5-Mecanismes](<Demo-5-Mecanismes.zip>) | `DemoMecanismes.unity` | 0.87 MB | 4.25 MB | [Veure tutorial](<../Demo-5-Mecanismes.md>) |
| [Demo-6-Palanca](<Demo-6-Palanca.zip>) | `DemoPalanca.unity` | 0.88 MB | 4.28 MB | [Veure tutorial](<../Demo-6-Palanca.md>) |
| [Demo-7-Guardia](<Demo-7-Guardia.zip>) | `DemoGuardia.unity` | 0.88 MB | 4.29 MB | [Veure tutorial](<../Demo-7-Guardia.md>) |
| [Demo-8-Duel](<Demo-8-Duel.zip>) | `DemoDuel.unity` | 0.87 MB | 4.26 MB | [Veure tutorial](<../Demo-8-Duel.md>) |
| [Demo-9-Salt](<Demo-9-Salt.zip>) | `DemoSalt.unity` | 0.86 MB | 4.23 MB | [Veure tutorial](<../Demo-9-Salt.md>) |

## Què inclouen els ZIP?

- **Assets/**: escena, scripts, materials i altres recursos de la demo, amb els seus fitxers **.meta**.
- **Packages/**: llista de paquets i versions que Unity ha d’instal·lar.
- **ProjectSettings/**: configuració del projecte.

No inclouen **Library**, **Temp** ni **Logs**. Unity genera aquestes carpetes automàticament; per això el ZIP és petit. No esborris els fitxers **.meta**, perquè mantenen les referències entre recursos.

Els projectes utilitzen **URP**, **Input System** i càmeres en **perspectiva**, ja configurats. La demo Plataformes correspon a l’alternativa URP descrita al tutorial i la seva escena es diu **DemoPlataformes**.

## Si no s’obre correctament

- Si Unity Hub no reconeix el projecte, comprova que has triat la carpeta amb Assets, Packages i ProjectSettings, no la carpeta exterior ni el ZIP.
- Si falten paquets, comprova la connexió i espera que acabi Package Manager abans de prémer Play.
- Si havies canviat d’escena, torna a la demo amb **Demo > Obrir escena principal**. Si el menú no apareix, espera que acabi la compilació i comprova Console.
- Si el personatge no respon, comprova que Play està actiu, que Pause està desactivat i clica Game.
- Si hi ha errors de compilació o materials roses, comprova la versió de Unity i que la instal·lació dels paquets hagi acabat.

## Comprovació dels projectes

Revisió repetida el **9 d’octubre de 2026** sobre els fitxers de la branca **main** de DAM-Unity.

Les demos **4-Nivells a 9-Salt** s’han reconstruït a partir de l’escena inicial de **Universal 3D**, amb els scripts literals dels tutorials. S’han contrastat les taules de posicions i dimensions, les referències dels components, les capes i els recursos inclosos als ZIPs.

La prova en Play inclou **66 comprovacions** amb Unity 6000.6.3f1 a macOS:

| Demo | Comprovacions principals |
|---|---|
| Nivells | Tres esferes, paret transparent amb collider, gir de càmera, pujar amb l’ascensor, porta i tresor |
| Mecanismes | Reixa bloquejada, empènyer la caixa a la placa, mantenir-la oberta en sortir, tresor i reinici; activar i alliberar la placa amb el jugador |
| Palanca | Distància de la palanca, animacions, colliders del pont, bloqueig mentre està ocupat i arribar al tresor |
| Guàrdia | Recollir la clau i escapar, reinici, visió tapada per murs, persecució i pèrdua de visió, malla del camp visual |
| Duel | Punteria i clic del ratolí, avís i direcció fixa del tret enemic, esquiva, visió tapada, projectils físics, interval entre trets, murs, dany, victòria, derrota i reinici |
| Salt | Salt curt i llarg, els tres salts del recorregut, checkpoint, caiguda i reaparició, meta i reinici; coyote time, jump buffer i absència de doble salt |

Els recorreguts de Nivells, Mecanismes, Palanca, Guàrdia i Salt s’han completat amb un teclat virtual i els scripts de moviment i la física reals. Les proves aïllades de visió i projectils col·loquen els personatges en posicions controlades; el duel també comprova punteria i dispars amb un ratolí virtual. La reconstrucció i aquestes proves són automatitzades amb l’API de Unity, no una repetició manual de tots els clics de l’Editor.

Els sis ZIPs s’han extret en carpetes noves per comprovar la primera obertura amb l’Editor gràfic: l’escena apareix automàticament, entra en Play i es comproven els colors quan acaba la càrrega. Les eines de prova no s’inclouen als ZIPs. Els scripts dels tutorials coincideixen amb els dels projectes.

No és una validació en Windows o Linux. Durant les proves batch, una excepció del cercador intern de l’Editor (`UnityEditor.Search.SearchDatabase`) s’ha registrat separadament dels errors de les demos.
