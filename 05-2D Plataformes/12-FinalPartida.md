# Meta i nivell completat

**Punt de partida:** Nivell01 amb tots els capítols 00–11. Afegirem una condició de victòria: recollir les cinc monedes i arribar amb vida a la bandera del tram dret. La derrota del capítol 08 continua funcionant.

## 1. Construir una meta visible

1. Amb Play aturat, crea un objecte buit **Meta** a Position **(28, -1, 0)**, Rotation zero i Scale u.
2. Afegeix **Box Collider 2D**, amb **Size = (1, 2)**, Offset zero i **Is Trigger activat**. No hi afegeixis Rigidbody2D: el jugador ja en porta un.
3. Afegeix dos fills amb **GameObject > 2D Object > Sprites > Square**, anomenats **Pal** i **Bandera**. Conserva els sprites sense colliders. Si Unity afegeix un collider, elimina'l d'aquests dos fills.
4. Posa Pal a **Local Position = (0, 0, 0)**, **Scale = (0.08, 2, 1)**, color **172B4D**. Posa Bandera a **Local Position = (0.4, 0.6, 0)**, **Scale = (0.8, 0.5, 1)**, color **FFC857**. En tots dos Sprite Renderer posa **Order in Layer = 30**. Square ocupa una unitat abans d'aplicar aquesta escala.

El trigger ocupa el tram de terra entre Y = -2 i Y = 0. La bandera no bloqueja el pas. La moneda de X = 24 s'ha de recollir abans d'arribar-hi.

## 2. Objectiu i pantalla de victòria

1. Dins de Canvas, crea **Text - TextMeshPro** anomenat **ObjectiveText**. Escull el preset **top / center** amb Alt+Shift / Option+Shift. Posa **Pos X = 0**, **Pos Y = -88**, **Width = 900**, **Height = 48**, **Font Size = 24**, color **172B4D**, alineació centrada i Raycast Target desactivat. El codi n'actualitzarà el text.
2. Duplica el panell **GameOver** i anomena la còpia **LevelCompleted**. Deixa'l com a últim fill del Canvas.
3. Canvia el text del seu títol a **Nivell completat!** i posa **Font Size = 48**. El botó continua dient **Tornar a jugar**.
4. Al botó de la còpia, elimina l'entrada heretada d'**On Click ()**. La connectarem al nou controlador. No canviïs el botó del panell GameOver original.
5. Deixa LevelCompleted desactivat quan acabis d'editar-lo. El codi també l'amaga a Awake.

## 3. Controlador de la meta

Crea **Assets/Scripts/LevelFinish.cs** amb aquest contingut i afegeix-lo a **Meta**, que sempre ha de quedar actiu:

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerDamage playerDamage;
    [SerializeField] private GameObject completedUI;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private int requiredCoins = 5;
    public bool Completed { get; private set; }

    void Awake()
    {
        if (completedUI) completedUI.SetActive(false);
    }

    void Update()
    {
        if (!Completed && player && objectiveText)
            objectiveText.text = "Recull les " + requiredCoins + " monedes i arriba a la bandera ("
                + player.coins + "/" + requiredCoins + ")";
    }

    void OnTriggerEnter2D(Collider2D other) => TryFinish(other);
    void OnTriggerStay2D(Collider2D other) => TryFinish(other);

    void TryFinish(Collider2D other)
    {
        if (Completed || Time.timeScale == 0f || !player || !playerDamage) return;
        if (other.GetComponentInParent<Player>() != player) return;
        if (playerDamage.Health <= 0 || player.coins < requiredCoins) return;
        Completed = true;
        if (completedUI) completedUI.SetActive(true);
        var input = player.GetComponent<PlayerInput>();
        if (input) input.DeactivateInput();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
```

1. A Meta, assigna **Player**, **Player Damage**, **Completed UI = LevelCompleted**, **Objective Text = ObjectiveText** i **Required Coins = 5**.
2. Al botó de LevelCompleted, afegeix una entrada a **On Click ()**, arrossega **Meta** i escull **LevelFinish > Restart()**.
3. Conserva l'EventSystem amb **Input System UI Input Module** i les accions de UI independents del PlayerInput, com al capítol 08.
4. Desa Nivell01 i comprova que està marcada a **File > Build Profiles > Scene List**.

El trigger comprova el jugador, la vida i el comptador. Entrar-hi abans de tenir totes les monedes no guanya la partida. `Completed` evita repetir la victòria; desactivar l'entrada i pausar la física deixa la UI operativa. El reinici recupera l'escena, les cinc monedes, tota la vida i el temps normal.

## 4. Prova d'una partida completa

1. Comença amb vida 100 i monedes 0. Ves a la bandera sense recollir-les totes: el joc ha de continuar.
2. Reinicia Play. Recull la moneda de l'esquerra i puja a les dues primeres plataformes; mantén Espai als salts alts.
3. Supera el primer forat, puja a la tercera plataforma i recull la seva moneda. Salta per damunt de les trampes cap al terra central dret.
4. Supera el segon forat i recull la cinquena moneda a X = 24. Pots provar la plataforma mòbil abans d'anar a la meta.
5. Arriba a la bandera a X = 28. Han d'aparèixer **Nivell completat!** i el botó **Tornar a jugar**. A/D i Espai no han de moure el personatge.
6. Prem el botó: han de tornar vida 100, monedes 0, les monedes del nivell i els controls.
7. Comprova també la derrota per trampa i per caiguda. Cap d'aquests casos ha d'activar LevelCompleted.

## Errors habituals

- Si la bandera és una paret, activa Is Trigger a Meta i treu els colliders dels sprites fills.
- Si mai guanyes, comprova les referències de Meta i que el nombre de monedes coincideixi amb Required Coins.
- Si el botó no funciona, comprova On Click, EventSystem, Graphic Raycaster i la llista d'escenes.
- No posis LevelFinish dins de LevelCompleted: un panell desactivat no rep els callbacks de la meta.
