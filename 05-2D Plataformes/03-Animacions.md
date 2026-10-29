# Animacions

**Punt de partida:** jugador amb Player i PlayerJump del tutorial anterior.

Les animacions es formen a partir de diverses imatges que es van mostrant en seqüència.

Es canvia d'animació, segons l'estat del personatge (quiet, caminant, saltant...).

## Animació per defecte (PlayerIdle)

Obre la finestra d'animacions:

*Window > Animation > Animation*

<center>
<img src="./assets/animations-window.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Arrossega la pestanya **Animation** dins de l'editor per tenir-la a mà.

<center>
<img src="./assets/animations-dragwindow.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Selecciona el personatge "Player" i apreta el botó **"Create"** de la finestra d'animacions.

<center>
<img src="./assets/animations-create.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Desa l'animació a la carpeta **Animations** amb el nom **PlayerIdle**. A Project, Assets/Animations conté el clip **PlayerIdle** i el controlador **Player**, que Unity crea i assigna al component Animator.

<center>
<img src="./assets/animations-save.png" alt="Finestra Project amb el controlador Player i el clip PlayerIdle a Assets/Animations" style="width: 90%; max-width: 450px">
</center>
<br/>

Abans de col·locar fotogrames, obre el menú **⋮** de la finestra Animation, activa **Show Sample Rate** i comprova **Samples = 60**. Després arrossega els sprites:

- "Idle 0" a la posició 0:00
- "Idle 1" a la posició 0:20
- "Idle 0" a la posició 0:40

Comprova que el component **Animator** de Player està activat (casella marcada).

<center>
<img src="./assets/animations-check.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Si proves el joc, veuràs que el personatge ja s'anima.

## Definir més animacions

Cal definir les diferents animacions del personatge.

### PlayerJump

A la secció d'animacions, crea un nou clip anomenat **"PlayerJump"**.

<center>
<img src="./assets/animations-newclip.png" style="width: 90%; max-width: 400px">
</center>
<br/>

Desa'l a la carpeta **Animations**. El clip apareix al costat de PlayerIdle.

<center>
<img src="./assets/animations-playerjump.png" alt="Finestra Project amb el clip PlayerJump a Assets/Animations" style="width: 90%; max-width: 450px">
</center>

Posa-hi els següents sprites:

- "Jump 0" a la posició 0:00
- "Jump 1" a la posició 0:10


Desactiva **Loop Time** al clip PlayerJump. En acabar, conserva l’última postura fins que el codi escull PlayerFall; no cal allargar-lo artificialment fins a cinc segons.

### PlayerFall

A la secció d'animacions, crea un nou clip anomenat **"PlayerFall"**. Desa'l a la carpeta **Animations**. El clip apareix al costat de PlayerIdle.

Posa-hi el següent sprite:

- "Jump 2" a la posició 0:00

### PlayerRun

A la secció d'animacions, crea un nou clip anomenat **"PlayerRun"**. Desa'l a la carpeta **Animations**. El clip apareix al costat de PlayerIdle.

Posa-hi els següents sprites:

- "Run 0" a la posició 0:00
- "Run 1" a la posició 0:08
- "Run 2" a la posició 0:15
- "Run 3" a la posició 0:22
- "Run 3" a la posició 0:30

Acobla la pestanya Animation al costat de Project i Console, o tanca-la.

<center>
<img src="./assets/animations-hidetab.png" style="width: 90%; max-width: 400px">
</center>
<br/>

## Entendre el temps dels clips

Tots els clips d'aquest exemple fan servir **Samples = 60**. `0:20` significa segon zero, fotograma vint; no són vint segons. Les posicions anteriors només tenen el mateix ritme si fem servir el mateix nombre de fotogrames per segon.

Selecciona cada clip a Project i configura **Loop Time**:

| Clip | Loop Time | Ús |
|---|---|---|
| PlayerIdle | Activat | Repetir mentre està quiet |
| PlayerRun | Activat | Repetir mentre camina |
| PlayerJump | Desactivat | Conservar l'últim fotograma en pujar |
| PlayerFall | Activat | Una sola postura mentre cau |

Obre **Window > Animation > Animator**. Comprova que hi ha un estat per a cada clip amb aquests noms exactes i que el controlador està assignat al component Animator de Player. Aquest exemple selecciona els estats des del codi; no cal crear transicions automàtiques entre ells.

## Codi complet d'animacions

Crea **PlayerAnimation.cs** i afegeix-lo a Player:

```csharp
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
[RequireComponent(typeof(Player), typeof(PlayerJump))]
public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    private PlayerJump jump;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private string currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        jump = GetComponent<PlayerJump>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // La plataforma pot transportar-nos sense que vulguem caminar.
        float direction = player.MoveX;
        if (Mathf.Abs(direction) > 0.01f) spriteRenderer.flipX = direction < 0f;
        string state = jump.isGrounded
            ? (Mathf.Abs(direction) > 0.01f ? "PlayerRun" : "PlayerIdle")
            : (rb.linearVelocity.y > 0f ? "PlayerJump" : "PlayerFall");
        if (state == currentState) return;
        animator.Play(state);
        currentState = state;
    }
}
```
La direcció i l'animació de caminar utilitzen **MoveX**, la intenció del jugador. Així una plataforma que el transporta no li canvia l'orientació ni el fa caminar visualment quan no prem cap tecla. `flipX` gira només el dibuix, sense invertir la física ni la jerarquia.

## Comprovar el resultat

Es veuen quatre estats diferents i el personatge mira cap a la direcció que demanes.

1. Queda’t quiet, camina, salta i deixa’l caure d’una vora.
2. Comprova cada estat a Animator mentre jugues.

## Errors habituals

- Si un estat no apareix, comprova el nom exacte a Animator.
- Si la velocitat de l’animació és incorrecta, revisa Samples.
- Si es reinicia contínuament, comprova que només canvies d’estat quan el nom és diferent.
