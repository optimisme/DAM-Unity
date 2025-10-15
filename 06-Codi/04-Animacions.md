# Animacions

A *Unity*, les animacions es gestionen principalment a través de tres components:

- **Animation clip**: .anim que conté la seqüència de moviments (posicions, rotacions, paràmetres, etc.)
- **Animator Controller**: .controller que defineix els estats i les transicions entre animacions.
- **Component Animator**: component del GameObject que executa el controlador i les animacions associades.

## Des dels paràmetres del controller

Es poden definir **"Parameters"** al **"Animator"**, i canviar-los el valor o activar-los des del codi.

Aquests paràmetres, han d'activar transicions **"Transition"** per provocar canvis entre estats **"State"**.

```csharp
Animator animator = GetComponent<Animator>();
animator.SetBool("isWalking", true);
animator.SetTrigger("attack");
animator.SetFloat("speed", 3.5f);
```

## Des dels codi

Es pot forçar manualment una animació concreta:
```csharp
animator.Play("Run");               // Canviar a l'animació de cop
animator.CrossFade("Jump", 0.25f);  // Transició suau de 0.25millisegons des de l'estat actual
```

Saber el punt en què està l'animació:
```csharp
AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
if (info.normalizedTime >= 1f)
    Debug.Log("Animació completada!");
// normalizedTime va de 0.0 a 1.0 (o més, si l’animació és en loop).
```

## Exemple

Imagina que tenim un personatge amb 3 estats:

- Idle
- Walk
- Jump

I un controlador amb els paràmetres:

- speed (float)
- isJumping (bool)

### Amb *"Transitions"* i *"Parameters"* al controller

```csharp
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    Animator animator;

    [Header("Config")]
    public float moveThreshold = 0.1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- 1) Llegir moviment horitzontal ---
        float h = Input.GetAxis("Horizontal");

        // --- 2) Actualitzar paràmetres ---
        animator.SetFloat("speed", Mathf.Abs(h));

        bool moving = Mathf.Abs(h) > moveThreshold;
        animator.SetBool("isWalking", moving);

        // --- 3) Salt (trigger únic) ---
        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("jump");

        // --- 4) Exemple de canvi manual ---
        if (Input.GetKeyDown(KeyCode.R))
            animator.Play("Roll"); // estat amb aquest nom
    }
}
```

### Gestió completa des del codi

```csharp
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class PlayerAnimDriver : MonoBehaviour
{
    public enum AnimState { Idle, Walk, Jump }

    [Header("Refs")]
    public Animator animator;
    public Renderer targetRenderer; // opcional: per canviar color segons estat

    [Header("Animator state names (exactes)")]
    public string idleState  = "Idle";
    public string walkState  = "Walk";
    public string jumpState  = "Jump";

    [Header("Control des del codi")]
    [Tooltip("Durada del crossfade entre estats (segons).")]
    public float crossFade = 0.20f;

    [Tooltip("Llindar de final de clip (normalizedTime).")]
    public float endThreshold = 0.98f;

    [Header("Colors per estat (opcionals)")]
    public Color idleColor = new Color(0.8f, 0.8f, 1f);
    public Color walkColor = new Color(0.8f, 1f, 0.8f);
    public Color jumpColor = new Color(1f, 0.8f, 0.6f);

    // Intern
    AnimState _current;
    int _idleHash, _walkHash, _jumpHash;
    Coroutine _oneShotCo;

    void Reset()
    {
        animator = GetComponent<Animator>();
        // Intenta agafar Renderer automàticament
        if (!targetRenderer) targetRenderer = GetComponentInChildren<Renderer>();
    }

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        _idleHash = Animator.StringToHash(idleState);
        _walkHash = Animator.StringToHash(walkState);
        _jumpHash = Animator.StringToHash(jumpState);

        // Arrenca a Idle
        ForceState(AnimState.Idle);
    }

    void Update()
    {
        // 1) Input molt simple: A/D o ←/→ per caminar, SPACE per saltar
        float h = Input.GetAxisRaw("Horizontal");
        bool wantsWalk = Mathf.Abs(h) > 0.1f;

        // 2) Salt (one-shot): salta quan premem, després tornem a Idle quan acabi el clip
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayOneShot(AnimState.Jump, returnTo: AnimState.Idle);
            return; // prioritat salt
        }

        // 3) Caminar vs Idle (clips en loop)
        if (_oneShotCo == null) // si no estem reproduint un one-shot
        {
            if (wantsWalk && _current != AnimState.Walk)
                CrossFadeTo(AnimState.Walk);
            else if (!wantsWalk && _current != AnimState.Idle)
                CrossFadeTo(AnimState.Idle);
        }
    }

    // --------- Motor d’estats (només C#) ---------

    void CrossFadeTo(AnimState next)
    {
        if (_current == next) return;
        _current = next;

        int hash = StateToHash(next);
        animator.CrossFade(hash, crossFade, 0, 0f);
        ApplyColor(next);
    }

    void ForceState(AnimState next)
    {
        _current = next;
        int hash = StateToHash(next);
        animator.Play(hash, 0, 0f);
        ApplyColor(next);
    }

    void PlayOneShot(AnimState oneShot, AnimState returnTo)
    {
        // Si ja hi ha un one-shot en reproducció, el cancelem i comencem el nou
        if (_oneShotCo != null) StopCoroutine(_oneShotCo);
        _oneShotCo = StartCoroutine(PlayOneShotCo(oneShot, returnTo));
    }

    IEnumerator PlayOneShotCo(AnimState oneShot, AnimState returnTo)
    {
        CrossFadeTo(oneShot);

        // Esperar fins al final del clip actual
        while (true)
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            // Nota: si el clip està en loop, normalizedTime seguirà pujant >1.
            // Per one-shots, posa el clip sense loop al Animator.
            if (info.normalizedTime >= endThreshold)
                break;
            yield return null;
        }

        CrossFadeTo(returnTo);
        _oneShotCo = null;
    }

    int StateToHash(AnimState s)
    {
        switch (s)
        {
            case AnimState.Idle: return _idleHash;
            case AnimState.Walk: return _walkHash;
            case AnimState.Jump: return _jumpHash;
        }
        return _idleHash;
    }

    void ApplyColor(AnimState s)
    {
        if (!targetRenderer) return;
        var c = s switch
        {
            AnimState.Idle => idleColor,
            AnimState.Walk => walkColor,
            AnimState.Jump => jumpColor,
            _ => Color.white
        };

        // Evitem tocar materials en Editor: en Play és segur fer material/color
        var mats = targetRenderer.materials; // instància per no pintar tots els usos
        for (int i = 0; i < mats.Length; i++)
        {
            if (mats[i].HasProperty("_BaseColor")) mats[i].SetColor("_BaseColor", c);
            else if (mats[i].HasProperty("_Color")) mats[i].SetColor("_Color", c);
        }
    }
}
```