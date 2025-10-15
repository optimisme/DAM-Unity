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