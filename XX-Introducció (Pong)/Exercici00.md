# Exercici 00

Modifica el "Pong" de l'exemple anterior, per fer un "Tenis" per dos jugadors locals.

- Els jugadors estàn a la dreta i esquerra de la pantalla
- El jugador de l'esquerra es mou amunt i avall amb 'W' i 'S'
- El jugador de la dreta es mou amunt i avall amb les fletxes
- El marcador compta els punts de cada jugador
- El primer jugador que fa tres punts guanya el joc

Tingues en compte que al codi original els moviments del jugador (paleta) estàn limitats a l'eix Y, i s'hauràn de canviar a l'eix X.

```csharp
// Objecte 'MovimentJugador'
// Funció SetupRigidbody
// Es congela la rotació i el moviment en Y (només pot moure’s en X).
rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY
```

Tingues en compte que caldrà actualitzar la posició del jugador verticalment modificant la velocitat
```csharp
// Objecte 'MovimentJugador'
// FixedUpdate
// Apliquem la velocitat al Rigidbody2D en l’eix X.
rb.linearVelocity = new Vector2(velocity, 0f);

// Versió per aplicar la velocitat al Rigidbody2D en l’eix Y.
rb.linearVelocity = new Vector2(0f, velocity);
```