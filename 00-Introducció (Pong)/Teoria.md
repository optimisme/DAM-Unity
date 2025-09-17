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
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private float leftBound, rightBound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        
        // Find walls if not assigned - keeping original names
        if (!leftWall) leftWall = GameObject.Find("Mur esquerra")?.transform;
        if (!rightWall) rightWall = GameObject.Find("Mur dreta")?.transform;
        
        CalculateBounds();
        SetupRigidbody();
    }

    private void SetupRigidbody()
    {
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    private void CalculateBounds()
    {
        if (leftWall && rightWall && playerCollider)
        {
            float halfWidth = playerCollider.bounds.extents.x;
            leftBound = leftWall.GetComponent<Collider2D>().bounds.max.x + halfWidth;
            rightBound = rightWall.GetComponent<Collider2D>().bounds.min.x - halfWidth;
        }
    }

    private void FixedUpdate()
    {
        if (GameState.gameOver) return;

        float input = GetMovementInput();
        float velocity = baseSpeed * GameState.speedFactor * input;
        
        rb.linearVelocity = new Vector2(velocity, 0f);
        ClampPosition();
    }

    private float GetMovementInput()
    {
        float input = 0f;
        
        if (Keyboard.current?.leftArrowKey.isPressed == true || 
            Keyboard.current?.aKey.isPressed == true)
            input -= 1f;
            
        if (Keyboard.current?.rightArrowKey.isPressed == true || 
            Keyboard.current?.dKey.isPressed == true)
            input += 1f;
            
        return input;
    }

    private void ClampPosition()
    {
        if (leftWall && rightWall)
        {
            var pos = rb.position;
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
            rb.position = pos;
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
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float speedIncrement = 0.5f;

    private Rigidbody2D rb;
    private float currentSpeed;
    private Vector2 direction;

    // Diagonal directions (45 degrees)
    private static readonly Vector2 UP_LEFT = new Vector2(-1, 1).normalized;
    private static readonly Vector2 UP_RIGHT = new Vector2(1, 1).normalized;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
    }

    private void Start()
    {
        SetupRigidbody();
        ResetBall();
    }

    private void SetupRigidbody()
    {
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (!GameState.gameOver)
        {
            // Maintain constant speed
            rb.linearVelocity = direction * currentSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        
        // Reflect direction based on collision normal
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            direction = new Vector2(-direction.x, direction.y); // Horizontal bounce
        else
            direction = new Vector2(direction.x, -direction.y); // Vertical bounce
            
        direction = direction.normalized;

        // Check if hit player - keeping original name check
        if (collision.gameObject.name == "Jugador" || collision.collider.transform.root.name == "Jugador")
        {
            OnPlayerHit();
        }
    }

    private void OnPlayerHit()
    {
        currentSpeed += speedIncrement;
        GameState.points++;
        GameState.speedFactor = Mathf.Max(1f, currentSpeed / initialSpeed);
    }

    private void OnBecameInvisible()
    {
        // Ball left screen - game over
        GameState.gameOver = true;
        rb.linearVelocity = Vector2.zero;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        currentSpeed = initialSpeed;
        
        // Random initial direction (up-left or up-right)
        direction = Random.value < 0.5f ? UP_LEFT : UP_RIGHT;
        
        if (rb) rb.linearVelocity = direction * currentSpeed;
    }
}
```

Vincula cada un dels cripts, amb el seu inspector, arrossegant l'script des dels **Assets** fins a **Inspector**

<center>
<video src="./assets/unity-editor-linkscript.mov" width="300" controls></video>
</center>

Fes un nou script amb les variables compartides entre els objectes del joc. 

Crea un script "GameState.cs":

```csharp
using UnityEngine;

public static class GameState
{
    public static float speedFactor = 1f;
    public static int points = 0;
    public static bool gameOver = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetOnLoad()
    {
        speedFactor = 1f;
        points = 0;
        gameOver = false;
    }

    public static void Reset()
    {
        speedFactor = 1f;
        points = 0;
        gameOver = false;
    }
}
```

### Valors inicials de les variables de l'Script

Les variables dels *scripts* marcades com a "public" permeten decidir el valor des de *l'inspector*, tot i tenir definit un altre valor inicial.

<center>
<img src="./assets/unity-editor-obj-jugadorvelocitat.png" style="width: 90%; max-width: 400px">
</center>

**Important!** El valor definit gràficament a Inspector té preferència al definit pel codi

**Prova**: Fes públiques les variables *murEsquerra* i *murDret* per assignar-les des de l'Inspector enlloc de fer-ho al mètode *Start()*


## UIHUD mostrar informació

**UIHUD (user interface heads-up display)** és el mètode pel que es mostra informació visual al jugador.

A 'Hierarchy' afegeix un 'Text (TMP)', segurament et demanarà que importis el paquet *TMP Essentials*

<center>
<img src="./assets/unity-editor-obj-hud.png" style="width: 90%; max-width: 400px">
</center>

Importar 'TMP Essentials' si ho demana.

<center>
<img src="./assets/unity-editor-tmpimporter.png" style="width: 90%; max-width: 400px">
</center>

A la jerarquia creará dos objectes:

- Canvas per dibuixar-hi informació
- Text (amb la informació)

<center>
<img src="./assets/unity-editor-hud-hierarchy.png" style="width: 90%; max-width: 400px">
</center>

A l'inspector del *Canvas* assigna **"Render Mode"** com a **"Screen Space - Camera"**

I arrossega la **"Main Camera"** des de *Hierarchy* fins a la opció **"Render Camera"**

<center>
<video src="./assets/unity-editor-hud-camera.mov" width="400" controls></video>
</center>

Canvia el nom de l'objecte *"Text (TMP)"* per *Punts* 

A l'inspector de l'objecte *"Punts"* posa "..." com a valor per defecte del text.

I ajusta si ho veus conveninet *"Pos X" i "Pos Y"

<center>
<img src="./assets/unity-editor-hud-text.png" style="width: 90%; max-width: 400px">
</center>

Augmenta la mida d'escriptura del text, amb la "Move Tool":

<center>
<video src="./assets/unity-editor-hud-textresize.mov" width="400" controls></video>
</center>

Afegeix l'script "UIHUD" següent:

```csharp
using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    private int lastPoints = -1;
    private float lastSpeedFactor = -1f;
    private bool lastGameOverState = false;

    private void Awake()
    {
        // Auto-find UI elements by name like original code
        if (!scoreText) scoreText = GameObject.Find("Punts")?.GetComponent<TextMeshProUGUI>();
        if (!gameOverText) gameOverText = GameObject.Find("GameOver")?.GetComponent<TextMeshProUGUI>();
        
        if (gameOverText) gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateScoreDisplay();
        UpdateGameOverDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText && (GameState.points != lastPoints || GameState.speedFactor != lastSpeedFactor))
        {
            scoreText.text = $"Factor: x{GameState.speedFactor:F2} - Punts: {GameState.points}";
            lastPoints = GameState.points;
            lastSpeedFactor = GameState.speedFactor;
        }
    }

    private void UpdateGameOverDisplay()
    {
        if (gameOverText && GameState.gameOver != lastGameOverState)
        {
            gameOverText.gameObject.SetActive(GameState.gameOver);
            lastGameOverState = GameState.gameOver;
        }
    }
}
```

Arrossega l'script **"UIHUD"** com a un nou component a l'inspector de **"Punts"**

<center>
<video src="./assets/unity-editor-hud-textscript.mov" width="400" controls></video>
</center>

## Game Over i gestor del joc

Afegeix un nout ext dins del *Canvas* anomenat **GameOver**

<center>
<img src="./assets/unity-editor-hud-gameover-hierarchy.png" style="width: 90%; max-width: 400px">
</center>

Centra el text i fes-lo gran:

<center>
<img src="./assets/unity-editor-hud-gameover-properties.png" style="width: 90%; max-width: 400px">
</center>

Crea un nou objecte **"Empty"**, això ens servirà per posar-hi un *Component* d'script i res més.

<center>
<img src="./assets/unity-editor-obj-manager.png" style="width: 90%; max-width: 400px">
</center>

Anomena l'objecte anterior com a **"GameManager"

Crea un nou script **"GameManager"** amb aquest codi i arrossega'l a l'inspector de l'objecte "GameManaer" com un nou *Component*

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MovimentPilota ball;
    [SerializeField] private Transform player;

    private void Awake()
    {
        // Find by GameObject name like original code
        if (!ball)
        {
            var go = GameObject.Find("Pilota");
            if (go) ball = go.GetComponent<MovimentPilota>();
            else Debug.LogWarning("[GameManager] No GameObject named 'Pilota' found");
        }
        if (!player)
        {
            var go = GameObject.Find("Jugador");
            if (go) player = go.transform;
            else Debug.LogWarning("[GameManager] No GameObject named 'Jugador' found");
        }
    }

    private void Start()
    {
        RestartGame();
    }

    private void Update()
    {
        if (GameState.gameOver && Keyboard.current?.spaceKey.wasPressedThisFrame == true)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        GameState.Reset();
        CenterPlayer();
        if (ball) ball.ResetBall();
    }

    private void CenterPlayer()
    {
        if (player)
        {
            var pos = player.position;
            pos.x = 0f;
            player.position = pos;
        }
    }
}
```

Aquest codi controla el "Game Over" i reinici del joc.