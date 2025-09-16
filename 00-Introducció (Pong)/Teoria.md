# Introducció a Unity (Pong)

Unity és un **motor de videojocs** que permet fer jocs per diferents plataformes. (Windows, Linux, Android, iOS, ...)

El software de unity està compost per múltiples aplicacions.

## Unity HUB

[Unity Hub](https://unity.com/es/download) és el gestor de Unity que permet organitzar:

- Els projectes
- Les versions del software

<center>
<img src="./assets/unity-hub.png" style="width: 90%; max-width: 400px">
</center>

## Unity Engine

**Unity Engine** és el nucli que permet que un joc funcioni, són llibreries que faciliten la programació de:

- Físiques
- Animacions
- IA
- Recursos
- ...

## Unity Editor

**Unity Editor** és una aplicació gràfica que permet crear i organitzar els objectes del joc.

Fa servir **Unity Engine** com a base de les llibreries.

<center>
<img src="./assets/unity-editor.png" style="width: 90%; max-width: 400px">
</center>

## Visual Studio Code

Permet programar el codi (controladors) del joc.

El codi dels jocs Unity és C# (C Sharp), i ens permetrà:

- Escriure el comportament dels objectes
- Programar la lògica del joc
- Gestionar recuros i interfícies
- Accedir al *Unity Engine* a través de la **API**

# Crear projectes Unity

Per crear un nou projecte Unity, cal fer servir **Unity HUB**

<center>
<img src="./assets/unity-hub-newproject.png" style="width: 90%; max-width: 400px">
</center>

Escull la opció *"2D Built-In Render Pipeline"*, i posa nom al teu joc (projecte):

<center>
<img src="./assets/unity-hub-builtinpipeline.png" style="width: 90%; max-width: 300px">
</center>

Escull la carpeta on guardaràs els projectes de Unity.

# Interfície de Unity Editor

La interfície de *Unity Editor* s'organitza en aquestes seccions:

<center>
<img src="./assets/unity-editor-interface.png" style="width: 90%; max-width: 300px">
</center>

- **Toolbar**: Accés ràpid a accions essencials. 

- **Hierarchy**: Reperesentació de les escenes i objectes que té el joc.

- **Game**: Simulació visual de com el joc es veu a través de les càmeres.

- **Inspector**: Edició de les propietats dels objectes.

- **Assets**: llibreria d'objectes/arxius que es necessiten pel joc.

## Afegir objectes predefinits

A la secció 'Hierarchy', obrir el menú de context amb el botó dret.

Escollir "2D objects" > Sprites > Square

<center>
<img src="./assets/unity-editor-addsquare.png" style="width: 90%; max-width: 300px">
</center>

Això afegeix un "quadre" al nostre joc. Canvia-li les propietats amb l'inspector per tal que siguin:

<center>
<img src="./assets/unity-editor-obj-mursuperior.png" style="width: 90%; max-width: 300px">
</center>

- **Nom**: Mur superior
- **Posició Y**: 4
- **Scale X**: 15
- **Scale Y**: 0.5

Fixa't que a la pestanya **"Scene"** hi ha opcions per canviar les propietats visualment:

<center>
<img src="./assets/unity-editor-sceneproperties.png" style="width: 90%; max-width: 300px">
</center>

- **Hand**: La mà, canvia la posició de la vista sense canviar els objectes.

- **Direction arrows**: Canvien la posició dels objectes
    * La vermella a l'eix X
    * La fletxa verda a l'eix Y
    * La blava a l'eix Z

- **Rotation arrows**: Canvien la rotació dels objectes:
    * El cercle vermell la rotació X
    * El cercle verd la rotació Y
    * El cercle blau la rotació Z

- **Scale**: Canvien la mida dels objectes:
    * El quadre vermell la rotació X
    * El quadre verd la rotació Y
    * El quadre blau la rotació Z

Fes dues còpies del mur superior, i canvia les propietats per tal que siguin:

<center>
<img src="./assets/unity-editor-obj-muresquerra.png" style="width: 90%; max-width: 300px">
</center>

<center>
<img src="./assets/unity-editor-obj-murdret.png" style="width: 90%; max-width: 300px">
</center>

Afegeix el rectangle que serà el jugador:

<center>
<img src="./assets/unity-editor-obj-jugador.png" style="width: 90%; max-width: 300px">
</center>

Afegeix el cercle que serà la pilota:

<center>
<img src="./assets/unity-editor-obj-pilota.png" style="width: 90%; max-width: 300px">
</center>

Ara la jerarquia i l'escena s'haurien de veure així:

<center>
<img src="./assets/unity-editor-status00.png" style="width: 90%; max-width: 300px">
</center>

I s'hi s'apreta el botó de 'Play', el joc sense interactivitat:

<center>
<img src="./assets/unity-editor-status01.png" style="width: 90%; max-width: 300px">
</center>

## Definir objectes amb col·lisió

Per tal que els murs facin rebotar la pilota, cal definir-los una col·lisió.

Això es fa afegint un *"Component"* tipus **"Box Collider 2d"** des de l'inspector. 

### Col·lisions als murs

Cal afegir-lo als 3 murs (no al jugador):

<center>
<img src="./assets/unity-editor-boxcollider2d.png" style="width: 90%; max-width: 300px">
</center>

Cal fixar-se que el "Box Collider 2d" es marca a l'escena amb la forma taronja que fa la col·lisió:

<center>
<img src="./assets/unity-editor-addscript.png" style="width: 90%; max-width: 600px">
</center>

### Col·lisions al jugador (paleta)

El jugador necessita dos components:

- **"Box Collider 2d"**, per definir que la forma de la col·lisió sigui rodona
- **"Rigidbody 2d"**, per marcar la pilota com que està afectada per físiques

A més, el jugador ha de desactivar les físiques amb **"Body Type"** a *Kinematic*:

<center>
<img src="./assets/unity-editor-obj-jugadorkinematic.png" style="width: 90%; max-width: 300px">
</center>

### Col·lisions a la pilota

La pilota cal afegir dos components:

- **"Circle Collider 2d"**, per definir que la forma de la col·lisió sigui rodona
- **"Rigidbody 2d"**, per marcar la pilota com que està afectada per físiques

A més, cal anular la gravetat posant el camp **"Gravity Scale"** a 0:

<center>
<img src="./assets/unity-editor-obj-pilotagravity.png" style="width: 90%; max-width: 300px">
</center>

## Scripts de moviment

A la secció **"Project"** a la part inferior, afegir un **"Asset"** tipus **"Empty C# Script"** amb el menú de context del botó dret:

<center>
<img src="./assets/unity-editor-addscript.png" style="width: 90%; max-width: 600px">
</center>

Anomena l'script com "MovimentJugador"

<center>
<img src="./assets/unity-editor-scripts.png" style="width: 90%; max-width: 300px">
</center>

Obre l'script al **Visual Studio Code**, amb un doble click.

Posa-hi aquest codi:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentJugador : MonoBehaviour
{
    public float velocitat = 8f;

    public Transform murEsquerra;  // Assign in Inspector or found by name
    public Transform murDreta;

    private Rigidbody2D rb;
    private Collider2D colPala;
    private Collider2D colMurEsq;
    private Collider2D colMurDre;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colPala = GetComponent<Collider2D>(); // paddle collider (for half width)

        // Auto-find by name if not set in Inspector
        if (murEsquerra == null) { var go = GameObject.Find("Mur esquerra"); if (go) murEsquerra = go.transform; }
        if (murDreta   == null) { var go = GameObject.Find("Mur dreta");   if (go) murDreta   = go.transform; }

        // Get wall colliders
        if (murEsquerra) colMurEsq = murEsquerra.GetComponent<Collider2D>();
        if (murDreta)    colMurDre = murDreta.GetComponent<Collider2D>();

        // Safety constraints (avoid rotation / vertical move)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    void FixedUpdate()
    {
        // Read keyboard directly (A/D or Left/Right)
        float x = 0f;
        if (Keyboard.current?.leftArrowKey.isPressed == true || Keyboard.current?.aKey.isPressed == true) x -= 1f;
        if (Keyboard.current?.rightArrowKey.isPressed == true || Keyboard.current?.dKey.isPressed == true) x += 1f;

        rb.velocity = new Vector2(x * velocitat, 0f);

        // If colliders exist, clamp using real bounds (includes scale)
        if (colPala && colMurEsq && colMurDre)
        {
            // Bounds are world-space AABBs
            float halfPala = colPala.bounds.extents.x;     // half width of paddle
            float rightOfLeftWall = colMurEsq.bounds.max.x; // right edge of left wall
            float leftOfRightWall = colMurDre.bounds.min.x; // left edge of right wall

            float minX = rightOfLeftWall + halfPala;
            float maxX = leftOfRightWall - halfPala;

            float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
            rb.position = new Vector2(clampedX, rb.position.y);
        }
    }
}
```

Fes el mateix per afegir un script "MovimentPilota.cs"

Posa-hi aquest codi:

```csharp
using UnityEngine;

public class MovimentPilota : MonoBehaviour
{
    public float velocitat = 6f;            // Velocitat inicial
    public float incrementVelocitat = 0.25f;

    private Rigidbody2D rb;

    // Constants de direcció
    private static readonly Vector2 AMUNT_ESQUERRA = new Vector2(-1, 1).normalized;
    private static readonly Vector2 AMUNT_DRETA   = new Vector2( 1, 1).normalized;
    private static readonly Vector2 AVALL_ESQUERRA= new Vector2(-1,-1).normalized;
    private static readonly Vector2 AVALL_DRETA   = new Vector2( 1,-1).normalized;

    private Vector2 direccioActual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Sortida inicial: sempre amunt (esquerra o dreta aleatori)
        direccioActual = Random.value < 0.5f ? AMUNT_ESQUERRA : AMUNT_DRETA;
        rb.linearVelocity = direccioActual * velocitat;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 normal = col.contacts[0].normal;

        // Decideix si és rebot vertical o horitzontal
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        {
            // Rebot vertical → invertir X
            direccioActual = new Vector2(-direccioActual.x, direccioActual.y).normalized;
        }
        else
        {
            // Rebot horitzontal → invertir Y
            direccioActual = new Vector2(direccioActual.x, -direccioActual.y).normalized;
        }

        // ✅ Ús correcte: sense ()
        if (col.gameObject.name == "Pala")
        {
            velocitat += incrementVelocitat;
        }

        rb.linearVelocity = direccioActual * velocitat;
    }
}
```

Vincula cada un dels cripts, amb el seu inspector, arrossegant l'script des dels **Assets** fins a **Inspector**

<center>
<video src="./assets/unity-editor-linkscript.mov" width="300" controls></video>
</center>

- Script **MovimentJugador** amb l'inspector del **Jugador**

- Script **MovimentPilota** amb l'inspector de la **Pilota**

