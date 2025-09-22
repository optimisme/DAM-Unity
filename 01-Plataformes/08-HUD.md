# HUD

Heads-Up-Display mostra informació important durant la partida.

Afegeix un nou objecte tipus **"Canvas"**

<center>
<img src="./assets/hud-addcanvas.png" style="width: 90%; max-width: 600px">
</center>
<br/>

**Nota:** Fixa't que automàticament ha afegit un objecte *EventSystem*, no l'esborris!

## Health bar

Dins del canvas afegeix un objecte buit i anomena'l "Healthbar", indicarà el nivell de vida.

Dins de la **"Healthbar"** crea dos objectes tipus **"Image"**.

<center>
<img src="./assets/hud-addimage.png" style="width: 90%; max-width: 600px">
</center>
<br/>

Anomena'ls **"Background"** i **"Health"** respectivament:

<center>
<img src="./assets/hud-healthbarhierarchy.png" style="width: 90%; max-width: 600px">
</center>
<br/>

Si fas **"doble click"** al canvas, veuràs l'area de dibuix del HUD.

<center>
<img src="./assets/hud-doubleclick.png" style="width: 90%; max-width: 600px">
</center>
<br/>

Modifica els paràmetres de l'objecte **"Healthbar"**:

- Pos X: -620
- Pos Y: 435

Modifica els paràmetres de l'objecte **"Healthbar > Background"** així:

- Scale X: 5
- Scale Y: 0.5
- Color: Negre

Modifica els paràmetres de l'objecte **"Healthbar > Health"** així:

- Pivot X: 0
- Scale X: 4.9
- Scale Y: 0.4
- Color: Verd

**Nota**: El "Pivot X" farà que la barra es faci més petita mantenint la posició esquerra fixe, enlloc de la central

De manera que quedi així:

<center>
<img src="./assets/hud-healthbarparameters.png" style="width: 90%; max-width: 600px">
</center>

Per tornar a la vista de l'escena (nivell de zoom de l'escena), fes doble click a **"Main Camera"**.

## Visualització

Durant la partida el canvas es sobreposa a la càmera.

Prova, com es veu la barra de nivell mentre juges.

## Script

Per tal de fer que la barra de nivell canvii, cal adaptar **"PlayerDamage.cs"**

```csharp
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerDamage : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;           // <-- afegit
    public int health = 100;
    [SerializeField] private int damagePerHit = 10;
    [SerializeField] private float damageCooldown = 0.3f;

    [Header("HUD (Healthbar)")]
    [Tooltip("Arrossega aquí l'objecte UI 'Healthbar/Health' (el verd)")]
    [SerializeField] private RectTransform healthFill;  // <-- afegit
    private Vector3 fillBaseScale = Vector3.one;        // <-- escala base capturada a l'inici

    [Header("Damage Feedback")]
    [SerializeField] private int flashCount = 2;
    [SerializeField] private float flashDuration = 0.1f;

    private float lastDamageTime = -999f;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // Inicialitza salut i barra
        health = Mathf.Clamp(health, 0, maxHealth);
        if (healthFill != null)
        {
            fillBaseScale = healthFill.localScale; // p.ex. (4.9, 0.4, 1) segons el teu setup
            UpdateHealthUI();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Damage"))
            TakeDamage(damagePerHit);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Damage"))
            TakeDamage(damagePerHit);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
            TakeDamage(damagePerHit);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
            TakeDamage(damagePerHit);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        lastDamageTime = Time.time;

        health = Mathf.Max(0, health - amount);
        Debug.Log($"Vida restant: {health}");

        UpdateHealthUI();             // <-- actualitza la barra
        StartCoroutine(FlashRed());

        if (health == 0)
        {
            Debug.Log("El jugador ha mort!");
            // TODO: lògica de mort
        }
    }

    // (Opcional) per curar
    public void Heal(int amount)
    {
        health = Mathf.Min(maxHealth, health + amount);
        Debug.Log($"Vida després de curar: {health}");
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (!healthFill) return;

        float t = (maxHealth > 0) ? (float)health / maxHealth : 0f; // 0..1
        // Manté l'alçada i profunditat originals, només escala l'ample en X
        healthFill.localScale = new Vector3(fillBaseScale.x * t, fillBaseScale.y, fillBaseScale.z);
    }

    private IEnumerator FlashRed()
    {
        Color original = sr.color;
        for (int i = 0; i < flashCount; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            sr.color = original;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
```

Cal vincular l'objecte **"Health"** amb la nova variable **"Health fill"** de l'script **"PlayerDamage.cs"**, arrosegant l'objecte a la variable.

<center>
<img src="./assets/hud-draghealth.png" style="width: 90%; max-width: 600px">
</center>