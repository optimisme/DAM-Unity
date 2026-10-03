# Animacions

## Elements

| Element | Funció |
|---|---|
| Animation Clip (`.anim`) | Canvis de propietats al llarg del temps |
| Animator Controller (`.controller`) | Estats, transicions i paràmetres |
| Animator | Component que executa el Controller sobre un objecte |
| State | Estat que reprodueix un clip o un altre moviment |
| Transition | Regla per passar d’un estat a un altre |
| Loop Time | Repetició del clip |
| Apply Root Motion | Aplicar el moviment de l’arrel de l’animació al personatge |

El component Animator ha de tenir el Controller assignat. Els noms dels paràmetres i dels estats han de coincidir amb els del Controller.

## Paràmetres

`animator` és una referència a un component Animator:

| Tipus | Instrucció | Ús habitual |
|---|---|---|
| Float | `animator.SetFloat("speed", speed)` | Velocitat o intensitat |
| Int | `animator.SetInteger("mode", 1)` | Selecció d’un mode |
| Bool | `animator.SetBool("isWalking", true)` | Condició que persisteix |
| Trigger | `animator.SetTrigger("jump")` | Petició consumida per una transició |
| Reset de Trigger | `animator.ResetTrigger("jump")` | Retirar una petició pendent |

Modificar un paràmetre només canvia l’estat si hi ha una transició configurada que l’utilitza.

## Transicions

| Opció | Significat |
|---|---|
| Conditions | Condicions dels paràmetres |
| Has Exit Time | Exigeix arribar al punt temporal de sortida |
| Transition Duration | Temps de mescla; comprovar si és normalitzat o fix |
| Any State | Permet definir una transició des de diversos estats |

## Canvi directe per codi

Dins d’un mètode; els estats indicats han d’existir:

```csharp
animator.Play("Base Layer.Idle", 0, 0f);
animator.CrossFadeInFixedTime("Base Layer.Walk", 0.2f);
```

| Mètode | Durada |
|---|---|
| `Play` | Canvi directe; el tercer argument indica el temps normalitzat inicial |
| `CrossFade` | Durada de transició normalitzada |
| `CrossFadeInFixedTime` | Durada de transició en segons |

Les dues instruccions anteriors són alternatives, no una seqüència necessària. [Durades de CrossFadeInFixedTime](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Animator.CrossFadeInFixedTime.html).

## Consultar l’estat

Dins d’un mètode; la capa 0 ha d’existir:

```csharp
AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
bool walking = state.IsName("Base Layer.Walk");
float progress = state.normalizedTime;
bool transitioning = animator.IsInTransition(0);
```

`normalizedTime` representa el progrés de l’estat: en bucle, la part entera compta voltes. Un valor major o igual a 1 no demostra, per si sol, que hagi acabat el clip que acabes de demanar: comprova l’estat i les transicions.

**Errors habituals:** paràmetres amb noms diferents; cridar Play cada fotograma i reiniciar l’animació; confondre Trigger amb Bool; expressar CrossFade en segons; moure el personatge simultàniament amb root motion i amb codi sense coordinar-los.

**Demo:** [Palanca: pont amb Animator](<Demo-5-Palanca.md>).

**Relacionat:** [Entrada](<Teoria-2-Entrada.md>) · [Moviment del jugador](<Teoria-8-Moviment del jugador.md>).
