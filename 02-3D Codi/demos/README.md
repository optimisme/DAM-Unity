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
5. A la finestra **Project**, entra a **Assets → Scenes** i fes doble clic a l’escena indicada a la taula.
6. Prem **Play** i clica la vista **Game** perquè rebi el teclat. Consulta el tutorial de la demo per als controls i les proves.
7. Atura Play abans de desar canvis permanents a l’escena.

## Descàrregues

Les mides són aproximades, en MB decimals; la mida descomprimida és abans que Unity generi la memòria cau.

| Projecte | Escena | ZIP | Descomprimit | Tutorial |
|---|---|---:|---:|---|
| [Demo-0-Objectes](<Demo-0-Objectes.zip>) | `DemoObjectes.unity` | 0.07 MB | 0.24 MB | [Veure tutorial](<../Demo-0-Objectes.md>) |
| [Demo-1-Prefabs](<Demo-1-Prefabs.zip>) | `DemoPrefabs.unity` | 0.06 MB | 0.23 MB | [Veure tutorial](<../Demo-1-Prefabs.md>) |
| [Demo-2-Plataformes](<Demo-2-Plataformes.zip>) | `DemoPlataformes.unity` | 0.06 MB | 0.23 MB | [Veure tutorial](<../Demo-2-Plataformes.md>) |
| [Demo-3-Monedes](<Demo-3-Monedes.zip>) | `DemoMonedes.unity` | 0.87 MB | 4.25 MB | [Veure tutorial](<../Demo-3-Monedes.md>) |
| [Demo-3-Nivells](<Demo-3-Nivells.zip>) | `DemoNivells.unity` | 0.88 MB | 4.25 MB | [Veure tutorial](<../Demo-3-Nivells.md>) |
| [Demo-4-Mecanismes](<Demo-4-Mecanismes.zip>) | `DemoMecanismes.unity` | 0.88 MB | 4.24 MB | [Veure tutorial](<../Demo-4-Mecanismes.md>) |
| [Demo-5-Palanca](<Demo-5-Palanca.zip>) | `DemoPalanca.unity` | 0.89 MB | 4.27 MB | [Veure tutorial](<../Demo-5-Palanca.md>) |

## Què inclouen els ZIP?

- **Assets/**: escena, scripts, materials i altres recursos de la demo, amb els seus fitxers **.meta**.
- **Packages/**: llista de paquets i versions que Unity ha d’instal·lar.
- **ProjectSettings/**: configuració del projecte.

No inclouen **Library**, **Temp** ni **Logs**. Unity genera aquestes carpetes automàticament; per això el ZIP és petit. No esborris els fitxers **.meta**, perquè mantenen les referències entre recursos.

Els projectes utilitzen **URP**, **Input System** i càmeres en **perspectiva**, ja configurats. La demo Plataformes correspon a l’alternativa URP descrita al tutorial i la seva escena es diu **DemoPlataformes**.

## Si no s’obre correctament

- Si Unity Hub no reconeix el projecte, comprova que has triat la carpeta amb Assets, Packages i ProjectSettings, no la carpeta exterior ni el ZIP.
- Si falten paquets, comprova la connexió i espera que acabi Package Manager abans de prémer Play.
- Si apareix una escena buida, obre manualment l’escena de la taula.
- Si el personatge no respon, comprova que Play està actiu, que Pause està desactivat i clica Game.
- Si hi ha errors de compilació o materials roses, comprova la versió de Unity i que la instal·lació dels paquets hagi acabat.

## Comprovació dels projectes

Els set ZIP s’han descomprimit en carpetes independents i s’han provat amb Unity 6000.6.3f1 a macOS: importació, compilació, obertura de l’escena i sis segons en Play. No s’han detectat scripts perduts ni errors dels scripts durant aquesta prova d’arrencada. No és una prova completa de totes les mecàniques ni una validació en Windows o Linux.

En la prova automàtica sense interfície gràfica, Unity ha mostrat una excepció del seu cercador intern (`UnityEditor.Search.SearchDatabase`). S’ha registrat separadament dels errors dels scripts; queda pendent comprovar si també apareix en obrir els projectes amb la interfície habitual.
