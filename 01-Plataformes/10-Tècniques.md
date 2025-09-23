# Tècniques

Aquí hi ha un resum de les tècniques utilitzades en aquest projecte.

## 🎮 Control i moviment del jugador

- Input System: El Player i PlayerJump utilitzen el nou UnityEngine.InputSystem per capturar moviments i salts.
- Moviment horitzontal amb Rigidbody2D: El Player aplica velocitat directa al cos físic (rb.linearVelocity).
- Salt amb millores modernes (PlayerJump):

    - Coyote Time: permet saltar encara que hagis sortit una mica de la plataforma.
    - Jump Buffer: si prems salt just abans de tocar terra, el joc el guarda i el fa quan sigui possible.
    - Variable Jump Height: si deixes anar el botó abans, el salt és més baix.

## ⚔️ Interacció i col·lisions

- Coins: es recullen amb OnTriggerEnter2D, incrementant la puntuació i actualitzant el HUD.
- Dany de trampes (PlayerDamage):
    - Detecta col·lisions amb objectes marcats amb tag.
    - Sistema de damage over time amb cooldown.
    - Feedback visual: el jugador fa flash en vermell amb coroutines.

## 🟦 Plataformes mòbils

- El Platform mou el contenidor "Sprites" seguint una sèrie de waypoints.
- Té suport de ping-pong o loop i pot fer pauses als punts.
- Calcula la velocitat de la superfície perquè el jugador pugui "enganxar-se" i moure’s correctament amb la plataforma.
- Dibuixa el camí amb Gizmos a l’editor per visualitzar els punts.

## 📷 Càmera

CameraFollow segueix el jugador amb:

- SmoothDamp per suavitzar el moviment.
- Offsets per ajustar la posició.
- Límits opcionals amb Mathf.Clamp per no sortir del nivell.

## 🎨 Animació i presentació

PlayerAnimation:

- Canvia automàticament entre Idle, Run, Jump i Fall segons l’estat.
- Fa servir SpriteRenderer.flipX per girar el personatge segons la direcció.

HUD i UI (UIHUD i PlayerDamage):

- Barra de vida feta amb Image i ajustant el RectTransform de healthFill.
- Comptador de monedes amb TextMeshProUGUI.
- Pantalla de Game Over que pausa el temps (Time.timeScale = 0f).
- Botó per reiniciar l’escena amb SceneManager.LoadScene.