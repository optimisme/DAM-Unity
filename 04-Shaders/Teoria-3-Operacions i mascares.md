# Operacions i màscares

Una **màscara** és un valor que controla on s'aplica un efecte. Habitualment 0 significa absència, 1 aplicació completa i els valors intermedis una transició. El mateix valor es pot utilitzar per barrejar colors, controlar emissió o retallar fragments.

## Operacions bàsiques

| Node | Operació | Exemple |
|---|---|---|
| Add | A + B | Desplaçar UV |
| Subtract | A − B | Calcular una diferència de posició |
| Multiply | A × B | Escalar coordenades o intensitat |
| Divide | A / B | Convertir una mida; evita divisors zero |
| One Minus | 1 − In | Invertir una màscara |
| Absolute | Valor absolut | Distància respecte de zero en un eix |
| Fraction | x − floor(x) | Repetir una rampa entre 0 inclòs i 1 exclòs |
| Floor | Enter inferior | Identificar una cel·la |

Les operacions aritmètiques sobre vectors s'apliquen component a component. Sumar colors pot superar 1; no és el mateix que interpolar-los.

## Limitar i transformar rangs

| Node | Entrades | Resultat d'exemple |
|---|---|---|
| Clamp | In, Min, Max | Clamp(1.4, 0.2, 0.8) = 0.8 |
| Saturate | In | Clamp entre 0 i 1 |
| Remap | In, In Min Max, Out Min Max | De [−1,1] a [0,1]: −1 → 0, 0 → 0.5, 1 → 1 |

**Remap** fa una transformació lineal:

```text
out = outMin + (in - inMin) * (outMax - outMin) / (inMax - inMin)
```

Els components X/Y dels dos ports de rang representen mínim/màxim. El rang d'entrada ha de tenir amplada diferent de zero. Remap no limita automàticament el resultat: una entrada fora del rang pot produir una sortida fora del rang. Afegeix Saturate si necessites una màscara 0–1. [Referència Remap](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Remap-Node.html).

## Llindars

**Step(Edge, In)** retorna 0 quan In és menor que Edge i 1 en cas contrari. Amb Edge = 0.5, els valors 0.2 i 0.8 es converteixen en 0 i 1. És útil per separar zones.

**Smoothstep(Edge1, Edge2, In)** crea una transició suau de 0 a 1 entre els dos límits, amb Edge1 menor que Edge2. Per sota retorna 0 i per sobre 1.

| In | Step amb Edge 0.5 | Smoothstep amb límits 0.3 i 0.7 |
|---|---:|---:|
| 0.2 | 0 | 0 |
| 0.5 | 1 | 0.5 |
| 0.8 | 1 | 1 |

Smoothstep és útil per suavitzar una vora o seleccionar regions de soroll. No és un Remap amb el mateix comportament fora del rang. [Referència Smoothstep](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Smoothstep-Node.html).

## Barrejar amb Lerp

```text
Lerp(A, B, T) = A * (1 - T) + B * T
```

- T = 0: resultat A.
- T = 1: resultat B.
- T = 0.25: 75% d'A i 25% de B.

Per a un patró, connecta la màscara a T i els colors a A i B. Lerp no limita T: fora de 0–1 extrapola. **Blend** ofereix modes de composició de colors; cal escollir-ne el mode, no assumir que equival a Lerp.

## Combinar màscares

Per a màscares binàries (0 o 1):

| Operació | Significat |
|---|---|
| Multiply(A,B) | Zones on es compleixen totes dues |
| Maximum(A,B) | Zones on es compleix almenys una |
| One Minus(A) | Zones que A no selecciona |

Amb transicions grises, aquestes operacions també afecten les intensitats de la vora. Saturate(A+B) és una altra combinació possible, però no és igual que Maximum per a valors intermedis.

## Vectors, orientació i Dot Product

```text
dot(A,B) = Ax*Bx + Ay*By + Az*Bz
```

Per tant, dot((1,1,1),(1,1,1)) = **3**. Dot no està limitat a 0–1. Amb direccions normalitzades, retorna el cosinus de l'angle: 1 mateixa direcció, 0 perpendiculars, −1 oposades. [Referència Dot Product](https://docs.unity3d.com/Packages/com.unity.shadergraph@17.0/manual/Dot-Product-Node.html).

Per seleccionar cares que miren amunt, calcula el producte escalar entre la normal normalitzada en espai World i (0,1,0). Una màscara Smoothstep sobre aquest resultat pot controlar la molsa d'una roca. Tots dos vectors han de ser al mateix espai.

**Cross Product** produeix un vector perpendicular als dos vectors d'entrada; l'ordre altera el sentit. No és una operació de barreja de colors.

**Errors habituals:** confondre Multiply amb Dot; esperar límits automàtics a Lerp o Remap; invertir els límits de Smoothstep; no inspeccionar la màscara abans d'aplicar el color.
