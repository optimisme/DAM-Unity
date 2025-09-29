# LLums

Als motors 3D hi ha diferents tipus d'il·luminació:

## ☀️ Directional Light
Projecta raigs de llum **paral·lels** des d’una direcció infinita.
- **No té posició**, només **rotació**.  
- S’usa per simular **el Sol** o qualsevol font molt llunyana.  
- **Efecte**: il·lumina tota l’escena de manera uniforme, sense pèrdua d’intensitat amb la distància.  
<center>
<img src="./assets/llums-lightdirectional.jpg" style="width: 90%; max-width: 400px">
</center>
<br/>


## 💡 Point Light
Una llum que surt **en totes direccions** des d’un punt concret.  
- La intensitat **disminueix amb la distància** segons un radi d’atenuació.  
- S’usa per bombetes, focs, espelmes, faroles, etc.  
- **Efecte**: com una bombeta esfèrica.  
<center>
<img src="./assets/llums-lightpoint.jpg" style="width: 90%; max-width: 400px">
</center>
<br/>


## 🔦 Spot Light
Una llum amb forma de **con**.  
- Té **posició** i una **direcció**, amb un **angle d’obertura** que pots ajustar.  
- També té un radi d’atenuació com la Point Light.  
- S’usa per focus de teatre, llanternes, fars de cotxe…  
- **Efecte**: un feix de llum concentrat.  
<center>
<img src="./assets/llums-lightspot.jpg" style="width: 90%; max-width: 400px">
</center>
<br/>

## 🟨 Area Light
Simula una **superfície lluminosa rectangular** (o en HDRP també disc).  
- No és un punt, sinó una àrea que emet llum.  
- En el render en temps real està limitat (no funciona en totes les pipelines sense baking).  
- S’usa per fluorescents, pantalles, finestres amb llum entrant…  
- **Efecte**: ombres més suaus i realistes perquè la llum ve d’una superfície gran, no d’un punt.  
<center>
<img src="./assets/llums-lightarea.png" style="width: 90%; max-width: 400px">
</center>
<br/>
